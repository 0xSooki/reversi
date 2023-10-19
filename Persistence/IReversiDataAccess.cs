using System;
using System.Threading.Tasks;

namespace Persistence
{
    public interface IReversiDataAccess
    {
        Task<(ReversiTable, Int32, Int32, Int32)> LoadAsync(String path);

        Task SaveAsync(String path, ReversiTable table, int currentPlayer, int p1Time, int gameTime);
    }
}