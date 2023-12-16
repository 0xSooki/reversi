using System;

namespace ReversiMaui
{
    /// <summary>
    /// Stored game view model
    /// </summary>
    public class StoredGameViewModel : ViewModelBase
    {
        private String _name = String.Empty;
        private DateTime _modified;

        /// <summary>
        /// Get name of the stored game.
        /// </summary>
        public String Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Get or set the date and time of the last modification.
        /// </summary>
        public DateTime Modified
        {
            get { return _modified; }
            set
            {
                if (_modified != value)
                {
                    _modified = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Load command
        /// </summary>
        public DelegateCommand? LoadGameCommand { get; set; }

        /// <summary>
        /// Save command
        /// </summary>
        public DelegateCommand? SaveGameCommand { get; set; }
    }
}