﻿namespace Model
{
    public class ReversiEventArgs : EventArgs
    {
        private Int32 gameTime;
        private Boolean isWon;
        private Int32 turnCount;

        public Int32 GameTime { get { return gameTime; } }

        public Int32 GameTurnCount { get { return turnCount; } }

        public Boolean IsWon { get { return isWon; } }

        public ReversiEventArgs(Boolean _isWon, Int32 _gameTime, Int32 _turnCount)
        {
            gameTime = _gameTime;
            isWon = _isWon;
            turnCount = _turnCount;
        }


    }
}