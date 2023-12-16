using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Persistence;

namespace Model
{
    /// <summary>
    /// Stored game browser model
    /// </summary>
    public class StoredGameBrowserModel
    {
        private IStore _store; // a perzisztencia

        /// <summary>
        /// Change event for the list of stored games.
        /// </summary>
        public event EventHandler? StoreChanged;

        public StoredGameBrowserModel(IStore store)
        {
            _store = store;

            StoredGames = new List<StoredGameModel>();
        }

        /// <summary>
        /// Get the list of stored games.
        /// </summary>
        public List<StoredGameModel> StoredGames { get; private set; }

        /// <summary>
        /// Update the list of stored games.
        /// </summary>
        public async Task UpdateAsync()
        {
            if (_store == null)
                return;

            StoredGames.Clear();

            // betöltjük a mentéseket
            foreach (String name in await _store.GetFilesAsync())
            {
                if (name == "SuspendedGame") 
                    continue;

                StoredGames.Add(new StoredGameModel
                {
                    Name = name,
                    Modified = await _store.GetModifiedTimeAsync(name)
                });
            }

            StoredGames = StoredGames.OrderByDescending(item => item.Modified).ToList();
            
            OnSavesChanged();
        }

        private void OnSavesChanged()
        {
            StoreChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
