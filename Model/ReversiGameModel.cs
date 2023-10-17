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
        private const Int32 FieldCountSmol = 10;
        private const Int32 FieldCountMedium = 20;
        private const Int32 FieldCountBig = 30;

        private ReversiTable table;
        private IReversiDataAccess dataAccess;
        private BoardSize boardSize;
        private Int32 currentPlayer;
        private Int32 gameTime;
        private Int32 turnCount;

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

        #endregion
    }
}
