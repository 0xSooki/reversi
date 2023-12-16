using System;

namespace Model
{
    /// <summary>
    /// Stored game model
    /// </summary>
    public class StoredGameModel
    {
        /// <summary>
        /// Get or set the name of the stored game.
        /// </summary>
        public String Name { get; set; } = String.Empty;

        /// <summary>
        /// Get or set the date and time of the last modification.
        /// </summary>
        public DateTime Modified { get; set; }
    }
}
