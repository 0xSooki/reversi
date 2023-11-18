using Microsoft.Win32;
using Model;
using Persistence;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ViewWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Fields

        private ReversiGameModel _model = null!;
        private ReversiViewModel _viewModel = null!;
        private MainWindow _view = null!;
        private DispatcherTimer _timer = null!;

        #endregion

        #region Constructors

        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }

        #endregion

        #region App event handlers

        private void App_Startup(Object sender, StartupEventArgs e)
        {
            _model = new ReversiGameModel(new ReversiFileDataAccess());
            _model.GameOver += new EventHandler<ReversiEventArgs>(Model_GameOver);
            _model.NewGame();

            _viewModel = new ReversiViewModel(_model);
            _viewModel.NewGame += new EventHandler(ViewModel_NewGame);
            _viewModel.LoadGame += new EventHandler(ViewModel_LoadGame);
            _viewModel.SaveGame += new EventHandler(ViewModel_SaveGame);
            _viewModel.ExitGame += new EventHandler(ViewModel_ExitGame);

            _view = new MainWindow();
            _view.DataContext = _viewModel;
            _view.Closing += new System.ComponentModel.CancelEventHandler(View_Closing);
            _view.Show();


            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += new EventHandler(Timer_Tick);
            _timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            _model.AdvanceTime();
        }

        #endregion

        #region View event handlers

        private void View_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            bool restart = _timer.IsEnabled;

            _timer.Stop();

            if (MessageBox.Show("Do you really want to exit?", "Reversi", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                e.Cancel = true;

                if (restart)
                {
                    _timer.Start();
                }
            }
        }

        #endregion

        #region ViewModel event handlers

        private void ViewModel_NewGame(object? sender, EventArgs e)
        {
            _model.NewGame();
            _timer.Start();
        }

        private async void ViewModel_LoadGame(object? sender, EventArgs e)
        {
            bool restart= _timer.IsEnabled;
            _timer.Stop();

            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Load reversi game";
                openFileDialog.Filter = "Reversi board|*stl;";
                if (openFileDialog.ShowDialog() == true)
                {
                    await _model.LoadGameAsync(openFileDialog.FileName);

                    _timer.Start();
                }
            }
            catch (ReversiDataException)
            {
                MessageBox.Show("Error loading game.", "Reversi", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (restart)
            {
                _timer.Start();
            }
        }

        private async void ViewModel_SaveGame(object? sender, EventArgs e)
        {
            bool restart = _timer.IsEnabled;
            _timer.Stop();

            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Save reversi game";
                saveFileDialog.Filter = "Reversi board|*stl;";
                if (saveFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        await _model.SaveGameAsync(saveFileDialog.FileName);
                    }
                    catch (ReversiDataException)
                    {
                        MessageBox.Show("Error saving game.", "Reversi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            } catch
            {
                MessageBox.Show("Error saving game.", "Reversi", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (restart) {                 _timer.Start();
                       }
        }

        private void ViewModel_ExitGame(object? sender, EventArgs e)
        {
            _view.Close();
        }

        #endregion

        #region Model event handlers

        private void Model_GameOver(object? sender, ReversiEventArgs e)
        {
            _timer.Stop();

            if (e.IsWon)
            {
                Int32 winner = _model.GetWinner();
                if (winner == 0)
                {
                    MessageBox.Show("Draw!" + Environment.NewLine +
                    "Player 1 " + TimeSpan.FromSeconds(e.GameTurnCount).ToString("g") + " play time." + Environment.NewLine +
                    "Player 2 " + TimeSpan.FromSeconds(e.GameTime).ToString("g") + " play time.",
                    "Reversi game",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Asterisk);
                }
                else
                {
                    MessageBox.Show($"Player {winner} won!" + Environment.NewLine +
                    "Player 1 " + TimeSpan.FromSeconds(e.GameTurnCount).ToString("g") + " play time." + Environment.NewLine +
                    "Player 2 " + TimeSpan.FromSeconds(e.GameTime).ToString("g") + " play time.",
                    "Reversi game",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Asterisk);
                }
            }
        }

        #endregion
    }
}
