namespace MudDesigner.Editor
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripFileSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGame = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStartStopServer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWorld = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEnvironment = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRealms = new System.Windows.Forms.ToolStripMenuItem();
            this.menuZones = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRooms = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGameObjects = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItems = new System.Windows.Forms.ToolStripMenuItem();
            this.lblProjectName = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mainPropertyGame = new System.Windows.Forms.PropertyGrid();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.mainPropertyServer = new System.Windows.Forms.PropertyGrid();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.mainTxtServerInfo = new System.Windows.Forms.RichTextBox();
            this.timerLogger = new System.Windows.Forms.Timer(this.components);
            this.mainMenuStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuGame,
            this.menuWorld});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(716, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSave,
            this.toolStripFileSeparator,
            this.menuExit});
            this.menuFile.ForeColor = System.Drawing.Color.White;
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(37, 20);
            this.menuFile.Text = "File";
            // 
            // menuSave
            // 
            this.menuSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.menuSave.ForeColor = System.Drawing.Color.White;
            this.menuSave.Name = "menuSave";
            this.menuSave.Size = new System.Drawing.Size(98, 22);
            this.menuSave.Text = "Save";
            this.menuSave.Click += new System.EventHandler(this.menuSave_Click);
            // 
            // toolStripFileSeparator
            // 
            this.toolStripFileSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.toolStripFileSeparator.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.toolStripFileSeparator.Name = "toolStripFileSeparator";
            this.toolStripFileSeparator.Size = new System.Drawing.Size(95, 6);
            // 
            // menuExit
            // 
            this.menuExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.menuExit.ForeColor = System.Drawing.Color.White;
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(98, 22);
            this.menuExit.Text = "Exit";
            this.menuExit.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // menuGame
            // 
            this.menuGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.menuGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStartStopServer});
            this.menuGame.ForeColor = System.Drawing.Color.White;
            this.menuGame.Name = "menuGame";
            this.menuGame.Size = new System.Drawing.Size(50, 20);
            this.menuGame.Text = "Game";
            // 
            // menuStartStopServer
            // 
            this.menuStartStopServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.menuStartStopServer.ForeColor = System.Drawing.Color.White;
            this.menuStartStopServer.Name = "menuStartStopServer";
            this.menuStartStopServer.Size = new System.Drawing.Size(133, 22);
            this.menuStartStopServer.Text = "Start Server";
            this.menuStartStopServer.Click += new System.EventHandler(this.menuStartStopServer_Click);
            // 
            // menuWorld
            // 
            this.menuWorld.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEnvironment,
            this.menuGameObjects});
            this.menuWorld.ForeColor = System.Drawing.Color.White;
            this.menuWorld.Name = "menuWorld";
            this.menuWorld.Size = new System.Drawing.Size(51, 20);
            this.menuWorld.Text = "World";
            // 
            // menuEnvironment
            // 
            this.menuEnvironment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.menuEnvironment.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuRealms,
            this.menuZones,
            this.menuRooms});
            this.menuEnvironment.ForeColor = System.Drawing.Color.White;
            this.menuEnvironment.Name = "menuEnvironment";
            this.menuEnvironment.Size = new System.Drawing.Size(148, 22);
            this.menuEnvironment.Text = "Environments";
            // 
            // menuRealms
            // 
            this.menuRealms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.menuRealms.ForeColor = System.Drawing.Color.White;
            this.menuRealms.Name = "menuRealms";
            this.menuRealms.Size = new System.Drawing.Size(112, 22);
            this.menuRealms.Text = "Realms";
            this.menuRealms.Click += new System.EventHandler(this.menuRealms_Click);
            // 
            // menuZones
            // 
            this.menuZones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.menuZones.ForeColor = System.Drawing.Color.White;
            this.menuZones.Name = "menuZones";
            this.menuZones.Size = new System.Drawing.Size(112, 22);
            this.menuZones.Text = "Zones";
            this.menuZones.Click += new System.EventHandler(this.menuZones_Click);
            // 
            // menuRooms
            // 
            this.menuRooms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.menuRooms.ForeColor = System.Drawing.Color.White;
            this.menuRooms.Name = "menuRooms";
            this.menuRooms.Size = new System.Drawing.Size(112, 22);
            this.menuRooms.Text = "Rooms";
            this.menuRooms.Click += new System.EventHandler(this.menuRooms_Click);
            // 
            // menuGameObjects
            // 
            this.menuGameObjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.menuGameObjects.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItems});
            this.menuGameObjects.ForeColor = System.Drawing.Color.White;
            this.menuGameObjects.Name = "menuGameObjects";
            this.menuGameObjects.Size = new System.Drawing.Size(148, 22);
            this.menuGameObjects.Text = "Game Objects";
            // 
            // menuItems
            // 
            this.menuItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.menuItems.ForeColor = System.Drawing.Color.White;
            this.menuItems.Name = "menuItems";
            this.menuItems.Size = new System.Drawing.Size(103, 22);
            this.menuItems.Text = "Items";
            // 
            // lblProjectName
            // 
            this.lblProjectName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProjectName.ForeColor = System.Drawing.Color.White;
            this.lblProjectName.Location = new System.Drawing.Point(0, 24);
            this.lblProjectName.Name = "lblProjectName";
            this.lblProjectName.Size = new System.Drawing.Size(716, 23);
            this.lblProjectName.TabIndex = 1;
            this.lblProjectName.Text = "New Game";
            this.lblProjectName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mainPropertyGame);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(4, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(346, 175);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Game Settings";
            // 
            // mainPropertyGame
            // 
            this.mainPropertyGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.mainPropertyGame.CanShowVisualStyleGlyphs = false;
            this.mainPropertyGame.CategorySplitterColor = System.Drawing.Color.White;
            this.mainPropertyGame.CommandsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.mainPropertyGame.CommandsForeColor = System.Drawing.Color.White;
            this.mainPropertyGame.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.mainPropertyGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPropertyGame.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.mainPropertyGame.HelpForeColor = System.Drawing.Color.White;
            this.mainPropertyGame.HelpVisible = false;
            this.mainPropertyGame.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.mainPropertyGame.Location = new System.Drawing.Point(3, 16);
            this.mainPropertyGame.Name = "mainPropertyGame";
            this.mainPropertyGame.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.mainPropertyGame.Size = new System.Drawing.Size(340, 156);
            this.mainPropertyGame.TabIndex = 0;
            this.mainPropertyGame.ToolbarVisible = false;
            this.mainPropertyGame.ViewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.mainPropertyGame.ViewForeColor = System.Drawing.Color.White;
            this.mainPropertyGame.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.mainPropertyGame_PropertyValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.mainPropertyServer);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(365, 50);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(346, 175);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Server Settings";
            // 
            // mainPropertyServer
            // 
            this.mainPropertyServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.mainPropertyServer.CanShowVisualStyleGlyphs = false;
            this.mainPropertyServer.CategorySplitterColor = System.Drawing.Color.White;
            this.mainPropertyServer.CommandsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.mainPropertyServer.CommandsForeColor = System.Drawing.Color.White;
            this.mainPropertyServer.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.mainPropertyServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPropertyServer.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.mainPropertyServer.HelpForeColor = System.Drawing.Color.White;
            this.mainPropertyServer.HelpVisible = false;
            this.mainPropertyServer.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.mainPropertyServer.Location = new System.Drawing.Point(3, 16);
            this.mainPropertyServer.Name = "mainPropertyServer";
            this.mainPropertyServer.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.mainPropertyServer.Size = new System.Drawing.Size(340, 156);
            this.mainPropertyServer.TabIndex = 0;
            this.mainPropertyServer.ToolbarVisible = false;
            this.mainPropertyServer.ViewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.mainPropertyServer.ViewForeColor = System.Drawing.Color.White;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.mainTxtServerInfo);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(4, 231);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(707, 270);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Server Information";
            // 
            // mainTxtServerInfo
            // 
            this.mainTxtServerInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTxtServerInfo.Location = new System.Drawing.Point(3, 16);
            this.mainTxtServerInfo.Name = "mainTxtServerInfo";
            this.mainTxtServerInfo.Size = new System.Drawing.Size(701, 251);
            this.mainTxtServerInfo.TabIndex = 0;
            this.mainTxtServerInfo.Text = "";
            // 
            // timerLogger
            // 
            this.timerLogger.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(716, 513);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblProjectName);
            this.Controls.Add(this.mainMenuStrip);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.mainMenuStrip;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mud Designer Editor";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.Label lblProjectName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PropertyGrid mainPropertyGame;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PropertyGrid mainPropertyServer;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox mainTxtServerInfo;
        private System.Windows.Forms.Timer timerLogger;
        private System.Windows.Forms.ToolStripMenuItem menuSave;
        private System.Windows.Forms.ToolStripSeparator toolStripFileSeparator;
        private System.Windows.Forms.ToolStripMenuItem menuGame;
        private System.Windows.Forms.ToolStripMenuItem menuStartStopServer;
        private System.Windows.Forms.ToolStripMenuItem menuWorld;
        private System.Windows.Forms.ToolStripMenuItem menuEnvironment;
        private System.Windows.Forms.ToolStripMenuItem menuRealms;
        private System.Windows.Forms.ToolStripMenuItem menuZones;
        private System.Windows.Forms.ToolStripMenuItem menuRooms;
        private System.Windows.Forms.ToolStripMenuItem menuGameObjects;
        private System.Windows.Forms.ToolStripMenuItem menuItems;
    }
}