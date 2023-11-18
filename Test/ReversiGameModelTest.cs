using Model;
using Moq;
using Persistence;

namespace Test
{
    [TestClass]
    public class ReversiGameModelTest
    {
        private ReversiGameModel model = null!;
        private ReversiTable table = null!;
        private Mock<IReversiDataAccess> mock = null!;

        [TestInitialize]
        public void Initialize()
        {
            table = new ReversiTable(10);

            mock = new Mock<IReversiDataAccess>();
            mock.Setup(mock => mock.LoadAsync((It.IsAny<String>())))
                .Returns(() => Task.FromResult((table, 0, 0, 1)));

            model = new ReversiGameModel(mock.Object);
            model.BoardSize = BoardSize.Smol;

            model.GameAdvanced += new EventHandler<ReversiEventArgs>(Model_GameAdvanced);
            model.GameOver += new EventHandler<ReversiEventArgs>(Model_GameOver);
        }

        [TestMethod]
        public void TestNewGame()
        {
            model.NewGame();

            Assert.AreEqual(0, model.GameTime);
            Assert.AreEqual(0, model.TurnCount);
            Assert.AreEqual(BoardSize.Smol, model.BoardSize);
            Assert.AreEqual(1, model.CurrentPlayer);

            for (Int32 i = 0; i < 10; i++)
                for (Int32 j = 0; j < 10; j++)
                {
                    Assert.AreEqual(table.GetValue(i, j), model.Table.GetValue(i, j));
                }
        }

        [TestMethod]
        public void TestSmolGame()
        {
            model.BoardSize = BoardSize.Smol;
            model.NewGame();
            Assert.AreEqual(10, model.Table.Size);
        }

        [TestMethod]
        public void TestMediumGame()
        {
            model.BoardSize = BoardSize.Medium;
            model.NewGame();
            Assert.AreEqual(20, model.Table.Size);
        }

        [TestMethod]
        public void TestBigGame()
        {
            model.BoardSize = BoardSize.Big;
            model.NewGame();
            Assert.AreEqual(30, model.Table.Size);
        }

        [TestMethod]
        public void TestAdvanceTime()
        {
            int initialGameTime = model.GameTime;

            model.AdvanceTime();

            Assert.AreEqual(initialGameTime + 1, model.GameTime);
            Assert.AreEqual(1, model.GameTime);

        }

        [TestMethod]
        public void TestGetWinner()
        {
            for (int i = 0; i < model.Table.Size; i++)
            {
                for (int j = 0; j < model.Table.Size; j++)
                {
                    if (model.Table[i, j] == 0)
                    {
                        model.Table.SetValue(i, j, 1);
                    }
                }
            }

            Assert.AreEqual(1, model.GetWinner());
        }

        [TestMethod]
        public void TestStep()
        {
            model.NewGame();
            model.Step(3, 4);

            Assert.AreEqual(1, model.CurrentPlayer);

            model.Step(3, 5);
            Assert.AreEqual(2, model.CurrentPlayer);

            model.Step(3, 4);
            Assert.AreEqual(1, model.CurrentPlayer);
        }

        [TestMethod]
        public void TestGameAdvanced()
        {
            model.NewGame();
            bool advancedRaised = false;
            model.GameAdvanced += (sender, e) =>
            {
                advancedRaised = true;
            };
            model.Step(5, 3);

            Assert.IsTrue(advancedRaised);
        }

        [TestMethod]
        public void TestPass()
        {
            model.NewGame();
            model.Table.SetValue(4, 4, 0);
            model.Table.SetValue(4, 5, 0);
            model.Table.SetValue(5, 4, 0);
            model.Table.SetValue(5, 5, 0);
            model.Table.SetValue(0, 1, 2);
            model.Table.SetValue(0, 2, 1);
            model.Table.SetValue(0, 3, 2);

            model.Step(0, 0);

            model.AdvanceTime();

            Assert.AreEqual(1, model.CurrentPlayer);
        }

        [TestMethod]
        public void TestWinBySteps()
        {
            bool winRaised = false;
            model.GameOver += (sender, e) =>
            {
                winRaised = true;
            };

            model.NewGame();
            model.Step(5, 3);
            Assert.AreEqual(2, model.CurrentPlayer);
            model.Step(6, 3);
            Assert.AreEqual(1, model.CurrentPlayer);
            model.Step(7, 3);
            Assert.AreEqual(2, model.CurrentPlayer);
            model.Step(5, 2);
            Assert.AreEqual(1, model.CurrentPlayer);
            model.Step(4, 1);
            Assert.AreEqual(2, model.CurrentPlayer);
            model.Step(5, 6);
            Assert.AreEqual(1, model.CurrentPlayer);
            model.Step(5, 7);
            Assert.AreEqual(2, model.CurrentPlayer);
            model.Step(4, 3);
            Assert.AreEqual(1, model.CurrentPlayer);
            model.Step(3, 4);

            Assert.IsTrue(winRaised);
            Assert.AreEqual(1, model.GetWinner());
            Assert.AreNotEqual(2, model.GetWinner());
        }

        [TestMethod]
        public void TestDraw()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i < 5)
                    {
                        model.Table.SetValue(i, j, 1);
                    }
                    else
                    {
                        model.Table.SetValue(i, j, 2);
                    }

                }
            }
            Assert.AreEqual(-1, model.GetWinner());
        }

        [TestMethod]
        public async Task TestLoadGameAsync()
        {
            model.NewGame();

            await model.LoadGameAsync(String.Empty);

            for (Int32 i = 0; i < 3; i++)
                for (Int32 j = 0; j < 3; j++)
                {
                    Assert.AreEqual(table.GetValue(i, j), model.Table.GetValue(i, j));

                }

            Assert.AreEqual(0, model.TurnCount);

            mock.Verify(dataAccess => dataAccess.LoadAsync(String.Empty), Times.Once());
        }

        private void Model_GameAdvanced(Object? sender, ReversiEventArgs e)
        {
            Assert.IsTrue(model.GameTime >= 0);

            Assert.AreEqual(e.GameTurnCount, model.TurnCount);
            Assert.AreEqual(e.GameTime, model.GameTime);
            Assert.IsFalse(e.IsWon);
        }

        private void Model_GameOver(Object? sender, ReversiEventArgs e)
        {
            Assert.IsTrue(model.IsGameOver);
            Assert.AreEqual(0, e.GameTime);
        }
    }
}
