using System;
using System.Threading.Tasks;

namespace Persistence
{
    public interface IReversiDataAccess
    {
        Task<ReversiTable> LoadAsync(String path);

        Task SaveAsync(String path, ReversiTable table);
    }
}