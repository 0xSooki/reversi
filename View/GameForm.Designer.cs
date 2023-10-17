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
            settingsToolStripMenuItem = new ToolStripMenuItem();
            newGameToolStripMenuItem = new ToolStripMenuItem();
            loadGameToolStripMenuItem = new ToolStripMenuItem();
            saveGameToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripSeparator2 = new ToolStripSeparator();
            smolGameToolStripMenuItem = new ToolStripMenuItem();
            mediumGameToolStripMenuItem = new ToolStripMenuItem();
            bigGameToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            toolStripStatusLabel3 = new ToolStripStatusLabel();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, settingsToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(825, 33);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { newGameToolStripMenuItem, toolStripSeparator2, loadGameToolStripMenuItem, saveGameToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(54, 29);
            toolStripMenuItem1.Text = "File";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { smolGameToolStripMenuItem, mediumGameToolStripMenuItem, bigGameToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(92, 29);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // newGameToolStripMenuItem
            // 
            newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            newGameToolStripMenuItem.Size = new Size(270, 34);
            newGameToolStripMenuItem.Text = "New game";
            // 
            // loadGameToolStripMenuItem
            // 
            loadGameToolStripMenuItem.Name = "loadGameToolStripMenuItem";
            loadGameToolStripMenuItem.Size = new Size(270, 34);
            loadGameToolStripMenuItem.Text = "Load game";
            // 
            // saveGameToolStripMenuItem
            // 
            saveGameToolStripMenuItem.Name = "saveGameToolStripMenuItem";
            saveGameToolStripMenuItem.Size = new Size(270, 34);
            saveGameToolStripMenuItem.Text = "Save game";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(270, 34);
            exitToolStripMenuItem.Text = "Exit";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(200, 6);
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(200, 6);
            // 
            // smolGameToolStripMenuItem
            // 
            smolGameToolStripMenuItem.Name = "smolGameToolStripMenuItem";
            smolGameToolStripMenuItem.Size = new Size(270, 34);
            smolGameToolStripMenuItem.Text = "Smol game";
            // 
            // mediumGameToolStripMenuItem
            // 
            mediumGameToolStripMenuItem.Name = "mediumGameToolStripMenuItem";
            mediumGameToolStripMenuItem.Size = new Size(270, 34);
            mediumGameToolStripMenuItem.Text = "Medium game";
            // 
            // bigGameToolStripMenuItem
            // 
            bigGameToolStripMenuItem.Name = "bigGameToolStripMenuItem";
            bigGameToolStripMenuItem.Size = new Size(270, 34);
            bigGameToolStripMenuItem.Text = "Big game";
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2, toolStripStatusLabel3 });
            statusStrip1.Location = new Point(0, 418);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(825, 32);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            statusStrip1.ItemClicked += statusStrip1_ItemClicked;
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(64, 25);
            toolStripStatusLabel1.Text = "Turns: ";
            toolStripStatusLabel1.Click += toolStripStatusLabel1_Click;
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(122, 25);
            toolStripStatusLabel2.Text = "Elapsed time: ";
            // 
            // toolStripStatusLabel3
            // 
            toolStripStatusLabel3.ImageAlign = ContentAlignment.BottomCenter;
            toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            toolStripStatusLabel3.Padding = new Padding(1, 0, 0, 0);
            toolStripStatusLabel3.Size = new Size(180, 25);
            toolStripStatusLabel3.Text = "toolStripStatusLabel3";
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(825, 450);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "GameForm";
            Text = "               ";
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
    }
}