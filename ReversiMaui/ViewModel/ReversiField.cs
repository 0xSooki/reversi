using System;

namespace ReversiMaui
{
    public class ReversiField : ViewModelBase
    {
        private Color _green = Color.FromRgb(0, 200, 0);
        private Color _black = Color.FromRgb(0, 0, 0); 
        private Color _white = Color.FromRgb(255, 255, 255);


        private Int32 _player;

        public Int32 Player
        {
            get { return _player; }
            set
            {
                _player = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Player));
                OnPropertyChanged(nameof(Color));

            }
        }

        public Color Color
        {
            get
            {
                return Player switch
                {
                    1 => _black,
                    2 => _white,
                    _ => _green,
                };
            }
        }

        public Int32 X { get; set; }
        public Int32 Y { get; set; }

        public Tuple<Int32, Int32> XY
        {
            get
            {
                return new(X, Y);
            }
        }

        public DelegateCommand? StepCommand { get; set; }

    }
}
