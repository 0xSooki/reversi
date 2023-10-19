using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        public Boolean CanPlayValidMove(Int32 player)
        {
            return GetValidMoves(player).Count > 0;
        }

        public Int32 CountPieces(Int32 player)
        {
            int sum = 0;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    if (board[i,j] == player) sum++;
                }
            }
            return sum;
        }

        public Int32 GetValue(Int32 x, Int32 y)
        {
            if (x < 0 || x >= board.GetLength(0))
                throw new ArgumentOutOfRangeException(nameof(x), "The X coordinate is out of range.");
            if (y < 0 || y >= board.GetLength(0))
                throw new ArgumentOutOfRangeException(nameof(y), "The Y coordinate is out of range.");
            return board[x, y];
        }
        
        public void SetValue(Int32 x, Int32 y, Int32 value)
        {
            if (x < 0 || x >= board.GetLength(0))
                throw new ArgumentOutOfRangeException(nameof(x), "The X coordinate is out of range.");
            if (y < 0 || y >= board.GetLength(0))
                throw new ArgumentOutOfRangeException(nameof(y), "The Y coordinate is out of range.");

            board[x, y] = value;
        }

        #endregion

        #region private methods

        public void Flip(List<(int, int)> piecesToFlip, int player)
        {
            foreach ((int, int) piece in piecesToFlip)
            {
                board[piece.Item1, piece.Item2] = player;
            }
        }

        public List<(int, int)> FindPiecesToFlip(int x, int y, int player)
        {
            List<(int, int)> piecesToFlip = new List<(int, int)>();
            int[] dx = { -1, -1, 0, 1, 1, 1, 0, -1 };
            int[] dy = { 0, 1, 1, 1, 0, -1, -1, -1 };

            for (int direction = 0; direction < 8; direction++)
            {
                int currentX = x + dx[direction];
                int currentY = y + dy[direction];
                List<(int, int)> piecesInDirection = new List<(int, int)>();
                bool foundOpponentPiece = false;

                while (currentX >= 0 && currentX < board.GetLength(0) && currentY >= 0 && currentY < board.GetLength(1))
                {
                    if (board[currentX, currentY] == 0)
                        break; 

                    if (board[currentX, currentY] == player)
                    {
                        if (foundOpponentPiece)
                        {
                            piecesToFlip.AddRange(piecesInDirection); 
                        }
                        break;
                    }

                    if (board[currentX, currentY] == 3 - player)
                    {
                        foundOpponentPiece = true; 
                        piecesInDirection.Add((currentX, currentY)); 
                    }

                    currentX += dx[direction];
                    currentY += dy[direction];
                }
            }

            return piecesToFlip;
        }


        public List<(int, int)> GetValidMoves(int player)
        {
            List<(int, int)> validMoves = new List<(int, int)>();

            int[] dx = { -1, -1, 0, 1, 1, 1, 0, -1 };
            int[] dy = { 0, 1, 1, 1, 0, -1, -1, -1 };

            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    if (board[x, y] != 0)
                        continue;

                    for (int direction = 0; direction < 8; direction++)
                    {
                        int currentX = x + dx[direction];
                        int currentY = y + dy[direction];
                        bool foundOpponentPiece = false;

                        while (currentX >= 0 && currentX < board.GetLength(0) && currentY >= 0 && currentY < board.GetLength(1))
                        {
                            if (board[currentX, currentY] == 0)
                                break; 

                            if (board[currentX, currentY] == player)
                            {
                                if (foundOpponentPiece)
                                    validMoves.Add((x, y)); 
                                break; 
                            }

                            if (board[currentX, currentY] == 3 - player)
                            {
                                foundOpponentPiece = true;
                            }

                            currentX += dx[direction];
                            currentY += dy[direction];
                        }
                    }
                }
            }

            return validMoves;
        }

        #endregion

    }
}
