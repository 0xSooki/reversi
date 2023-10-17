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
            //model.FieldChanged += new EventHandler<ReversiFieldEventArgs>(Game_FieldChanged);

            model = new ReversiGameModel(dataAccess);

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(Timer_Tick);

            GenerateTable();

            model.NewGame();
            SetupTable();
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

                    Controls.Add(buttonGrid[i, j]);
                }
        }

        private void SetupTable()
        {
            for (Int32 i = 0; i < buttonGrid.GetLength(0); i++)
            {
                for (Int32 j = 0; j < buttonGrid.GetLength(1); j++)
                {
                    if (model.Table.GetValue(i, j) == 1)
                    {
                        buttonGrid[i, j].Enabled = false;
                        buttonGrid[i, j].BackColor = Color.White;

                    } else if (model.Table.GetValue(i, j) == 2)
                    {
                        buttonGrid[i, j].Enabled = false;
                        buttonGrid[i, j].BackColor = Color.Black;
                    }
                    else
                    {
                        buttonGrid[i, j].Text = String.Empty;
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
                buttonGrid[e.X, e.Y].Text = String.Empty;
            else
                buttonGrid[e.X, e.Y].Text = model.Table[e.X, e.Y].ToString();
        }

        #endregion

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}