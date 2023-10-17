using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class ReversiFileDataAccess : IReversiDataAccess
    {
        Task<ReversiTable> IReversiDataAccess.LoadAsync(string path)
        {
            throw new NotImplementedException();
        }

        Task IReversiDataAccess.SaveAsync(string path, ReversiTable table)
        {
            throw new NotImplementedException();
        }
    }
}
