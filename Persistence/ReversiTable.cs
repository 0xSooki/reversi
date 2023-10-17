using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class ReversiTable
    {

        #region fields

        private Int32[,] board;

        #endregion

        #region properties
        public Boolean IsFilled
        {
            get
            {
                foreach (Int32 value in board)
                    if (value == 0)
                        return false;
                return true;
            }
        }

        public Int32 Size { get { return board.GetLength(0); } }

        public Int32 this[Int32 x, Int32 y] { get { return GetValue(x, y); } }
        #endregion

        #region constructors

        public ReversiTable() : this(20) { }
        public ReversiTable(Int32 size)
        {
            board = new Int32[size, size];
            board[size / 2 - 1, size / 2 - 1] = 1;
            board[size / 2, size / 2] = 1;
            board[size / 2 - 1, size / 2] = 2;
            board[size / 2, size / 2 - 1] = 2;
        }

        #endregion

        #region public methods
        public Int32 GetValue(Int32 x, Int32 y)
        {
            if (x < 0 || x >= board.Length)
                throw new ArgumentOutOfRangeException(nameof(x), "The X coordinate is out of range.");
            if (y < 0 || y >= board.Length)
                throw new ArgumentOutOfRangeException(nameof(y), "The Y coordinate is out of range.");
            return board[x, y];
        }

        public void SetValue(Int32 x, Int32 y, Int32 value)
        {
            if (x < 0 || x >= board.Length)
                throw new ArgumentOutOfRangeException(nameof(x), "The X coordinate is out of range.");
            if (y < 0 || y >= board.Length)
                throw new ArgumentOutOfRangeException(nameof(y), "The Y coordinate is out of range.");

            board[x, y] = value;
        }

        #endregion

        #region private methods

        private Boolean CheckStep(Int32 x, Int32 y)
        {
            return true;
        }

        private Boolean GameOver()
        {
            return true;
        }

        #endregion

    }
}
