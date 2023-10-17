using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ReversiFieldEventArgs : EventArgs
    {
        private Int32 x;
        private Int32 y;

        public Int32 X { get { return x; } }
        public Int32 Y { get { return y; } }

        public ReversiFieldEventArgs(Int32 x, Int32 y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
