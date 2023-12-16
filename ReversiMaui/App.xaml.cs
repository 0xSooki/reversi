using Persistence;
using Model;
using ReversiMaui.Persistence;

namespace ReversiMaui
{
    public partial class App : Application
    {
        /// <summary>
        /// Save the path for storing suspended games.
        /// </summary>
        private const string SuspendedGameSavePath = "SuspendedGame";

        private readonly AppShell _appShell;
        private readonly IReversiDataAccess _reversiDataAccess;
        private readonly ReversiGameModel _reversiGameModel;
        private readonly IStore _reversiStore;
        private readonly ReversiViewModel _reversiViewModel;

        public App()
        {
            InitializeComponent();

            _reversiStore = new ReversiStore();
            _reversiDataAccess = new ReversiFileDataAccess(FileSystem.AppDataDirectory);

            _reversiGameModel = new ReversiGameModel(_reversiDataAccess);
            _reversiViewModel = new ReversiViewModel(_reversiGameModel);

            _appShell = new AppShell(_reversiStore, _reversiDataAccess, _reversiGameModel, _reversiViewModel)
            {
                BindingContext = _reversiViewModel
            };
            MainPage = _appShell;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Window window = base.CreateWindow(activationState);

            // When the application is launched
            window.Created += (s, e) =>
            {
                // Start a new game
                _reversiGameModel.NewGame();
                _appShell.StartTimer();
            };

            // When the application gains focus
            window.Activated += (s, e) =>
            {
                if (!File.Exists(Path.Combine(FileSystem.AppDataDirectory, SuspendedGameSavePath)))
                    return;

                Task.Run(async () =>
                {
                    // Load the suspended game if available
                    try
                    {
                        await _reversiGameModel.LoadGameAsync(SuspendedGameSavePath);

                        // Start the timer only if the game was successfully loaded
                        _appShell.StartTimer();
                    }
                    catch
                    {
                    }
                });
            };

            // When the application loses focus
            window.Deactivated += (s, e) =>
            {
                Task.Run(async () =>
                {
                    try
                    {
                        // Save the current game in progress
                        _appShell.StopTimer();
                        await _reversiGameModel.SaveGameAsync(SuspendedGameSavePath);
                    }
                    catch
                    {
                    }
                });
            };

            return window;
        }
    }
}
