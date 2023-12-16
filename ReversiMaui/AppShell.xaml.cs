using System;
using Persistence;
using Model;
using ReversiMaui.View;

namespace ReversiMaui
{
    public partial class AppShell : Shell
    {
        #region Fields

        private IReversiDataAccess _reversiDataAccess;
        private readonly ReversiGameModel _reversiGameModel;
        private readonly ReversiViewModel _reversiViewModel;

        private readonly IDispatcherTimer _timer;

        private readonly IStore _store;
        private readonly StoredGameBrowserModel _storedGameBrowserModel;
        private readonly StoredGameBrowserViewModel _storedGameBrowserViewModel;

        #endregion

        #region Application methods

        public AppShell(IStore reversiStore,
            IReversiDataAccess reversiDataAccess,
            ReversiGameModel reversiGameModel,
            ReversiViewModel reversiViewModel)
        {
            InitializeComponent();

            // Game setup
            _store = reversiStore;
            _reversiDataAccess = reversiDataAccess;
            _reversiGameModel = reversiGameModel;
            _reversiViewModel = reversiViewModel;

            _timer = Dispatcher.CreateTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (_, _) => _reversiGameModel.AdvanceTime();

            _reversiGameModel.GameOver += ReversiGameModel_GameOver;

            _reversiViewModel.NewGame += ReversiViewModel_NewGame;
            _reversiViewModel.LoadGame += ReversiViewModel_LoadGame;
            _reversiViewModel.SaveGame += ReversiViewModel_SaveGame;
            _reversiViewModel.ExitGame += ReversiViewModel_ExitGame;
            _reversiViewModel.PauseGame += ReversiViewModel_PauseGame;


            // Saved games setup
            _storedGameBrowserModel = new StoredGameBrowserModel(_store);
            _storedGameBrowserViewModel = new StoredGameBrowserViewModel(_storedGameBrowserModel);
            _storedGameBrowserViewModel.GameLoading += StoredGameBrowserViewModel_GameLoading;
            _storedGameBrowserViewModel.GameSaving += StoredGameBrowserViewModel_GameSaving;
        }

        #endregion

        #region Internal methods

        /// <summary>
        ///     Starts the timer used for game progression.
        /// </summary>
        internal void StartTimer() => _timer.Start();

        /// <summary>
        ///     Stops the timer used for game progression.
        /// </summary>
        internal void StopTimer() => _timer.Stop();

        #endregion


        #region Model event handlers

        /// <summary>
        ///     Event handler for game over.
        /// </summary>
        private async void ReversiGameModel_GameOver(object? sender, ReversiEventArgs e)
        {
            StopTimer();

            if (e.IsWon)
            {
                Int32 winner = _reversiGameModel.GetWinner();
                if (winner == 0)
                {
                    await DisplayAlert("Reversi game", "Draw!" + Environment.NewLine +
                    "Player 1 " + TimeSpan.FromSeconds(e.GameTurnCount).ToString("g") + " play time." + Environment.NewLine +
                    "Player 2 " + TimeSpan.FromSeconds(e.GameTime).ToString("g") + " play time.", "OK");
                    
                }
                else
                {
                    await DisplayAlert("Reversi game", $"Player {winner} won!" + Environment.NewLine +
                    "Player 1 " + TimeSpan.FromSeconds(e.GameTurnCount).ToString("g") + " play time." + Environment.NewLine +
                    "Player 2 " + TimeSpan.FromSeconds(e.GameTime).ToString("g") + " play time.",
                    "OK");
                }
            }
        }

        #endregion

        #region ViewModel event handlers

        private void ReversiViewModel_PauseGame(object? sender, EventArgs e)
        {
            if (_timer.IsRunning)
            {
                _reversiViewModel.Paused = true;
                StopTimer();
            }
            else
            {
                _reversiViewModel.Paused = false;
                StartTimer();
            }

        }

        /// <summary>
        ///     Event handler for starting a new game.
        /// </summary>
        private void ReversiViewModel_NewGame(object? sender, EventArgs e)
        {
            _reversiGameModel.NewGame();

            StartTimer();
        }

        /// <summary>
        ///     Event handler for loading a game.
        /// </summary>
        private async void ReversiViewModel_LoadGame(object? sender, EventArgs e)
        {
            await _storedGameBrowserModel.UpdateAsync(); // Update the list of stored games
            await Navigation.PushAsync(new LoadGamePage
            {
                BindingContext = _storedGameBrowserViewModel
            }); // Navigate to the page
        }

        /// <summary>
        ///     Event handler for saving a game.
        /// </summary>
        private async void ReversiViewModel_SaveGame(object? sender, EventArgs e)
        {
            await _storedGameBrowserModel.UpdateAsync(); // Update the list of stored games
            await Navigation.PushAsync(new SaveGamePage
            {
                BindingContext = _storedGameBrowserViewModel
            }); // Navigate to the page
        }

        private async void ReversiViewModel_ExitGame(object? sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage
            {
                BindingContext = _reversiViewModel
            }); // Navigate to the settings page
        }


        /// <summary>
        ///     Event handler for executing the game loading.
        /// </summary>
        private async void StoredGameBrowserViewModel_GameLoading(object? sender, StoredGameEventArgs e)
        {
            await Navigation.PopAsync(); // Navigate back

            // Load the saved game if available
            try
            {
                await _reversiGameModel.LoadGameAsync(e.Name);

                // Successful loading
                await Navigation.PopAsync(); // Navigate back to the game board
                await DisplayAlert("Reversi Game", "Successful loading.", "OK");

                // Start the timer only if the game was successfully loaded
                StartTimer();
                
            }
            catch
            {
                await DisplayAlert("Reversi Game", "Failed to load.", "OK");
            }
        }

        /// <summary>
        ///     Event handler for executing the game saving.
        /// </summary>
        private async void StoredGameBrowserViewModel_GameSaving(object? sender, StoredGameEventArgs e)
        {
            await Navigation.PopAsync(); // Navigate back
            StopTimer();

            try
            {
                // Save the game
                await _reversiGameModel.SaveGameAsync(e.Name);
                await DisplayAlert("Reversi Game", "Successful save.", "OK");
            }
            catch
            {
                await DisplayAlert("Reversi Game", "Failed to save.", "OK");
            }
        }

        #endregion

    }
}
