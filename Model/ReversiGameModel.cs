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

        public Boolean IsGameOver { get { return ((table.GetValidMoves(1).Count == 0 && table.GetValidMoves(2).Count == 0) || table.IsFilled); } }

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
            if (table.IsFilled)
                return;
            if (CurrentPlayer == 1)
            {
                turnCount++;
            }
            gameTime++;
            OnGameAdvanced();
        }

        public Int32 GetWinner()
        {
            if (!IsGameOver) return -1;
            Int32 p1 = table.CountPieces(1);
            Int32 p2 = table.CountPieces(2);
            if (p1 > p2) return 1;
            if (p2 > p1) return 2;
            return 0;
        }

        public void Step(Int32 x, Int32 y)
        {
            if (IsGameOver)
            {
                return;
            }

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

            if (currentPlayer == 1)
            {
                if ( table.CanPlayValidMove(2))
                currentPlayer = 2;
                else currentPlayer = 1;
            }
            else if (currentPlayer == 2)
            {
                if (table.CanPlayValidMove(1))
                    currentPlayer = 1;
                else currentPlayer = 2;
            } else {                 if (GameOver != null)
                {
                    GameOver(this, new ReversiEventArgs(true, gameTime, turnCount));
                }
            }

            OnGameAdvanced();
            if (IsGameOver)
            {
                OnGameOver(true);
            }
        }

        public async Task SaveGameAsync(String path)
        {
            if (dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            await dataAccess.SaveAsync(path, table, currentPlayer, turnCount, gameTime);
        }

        public async Task LoadGameAsync(String path)
        {
            if (dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

             (ReversiTable, int, int, int) data = await dataAccess.LoadAsync(path);

            table = data.Item1;
            currentPlayer = data.Item4;
            turnCount = data.Item3;
            gameTime = data.Item2;

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

        private void OnGameOver(Boolean isWon)
        {
            GameOver?.Invoke(this, new ReversiEventArgs(isWon, gameTime-turnCount, turnCount));
        }


        #endregion
    }
}
