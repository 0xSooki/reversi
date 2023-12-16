using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence
{
    /// <summary>
    /// Game store interface.
    /// </summary>
    public interface IStore
    {
        /// <summary>
        /// Get the files.
        /// </summary>
        /// <returns>List of files</returns>
        Task<IEnumerable<String>> GetFilesAsync();

        /// <summary>
        /// Get the last modified time.
        /// </summary>
        /// <param name="name">Name of file</param>
        /// <returns>Last change</returns>
        Task<DateTime> GetModifiedTimeAsync(String name);
    }
}
