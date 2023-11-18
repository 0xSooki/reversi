using System;

namespace ViewWPF
{
    public class ReversiField : ViewModelBase
    {
        private Int32 _player;

        public Int32 Player
        {
            get { return _player; }
            set
            {
                _player = value;
                OnPropertyChanged();
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
