using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Persistence;
using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
        public void TestNewGame() { 
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
        public void TestSmolGame() {
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

            int winner = model.GetWinner();

            Assert.AreEqual(1, winner);
        }

        [TestMethod]
        public void TestStep()
        {
            model.NewGame();
            model.Step(3, 4);

            Assert.AreEqual(1, model.CurrentPlayer);
        }

        [TestMethod]
        public void ReversiGameModelAdvanceTimeTest()
        {
            model.NewGame();

            Int32 time = model.GameTime;
            model.AdvanceTime();

            Assert.AreEqual(1, model.GameTime);
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
            Assert.AreEqual(model.GameTime == 0, model.IsGameOver);

            Assert.AreEqual(e.GameTurnCount, model.TurnCount);
            Assert.AreEqual(e.GameTime, model.GameTime);
            Assert.IsFalse(e.IsWon);
        }

        private void Model_GameOver(Object? sender, ReversiEventArgs e)
        {
            Assert.IsTrue(model.IsGameOver); 
            Assert.AreEqual(0, e.GameTime);
            Assert.IsFalse(e.IsWon);
        }
    }
}
