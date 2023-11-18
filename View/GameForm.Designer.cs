namespace View
{
    partial class GameForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            newGameToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            loadGameToolStripMenuItem = new ToolStripMenuItem();
            saveGameToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            smolGameToolStripMenuItem = new ToolStripMenuItem();
            mediumGameToolStripMenuItem = new ToolStripMenuItem();
            bigGameToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            toolStripStatusLabel3 = new ToolStripStatusLabel();
            toolStripStatusLabel4 = new ToolStripStatusLabel();
            toolStripStatusLabel5 = new ToolStripStatusLabel();
            toolStripStatusLabel6 = new ToolStripStatusLabel();
            saveFileDialog1 = new SaveFileDialog();
            openFileDialog1 = new OpenFileDialog();
            pauseToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, settingsToolStripMenuItem, pauseToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(4, 1, 0, 1);
            menuStrip1.Size = new Size(578, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { newGameToolStripMenuItem, toolStripSeparator2, loadGameToolStripMenuItem, saveGameToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(37, 22);
            toolStripMenuItem1.Text = "File";
            // 
            // newGameToolStripMenuItem
            // 
            newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            newGameToolStripMenuItem.Size = new Size(133, 22);
            newGameToolStripMenuItem.Text = "New game";
            newGameToolStripMenuItem.Click += newGameToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(130, 6);
            // 
            // loadGameToolStripMenuItem
            // 
            loadGameToolStripMenuItem.Name = "loadGameToolStripMenuItem";
            loadGameToolStripMenuItem.Size = new Size(133, 22);
            loadGameToolStripMenuItem.Text = "Load game";
            loadGameToolStripMenuItem.Click += loadGameToolStripMenuItem_Click;
            // 
            // saveGameToolStripMenuItem
            // 
            saveGameToolStripMenuItem.Name = "saveGameToolStripMenuItem";
            saveGameToolStripMenuItem.Size = new Size(133, 22);
            saveGameToolStripMenuItem.Text = "Save game";
            saveGameToolStripMenuItem.Click += saveGameToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(130, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(133, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Checked = true;
            settingsToolStripMenuItem.CheckState = CheckState.Checked;
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { smolGameToolStripMenuItem, mediumGameToolStripMenuItem, bigGameToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(61, 22);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // smolGameToolStripMenuItem
            // 
            smolGameToolStripMenuItem.Name = "smolGameToolStripMenuItem";
            smolGameToolStripMenuItem.Size = new Size(152, 22);
            smolGameToolStripMenuItem.Text = "Smol game";
            smolGameToolStripMenuItem.Click += smolGameToolStripMenuItem_Click;
            // 
            // mediumGameToolStripMenuItem
            // 
            mediumGameToolStripMenuItem.Name = "mediumGameToolStripMenuItem";
            mediumGameToolStripMenuItem.Size = new Size(152, 22);
            mediumGameToolStripMenuItem.Text = "Medium game";
            mediumGameToolStripMenuItem.Click += mediumGameToolStripMenuItem_Click;
            // 
            // bigGameToolStripMenuItem
            // 
            bigGameToolStripMenuItem.Name = "bigGameToolStripMenuItem";
            bigGameToolStripMenuItem.Size = new Size(152, 22);
            bigGameToolStripMenuItem.Text = "Big game";
            bigGameToolStripMenuItem.Click += bigGameToolStripMenuItem_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2, toolStripStatusLabel3, toolStripStatusLabel4, toolStripStatusLabel5, toolStripStatusLabel6 });
            statusStrip1.Location = new Point(0, 248);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 10, 0);
            statusStrip1.Size = new Size(578, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(50, 17);
            toolStripStatusLabel1.Text = "Current:";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(38, 17);
            toolStripStatusLabel2.Text = "White";
            // 
            // toolStripStatusLabel3
            // 
            toolStripStatusLabel3.ImageAlign = ContentAlignment.BottomCenter;
            toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            toolStripStatusLabel3.Padding = new Padding(1, 0, 0, 0);
            toolStripStatusLabel3.Size = new Size(52, 17);
            toolStripStatusLabel3.Text = "Player 1:";
            // 
            // toolStripStatusLabel4
            // 
            toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            toolStripStatusLabel4.Size = new Size(43, 17);
            toolStripStatusLabel4.Text = "0:00:00";
            // 
            // toolStripStatusLabel5
            // 
            toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            toolStripStatusLabel5.Size = new Size(51, 17);
            toolStripStatusLabel5.Text = "Player 2:";
            // 
            // toolStripStatusLabel6
            // 
            toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            toolStripStatusLabel6.Size = new Size(43, 17);
            toolStripStatusLabel6.Text = "0:00:00";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // pauseToolStripMenuItem
            // 
            pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            pauseToolStripMenuItem.Size = new Size(50, 22);
            pauseToolStripMenuItem.Text = "Pause";
            pauseToolStripMenuItem.Click += pauseToolStripMenuItem_Click;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(578, 270);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(2);
            MaximizeBox = false;
            Name = "GameForm";
            Text = "Reversi game";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem newGameToolStripMenuItem;
        private ToolStripMenuItem loadGameToolStripMenuItem;
        private ToolStripMenuItem saveGameToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem smolGameToolStripMenuItem;
        private ToolStripMenuItem mediumGameToolStripMenuItem;
        private ToolStripMenuItem bigGameToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripStatusLabel toolStripStatusLabel3;
        private ToolStripStatusLabel toolStripStatusLabel4;
        private SaveFileDialog saveFileDialog1;
        private OpenFileDialog openFileDialog1;
        private ToolStripStatusLabel toolStripStatusLabel5;
        private ToolStripStatusLabel toolStripStatusLabel6;
        private ToolStripMenuItem pauseToolStripMenuItem;
    }
}