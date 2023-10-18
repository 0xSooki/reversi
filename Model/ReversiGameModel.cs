using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum BoardSize { Smol, Medium, Big }
    public class ReversiGameModel
    {
        #region consts

        private const Int32 FieldCountSmol = 10;
        private const Int32 FieldCountMedium = 20;
        private const Int32 FieldCountBig = 30;

        #endregion

        #region fields

        private ReversiTable table;
        private IReversiDataAccess dataAccess;
        private BoardSize boardSize;
        private Int32 currentPlayer;
        private Int32 gameTime;
        private Int32 player1Time;
        private Int32 player2Time;

        private Int32 turnCount;

        #endregion

        #region properties

        public Int32 CurrentPlayer
        {
            get { return currentPlayer; }
            set { currentPlayer = value; }
        }

        public Int32 GameTime
        {
            get { return gameTime; }
        }

        public ReversiTable Table
        {
            get { return table; }
        }

        public Int32 TurnCount
        {
            get { return turnCount; }
        }

        public BoardSize BoardSize
        {
            get { return boardSize; } set { boardSize = value; }
        }

        #endregion

        #region events

        public event EventHandler<ReversiFieldEventArgs>? FieldChanged;

        public event EventHandler<ReversiEventArgs>? GameOver;

        public event EventHandler<ReversiEventArgs>? GameAdvanced;

        #endregion

        #region constructor

        public ReversiGameModel(IReversiDataAccess _dataAccess)
        {
            dataAccess = _dataAccess;
            boardSize = BoardSize.Medium;
            table = new ReversiTable();
        }

        #endregion

        #region public methods

        public void NewGame()
        {
            currentPlayer = 1;
            turnCount = 0;

            switch (boardSize)
            {
                case BoardSize.Smol:
                    table = new ReversiTable(FieldCountSmol);
                    break;
                case BoardSize.Big:
                    table = new ReversiTable(FieldCountBig);
                    break;
                default:
                    table = new ReversiTable(FieldCountMedium);
                    break;
            }
        }

        public void AdvanceTime()
        {
            if (GameOver != null)
            {
                if (table.IsFilled)
                {
                    GameOver(this, new ReversiEventArgs(false, gameTime, turnCount));
                }
            }
        }

        public void Step(Int32 x, Int32 y)
        {

            if (table[x, y] != 0)
            {
                return;
            }

            if (GameOver != null)
            {
                   if (table.IsFilled)
                {
                    GameOver(this, new ReversiEventArgs(false, gameTime, turnCount));
                }
            }

            List<(Int32, Int32)> validMoves = table.GetValidMoves(currentPlayer);
            if (!validMoves.Contains((x, y)))
            {
                return;
            }

            table.SetValue(x, y, currentPlayer);
            OnFieldChanged(x, y);

            List<(int, int)> values = table.FindPiecesToFlip(x, y, currentPlayer);
            table.Flip(values, currentPlayer);

            foreach ((int, int) value in values)
            {
                OnFieldChanged(value.Item1, value.Item2);
            }


            

            if (currentPlayer == 1 && table.CanPlayValidMove(2))
            {
                currentPlayer = 2;
            }
            else if (currentPlayer == 2 && table.CanPlayValidMove(1))
            {
                currentPlayer = 1;
            } else {                 if (GameOver != null)
                {
                    GameOver(this, new ReversiEventArgs(false, gameTime, currentPlayer));
                }
            }
            turnCount++;



            OnGameAdvanced();
        }

        public async Task SaveGameAsync(String path)
        {
            if (dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            await dataAccess.SaveAsync(path, table);
        }

        public async Task LoadGameAsync(String path)
        {
            if (dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            table = await dataAccess.LoadAsync(path);


            switch (BoardSize)
            {
                case BoardSize.Smol:
                    boardSize = BoardSize.Smol;
                    break;
                case BoardSize.Medium:
                    boardSize = BoardSize.Medium;
                    break;
                case BoardSize.Big:
                    boardSize = BoardSize.Big;
                    break;
            }
        }

        #endregion

        #region private event methods


        private void OnFieldChanged(Int32 x, Int32 y)
        {
            FieldChanged?.Invoke(this, new ReversiFieldEventArgs(x, y));
        }

        private void OnGameAdvanced()
        {
            GameAdvanced?.Invoke(this, new ReversiEventArgs(false, gameTime, currentPlayer));
        }

        #endregion
    }
}
