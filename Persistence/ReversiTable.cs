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

        /// <summary>
        /// checks whether the given player can make a valid move
        /// </summary>
        /// <param name="player"></param>
        /// <returns>whether the player can make a move</returns>
        public Boolean CanPlayValidMove(Int32 player)
        {
            return GetValidMoves(player).Count > 0;
        }

        /// <summary>
        /// counts the pieces for the given player
        /// </summary>
        /// <param name="player"></param>
        /// <returns>the pieces of the current player placed on the board</returns>
        public Int32 CountPieces(Int32 player)
        {
            int sum = 0;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    if (board[i, j] == player) sum++;
                }
            }
            return sum;
        }

        /// <summary>
        /// getter for a value at given x and y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>1 or 2 for players and 0 if the spot is not yet occupied</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Int32 GetValue(Int32 x, Int32 y)
        {
            if (x < 0 || x >= board.GetLength(0))
                throw new ArgumentOutOfRangeException(nameof(x), "The X coordinate is out of range.");
            if (y < 0 || y >= board.GetLength(0))
                throw new ArgumentOutOfRangeException(nameof(y), "The Y coordinate is out of range.");
            return board[x, y];
        }

        /// <summary>
        /// setter for value at given x and y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void SetValue(Int32 x, Int32 y, Int32 value)
        {
            if (x < 0 || x >= board.GetLength(0))
                throw new ArgumentOutOfRangeException(nameof(x), "The X coordinate is out of range.");
            if (y < 0 || y >= board.GetLength(0))
                throw new ArgumentOutOfRangeException(nameof(y), "The Y coordinate is out of range.");

            board[x, y] = value;
        }

        /// <summary>
        /// flips the given pieces to the others players color
        /// </summary>
        /// <param name="piecesToFlip"></param>
        /// <param name="player"></param>
        public void Flip(List<(int, int)> piecesToFlip, int player)
        {
            foreach ((int, int) piece in piecesToFlip)
            {
                board[piece.Item1, piece.Item2] = player;
            }
        }

        /// <summary>
        /// finds pieces intersecting the piece placed at x & y that needs to be flipped
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="player"></param>
        /// <returns>list of pieces that are intersecting the placed piece and need to be flipped</returns>
        public List<(int, int)> FindPiecesToFlip(int x, int y, int player)
        {
            List<(int, int)> piecesToFlip = new List<(int, int)>();
            int[] dx = { -1, -1, 0, 1, 1, 1, 0, -1 };
            int[] dy = { 0, 1, 1, 1, 0, -1, -1, -1 };

            for (int dir = 0; dir < 8; dir++)
            {
                int currX = x + dx[dir];
                int currY = y + dy[dir];
                List<(int, int)> piecesInDirection = new List<(int, int)>();
                bool found = false;

                while (currX >= 0 && currX < board.GetLength(0) && currY >= 0 && currY < board.GetLength(0))
                {
                    if (board[currX, currY] == 0)
                        break;

                    if (board[currX, currY] == player)
                    {
                        if (found)
                        {
                            piecesToFlip.AddRange(piecesInDirection);
                        }
                        break;
                    }

                    if (board[currX, currY] == 3 - player)
                    {
                        found = true;
                        piecesInDirection.Add((currX, currY));
                    }

                    currX += dx[dir];
                    currY += dy[dir];
                }
            }

            return piecesToFlip;
        }

        /// <summary>
        /// gets all the valid moves a player can make
        /// </summary>
        /// <param name="player"></param>
        /// <returns>a list of valid moves for the given player</returns>
        public List<(int, int)> GetValidMoves(int player)
        {
            List<(int, int)> validMoves = new List<(int, int)>();

            int[] dx = { -1, -1, 0, 1, 1, 1, 0, -1 };
            int[] dy = { 0, 1, 1, 1, 0, -1, -1, -1 };

            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(0); y++)
                {
                    if (board[x, y] != 0)
                        continue;

                    for (int dir = 0; dir < 8; dir++)
                    {
                        int currX = x + dx[dir];
                        int currY = y + dy[dir];
                        bool found = false;

                        while (currX >= 0 && currX < board.GetLength(0) && currY >= 0 && currY < board.GetLength(0))
                        {
                            if (board[currX, currY] == 0)
                                break;

                            if (board[currX, currY] == player)
                            {
                                if (found)
                                    validMoves.Add((x, y));
                                break;
                            }

                            if (board[currX, currY] == 3 - player)
                            {
                                found = true;
                            }

                            currX += dx[dir];
                            currY += dy[dir];
                        }
                    }
                }
            }

            return validMoves;
        }

        #endregion
    }
}
