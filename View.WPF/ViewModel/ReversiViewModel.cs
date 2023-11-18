using Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ViewWPF
{
    public class ReversiViewModel : ViewModelBase
    {
        #region Fields

        private ReversiGameModel _model;
        private bool _isPaused;

        #endregion

        #region Properties

        public DelegateCommand NewGameCommand { get; private set; }

        public DelegateCommand LoadGameCommand { get; private set; }

        public DelegateCommand SaveGameCommand { get; private set; }

        public DelegateCommand ExitCommand { get; private set; }

        public DelegateCommand PauseGameCommand { get; private set; }

        public ObservableCollection<ReversiField> Fields { get; set; }

        public String IsPaused { get { return _isPaused ? "Continue" : "Pause"; } }

        public bool Paused
        {
            get { return _isPaused; }
            set
            {
                _isPaused = value;
                OnPropertyChanged(nameof(IsPaused));
            }
        }

        public String CurrentPlayer
        {
            get
            {
                if (_model.CurrentPlayer == 1)
                    return "White";
                else
                    return "Black";
            }
        }

        public String TurnCount { get { return TimeSpan.FromSeconds(_model.GameTime - _model.TurnCount).ToString("g"); } }

        public String GameTime { get { return TimeSpan.FromSeconds(_model.TurnCount).ToString("g"); } }

        public Int32 Size { get { return _model.Table.Size; } }



        public Boolean IsGameSmol
        {
            get { return _model.BoardSize == BoardSize.Smol; }
            set
            {
                if (_model.BoardSize == BoardSize.Smol)
                    return;

                _model.BoardSize = BoardSize.Smol;
                OnPropertyChanged(nameof(IsGameSmol));
                OnPropertyChanged(nameof(IsGameMedium));
                OnPropertyChanged(nameof(IsGameBig));
            }
        }

        public Boolean IsGameMedium
        {
            get { return _model.BoardSize == BoardSize.Medium; }
            set
            {
                if (_model.BoardSize == BoardSize.Medium)
                    return;

                _model.BoardSize = BoardSize.Medium;
                OnPropertyChanged(nameof(IsGameSmol));
                OnPropertyChanged(nameof(IsGameMedium));
                OnPropertyChanged(nameof(IsGameBig));
            }
        }

        public Boolean IsGameBig
        {
            get { return _model.BoardSize == BoardSize.Big; }
            set
            {
                if (_model.BoardSize == BoardSize.Big)
                    return;

                _model.BoardSize = BoardSize.Big;
                OnPropertyChanged(nameof(IsGameSmol));
                OnPropertyChanged(nameof(IsGameMedium));
                OnPropertyChanged(nameof(IsGameBig));
            }
        }

        #endregion


        #region Constructors

        public ReversiViewModel(ReversiGameModel model)
        {
            _model = model;
            _model.FieldChanged += new EventHandler<ReversiFieldEventArgs>(Model_FieldChanged);
            _model.GameAdvanced += new EventHandler<ReversiEventArgs>(Model_GameAdvanced);
            _model.GameOver += new EventHandler<ReversiEventArgs>(Model_GameOver);
            _model.GameCreated += new EventHandler<ReversiEventArgs>(Mode_GameCreated);

            NewGameCommand = new DelegateCommand(param => OnNewGame());
            LoadGameCommand = new DelegateCommand(param => OnLoadGame());
            SaveGameCommand = new DelegateCommand(param => OnSaveGame());
            ExitCommand = new DelegateCommand(param => OnExit());
            PauseGameCommand = new DelegateCommand(param => OnPause());

            Fields = new ObservableCollection<ReversiField>();
            for (Int32 i = 0; i < _model.Table.Size; i++)
            {
                for (Int32 j = 0; j < _model.Table.Size; j++)
                {
                    Fields.Add(new ReversiField
                    {
                        X = i,
                        Y = j,
                        Player = _model.Table[i, j],
                        StepCommand = new DelegateCommand(param =>
                    {
                        if (param is Tuple<Int32, Int32> pos) StepGame(pos.Item1, pos.Item2);
                    })
                    });
                }
            }

            RefreshTable();
        }

        #endregion

        #region Private Methods

        private void StepGame(Int32 x, Int32 y)
        {
            _model.Step(x, y);
            OnPropertyChanged(nameof(CurrentPlayer));
        }

        public void RefreshTable()
        {
            Fields = new ObservableCollection<ReversiField>();
            for (Int32 i = 0; i < _model.Table.Size; i++)
            {
                for (Int32 j = 0; j < _model.Table.Size; j++)
                {
                    ReversiField field = new ReversiField
                    {
                        X = i,
                        Y = j,
                        Player = _model.Table[i, j],
                        StepCommand = new DelegateCommand(param =>
                        {
                            if (param is Tuple<Int32, Int32> pos) StepGame(pos.Item1, pos.Item2);
                        })
                    };
                    Fields.Add(field);


                }
            }
            OnPropertyChanged(nameof(Fields));
            OnPropertyChanged(nameof(Size));
            OnPropertyChanged(nameof(GameTime));
            OnPropertyChanged(nameof(TurnCount));
        }


        #endregion 

        #region Game Event Handlers

        private void Model_FieldChanged(object? sender, ReversiFieldEventArgs e)
        {
            ReversiField field = Fields.Single(f => f.X == e.X && f.Y == e.Y);

            field.Player = _model.Table[field.X, field.Y] == 0 ? 0 : _model.Table[field.X, field.Y];
            OnPropertyChanged(nameof(TurnCount));
        }

        private void Model_GameOver(object? sender, EventArgs e)
        {
        }

        private void Model_GameAdvanced(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(GameTime));
            OnPropertyChanged(nameof(TurnCount));
        }

        private void Mode_GameCreated(object? sender, EventArgs e)
        {
            RefreshTable();
        }


        #endregion

        #region Events handlers

        public event EventHandler? NewGame;

        public event EventHandler? LoadGame;

        public event EventHandler? SaveGame;

        public event EventHandler? ExitGame;

        public event EventHandler? PauseGame;

        #endregion

        #region Events

        private void OnNewGame()
        {
            NewGame?.Invoke(this, EventArgs.Empty);
        }

        private void OnLoadGame()
        {
            LoadGame?.Invoke(this, EventArgs.Empty);
        }

        private void OnSaveGame()
        {
            SaveGame?.Invoke(this, EventArgs.Empty);
        }

        private void OnExit()
        {
            ExitGame?.Invoke(this, EventArgs.Empty);
        }

        private void OnPause()
        {
            PauseGame?.Invoke(this, EventArgs.Empty);
        }

        #endregion

    }
}
