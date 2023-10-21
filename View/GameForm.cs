using Model;
using Persistence;
using System;
using System.Diagnostics.Eventing.Reader;
using System.IO.Packaging;
using System.Runtime.Versioning;
using System.Security.AccessControl;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace View
{
    public partial class GameForm : Form
    {

        #region fields

        private IReversiDataAccess dataAccess = null!;
        private ReversiGameModel model = null!;
        private Button[,] buttonGrid = null!;
        private Timer timer = null!;

        #endregion
        public GameForm()
        {
            InitializeComponent();

            dataAccess = new ReversiFileDataAccess();
            model = new ReversiGameModel(dataAccess);
            model.FieldChanged += new EventHandler<ReversiFieldEventArgs>(Game_FieldChanged);
            model.GameAdvanced += new EventHandler<ReversiEventArgs>(Game_GameAdvanced);
            model.GameOver += new EventHandler<ReversiEventArgs>(Game_GameOver);

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(Timer_Tick);

            GenerateTable();

            model.NewGame();
            SetupTable();
            SetupMenus();

            timer.Start();
        }

        #region timer event handlers

        private void Timer_Tick(Object? sender, EventArgs e)
        {
            model.AdvanceTime();
        }

        #endregion

        #region grid event handlers

        private void ButtonGrid_MouseClick(Object? sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {

                Int32 x = (button.TabIndex - 100) / model.Table.Size;
                Int32 y = (button.TabIndex - 100) % model.Table.Size;

                model.Step(x, y);

            }
        }

        #endregion

        #region private methods

        private void GenerateTable()
        {
            RemoveButtonGrid();
            buttonGrid = new Button[model.Table.Size, model.Table.Size];
            for (Int32 i = 0; i < model.Table.Size; i++)
                for (Int32 j = 0; j < model.Table.Size; j++)
                {
                    buttonGrid[i, j] = new Button();
                    buttonGrid[i, j].Location = new Point(5 + 50 * j, 35 + 50 * i);
                    buttonGrid[i, j].Size = new Size(50, 50);
                    buttonGrid[i, j].Font = new Font(FontFamily.GenericSansSerif, 25, FontStyle.Bold);
                    buttonGrid[i, j].Enabled = false;
                    buttonGrid[i, j].TabIndex = 100 + i * model.Table.Size + j;
                    buttonGrid[i, j].FlatStyle = FlatStyle.Flat;
                    buttonGrid[i, j].FlatAppearance.BorderColor = Color.Black;
                    buttonGrid[i, j].FlatAppearance.BorderSize = 1;
                    buttonGrid[i, j].BackColor = Color.Green;
                    buttonGrid[i, j].MouseClick += new MouseEventHandler(ButtonGrid_MouseClick);
                    buttonGrid[i, j].Margin = new Padding(0, 0, 5, 35);
                    Controls.Add(buttonGrid[i, j]);
                }
        }

        private void RemoveButtonGrid()
        {
            if (buttonGrid == null) return;
            for (Int32 i = 0; i < buttonGrid.GetLength(0); i++)
            {
                for (Int32 j = 0; j < buttonGrid.GetLength(1); j++)
                {
                    Controls.Remove(buttonGrid[i, j]);
                    buttonGrid[i, j].Dispose();
                }
            }
        }


        private void SetupTable()
        {
            for (Int32 i = 0; i < buttonGrid.GetLength(0); i++)
            {
                for (Int32 j = 0; j < buttonGrid.GetLength(0); j++)
                {
                    if (model.Table.GetValue(i, j) == 1)
                    {
                        buttonGrid[i, j].Enabled = false;
                        buttonGrid[i, j].BackColor = Color.White;

                    }
                    else if (model.Table.GetValue(i, j) == 2)
                    {
                        buttonGrid[i, j].Enabled = false;
                        buttonGrid[i, j].BackColor = Color.Black;
                    }
                    else
                    {
                        buttonGrid[i, j].Enabled = true;
                        buttonGrid[i, j].BackColor = Color.Green;
                    }

                }
            }

        }

        #endregion

        #region game event handlers

        private void Game_FieldChanged(object? sender, ReversiFieldEventArgs e)
        {
            if (model.Table.GetValue(e.X, e.Y) == 0)
            {
                buttonGrid[e.X, e.Y].BackColor = Color.Green;
                buttonGrid[e.X, e.Y].Enabled = true;
            }
            else if (model.Table.GetValue(e.X, e.Y) == 1)
            {
                buttonGrid[e.X, e.Y].BackColor = Color.White;
                buttonGrid[e.X, e.Y].Enabled = false;
            }
            else
            {
                buttonGrid[e.X, e.Y].BackColor = Color.Black;
                buttonGrid[e.X, e.Y].Enabled = false;
            }
        }

        private void Game_GameAdvanced(Object? sender, ReversiEventArgs e)
        {
            toolStripStatusLabel2.Text = model.CurrentPlayer == 1 ? "White" : "Black";
            toolStripStatusLabel4.Text = TimeSpan.FromSeconds(e.GameTime).ToString("g");
        }

        private void Game_GameOver(Object? sender, ReversiEventArgs e)
        {
            timer.Stop();

            foreach (Button button in buttonGrid)
                button.Enabled = false;

            if (e.IsWon)
            {
                Int32 winner = model.GetWinner();
                if (winner == 0)
                {
                    MessageBox.Show("Draw!" + Environment.NewLine +
                    "Player 1 " + TimeSpan.FromSeconds(e.GameTurnCount).ToString("g") + " play time." + Environment.NewLine +
                    "Player 2 " + TimeSpan.FromSeconds(e.GameTime).ToString("g") + " play time.",
                    "Reversi game",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show($"Player {winner} won!" + Environment.NewLine +
                    "Player 1 " + TimeSpan.FromSeconds(e.GameTurnCount).ToString("g") + " play time." + Environment.NewLine +
                    "Player 2 " + TimeSpan.FromSeconds(e.GameTime).ToString("g") + " play time.",
                    "Reversi game",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                }

            }
        }

        #endregion

        #region menu event handlers


        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            model.NewGame();
            GenerateTable();
            SetupTable();

            timer.Start();
        }

        private void smolGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            model.BoardSize = BoardSize.Smol;
            SetupMenus();
        }

        private void mediumGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            model.BoardSize = BoardSize.Medium;
            SetupMenus();
        }

        private void bigGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            model.BoardSize = BoardSize.Big;
            SetupMenus();
        }

        private void SetupMenus()
        {
            smolGameToolStripMenuItem.Checked = (model.BoardSize == BoardSize.Smol);
            mediumGameToolStripMenuItem.Checked = (model.BoardSize == BoardSize.Medium);
            bigGameToolStripMenuItem.Checked = (model.BoardSize == BoardSize.Big);
        }

        private async void saveGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await model.SaveGameAsync(saveFileDialog1.FileName);
                }
                catch (ReversiDataException)
                {
                    MessageBox.Show("Saving game failed!" + Environment.NewLine + "Wrong path or permissions.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Boolean restartTimer = timer.Enabled;
            timer.Stop();

            if (MessageBox.Show("Are you sure you want to exit?", "Reversi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
            }
            else
            {
                if (restartTimer)
                    timer.Start();
            }
        }

        private async void loadGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Boolean restartTimer = timer.Enabled;
            timer.Stop();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await model.LoadGameAsync(openFileDialog1.FileName);
                }
                catch (ReversiDataException)
                {
                    MessageBox.Show("Loading game failed!" + Environment.NewLine + "Wrong path or file", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    model.NewGame();
                }
                GenerateTable();
                SetupTable();
            }

            if (restartTimer)
                timer.Start();
        }

        #endregion
    }
}