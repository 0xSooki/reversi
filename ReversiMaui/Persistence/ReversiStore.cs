using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence;

namespace ReversiMaui.Persistence
{
    public class ReversiStore : IStore
    {

        /// <summary>
        /// Get files
        /// </summary>
        /// <returns>List of files</returns>
        public async Task<IEnumerable<string>> GetFilesAsync()
        {
            return await Task.Run(() => Directory.GetFiles(FileSystem.AppDataDirectory)
                .Select(Path.GetFileName)
                .Where(name => name?.EndsWith(".stl") ?? false)
                .OfType<string>());
        }

        /// <summary>
        /// Retrieve the modification time.
        /// </summary>
        /// <param name="name">The name of the file.</param>
        /// <returns>The last modification time.</returns>
        public async Task<DateTime> GetModifiedTimeAsync(string name)
        {
            var info = new FileInfo(Path.Combine(FileSystem.AppDataDirectory, name));

            return await Task.Run(() => info.LastWriteTime);
        }
    }
}
