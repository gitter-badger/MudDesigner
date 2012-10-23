namespace MudDesigner.Editor
{
    partial class Editor
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Barrel");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Plant");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Decoration", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Counter");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Couch");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Bookshelf");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Bed");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Furniture", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("NPC");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Animal");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Mobs", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10});
            this.EditorStatus = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusSelectedObject = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.worldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newRealmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newZoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newRoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editorContainer = new System.Windows.Forms.SplitContainer();
            this.objectBrowser = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.RoomEditor_Properties = new System.Windows.Forms.SplitContainer();
            this.lblRoomName = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabStaticGameObjects = new System.Windows.Forms.TabPage();
            this.tabUsableGameObjects = new System.Windows.Forms.TabPage();
            this.tabMobs = new System.Windows.Forms.TabPage();
            this.tabScripts = new System.Windows.Forms.TabPage();
            this.West = new System.Windows.Forms.Button();
            this.contextRooms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuClearRoom = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuLoadRoom = new System.Windows.Forms.ToolStripMenuItem();
            this.East = new System.Windows.Forms.Button();
            this.South = new System.Windows.Forms.Button();
            this.North = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.GameExplorer = new System.Windows.Forms.TabControl();
            this.EnvironmentPage = new System.Windows.Forms.TabPage();
            this.EnvironmentOptions = new System.Windows.Forms.TabControl();
            this.RealmTab = new System.Windows.Forms.TabPage();
            this.AvailableRealms = new System.Windows.Forms.ListBox();
            this.contextEnvironmentMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuDeleteEnvironmentItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ZoneTab = new System.Windows.Forms.TabPage();
            this.AvailableZones = new System.Windows.Forms.ListBox();
            this.RoomTab = new System.Windows.Forms.TabPage();
            this.AvailableRooms = new System.Windows.Forms.ListBox();
            this.tabStaticObjects = new System.Windows.Forms.TabPage();
            this.treeStaticObjects = new System.Windows.Forms.TreeView();
            this.SelectedObjectLabel = new System.Windows.Forms.Label();
            this.objectProperties = new System.Windows.Forms.PropertyGrid();
            this.EditorStatus.SuspendLayout();
            this.MainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editorContainer)).BeginInit();
            this.editorContainer.Panel1.SuspendLayout();
            this.editorContainer.Panel2.SuspendLayout();
            this.editorContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RoomEditor_Properties)).BeginInit();
            this.RoomEditor_Properties.Panel1.SuspendLayout();
            this.RoomEditor_Properties.Panel2.SuspendLayout();
            this.RoomEditor_Properties.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.contextRooms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.GameExplorer.SuspendLayout();
            this.EnvironmentPage.SuspendLayout();
            this.EnvironmentOptions.SuspendLayout();
            this.RealmTab.SuspendLayout();
            this.contextEnvironmentMenu.SuspendLayout();
            this.ZoneTab.SuspendLayout();
            this.RoomTab.SuspendLayout();
            this.tabStaticObjects.SuspendLayout();
            this.SuspendLayout();
            // 
            // EditorStatus
            // 
            this.EditorStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.EditorStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.statusSelectedObject});
            this.EditorStatus.Location = new System.Drawing.Point(0, 716);
            this.EditorStatus.Name = "EditorStatus";
            this.EditorStatus.Size = new System.Drawing.Size(1008, 24);
            this.EditorStatus.TabIndex = 0;
            this.EditorStatus.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(61, 19);
            this.toolStripStatusLabel1.Text = "Not Saved";
            // 
            // statusSelectedObject
            // 
            this.statusSelectedObject.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusSelectedObject.ForeColor = System.Drawing.Color.White;
            this.statusSelectedObject.Name = "statusSelectedObject";
            this.statusSelectedObject.Size = new System.Drawing.Size(104, 19);
            this.statusSelectedObject.Text = "Nothing selected.";
            // 
            // MainMenu
            // 
            this.MainMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.worldToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(1008, 24);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.saveToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.exitToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // worldToolStripMenuItem
            // 
            this.worldToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newRealmToolStripMenuItem,
            this.newZoneToolStripMenuItem,
            this.newRoomToolStripMenuItem});
            this.worldToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.worldToolStripMenuItem.Name = "worldToolStripMenuItem";
            this.worldToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.worldToolStripMenuItem.Text = "Environment";
            // 
            // newRealmToolStripMenuItem
            // 
            this.newRealmToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.newRealmToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.newRealmToolStripMenuItem.Name = "newRealmToolStripMenuItem";
            this.newRealmToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.W)));
            this.newRealmToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.newRealmToolStripMenuItem.Text = "New Realm";
            this.newRealmToolStripMenuItem.Click += new System.EventHandler(this.newRealmToolStripMenuItem_Click);
            // 
            // newZoneToolStripMenuItem
            // 
            this.newZoneToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.newZoneToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.newZoneToolStripMenuItem.Name = "newZoneToolStripMenuItem";
            this.newZoneToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Z)));
            this.newZoneToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.newZoneToolStripMenuItem.Text = "New Zone";
            this.newZoneToolStripMenuItem.Click += new System.EventHandler(this.newZoneToolStripMenuItem_Click);
            // 
            // newRoomToolStripMenuItem
            // 
            this.newRoomToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.newRoomToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.newRoomToolStripMenuItem.Name = "newRoomToolStripMenuItem";
            this.newRoomToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R)));
            this.newRoomToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.newRoomToolStripMenuItem.Text = "New Room";
            this.newRoomToolStripMenuItem.Click += new System.EventHandler(this.newRoomToolStripMenuItem_Click);
            // 
            // editorContainer
            // 
            this.editorContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.editorContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.editorContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.editorContainer.Location = new System.Drawing.Point(0, 24);
            this.editorContainer.Name = "editorContainer";
            // 
            // editorContainer.Panel1
            // 
            this.editorContainer.Panel1.Controls.Add(this.objectBrowser);
            this.editorContainer.Panel1.Controls.Add(this.panel1);
            // 
            // editorContainer.Panel2
            // 
            this.editorContainer.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.editorContainer.Panel2.Controls.Add(this.RoomEditor_Properties);
            this.editorContainer.Size = new System.Drawing.Size(1008, 692);
            this.editorContainer.SplitterDistance = 200;
            this.editorContainer.SplitterIncrement = 5;
            this.editorContainer.SplitterWidth = 5;
            this.editorContainer.TabIndex = 2;
            this.editorContainer.TabStop = false;
            // 
            // objectBrowser
            // 
            this.objectBrowser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.objectBrowser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.objectBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectBrowser.ForeColor = System.Drawing.Color.White;
            this.objectBrowser.FormattingEnabled = true;
            this.objectBrowser.Location = new System.Drawing.Point(0, 15);
            this.objectBrowser.Name = "objectBrowser";
            this.objectBrowser.Size = new System.Drawing.Size(196, 673);
            this.objectBrowser.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(196, 15);
            this.panel1.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.textBox1.Location = new System.Drawing.Point(77, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(119, 13);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Search Toolbox";
            this.textBox1.WordWrap = false;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Toolbox";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RoomEditor_Properties
            // 
            this.RoomEditor_Properties.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.RoomEditor_Properties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RoomEditor_Properties.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.RoomEditor_Properties.IsSplitterFixed = true;
            this.RoomEditor_Properties.Location = new System.Drawing.Point(0, 0);
            this.RoomEditor_Properties.MinimumSize = new System.Drawing.Size(0, 680);
            this.RoomEditor_Properties.Name = "RoomEditor_Properties";
            // 
            // RoomEditor_Properties.Panel1
            // 
            this.RoomEditor_Properties.Panel1.Controls.Add(this.lblRoomName);
            this.RoomEditor_Properties.Panel1.Controls.Add(this.tabControl1);
            this.RoomEditor_Properties.Panel1.Controls.Add(this.West);
            this.RoomEditor_Properties.Panel1.Controls.Add(this.East);
            this.RoomEditor_Properties.Panel1.Controls.Add(this.South);
            this.RoomEditor_Properties.Panel1.Controls.Add(this.North);
            // 
            // RoomEditor_Properties.Panel2
            // 
            this.RoomEditor_Properties.Panel2.Controls.Add(this.splitContainer2);
            this.RoomEditor_Properties.Size = new System.Drawing.Size(803, 692);
            this.RoomEditor_Properties.SplitterDistance = 485;
            this.RoomEditor_Properties.SplitterIncrement = 2;
            this.RoomEditor_Properties.SplitterWidth = 5;
            this.RoomEditor_Properties.TabIndex = 0;
            // 
            // lblRoomName
            // 
            this.lblRoomName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRoomName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoomName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblRoomName.Location = new System.Drawing.Point(0, 0);
            this.lblRoomName.Name = "lblRoomName";
            this.lblRoomName.Size = new System.Drawing.Size(481, 23);
            this.lblRoomName.TabIndex = 5;
            this.lblRoomName.Text = "No Room Loaded";
            this.lblRoomName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabStaticGameObjects);
            this.tabControl1.Controls.Add(this.tabUsableGameObjects);
            this.tabControl1.Controls.Add(this.tabMobs);
            this.tabControl1.Controls.Add(this.tabScripts);
            this.tabControl1.Location = new System.Drawing.Point(3, 387);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(475, 301);
            this.tabControl1.TabIndex = 4;
            // 
            // tabStaticGameObjects
            // 
            this.tabStaticGameObjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.tabStaticGameObjects.ForeColor = System.Drawing.Color.White;
            this.tabStaticGameObjects.Location = new System.Drawing.Point(4, 25);
            this.tabStaticGameObjects.Name = "tabStaticGameObjects";
            this.tabStaticGameObjects.Size = new System.Drawing.Size(467, 272);
            this.tabStaticGameObjects.TabIndex = 0;
            this.tabStaticGameObjects.Text = "Static Game Objects";
            // 
            // tabUsableGameObjects
            // 
            this.tabUsableGameObjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.tabUsableGameObjects.ForeColor = System.Drawing.Color.White;
            this.tabUsableGameObjects.Location = new System.Drawing.Point(4, 25);
            this.tabUsableGameObjects.Name = "tabUsableGameObjects";
            this.tabUsableGameObjects.Size = new System.Drawing.Size(467, 268);
            this.tabUsableGameObjects.TabIndex = 2;
            this.tabUsableGameObjects.Text = "Usable Game Objects";
            // 
            // tabMobs
            // 
            this.tabMobs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.tabMobs.ForeColor = System.Drawing.Color.White;
            this.tabMobs.Location = new System.Drawing.Point(4, 25);
            this.tabMobs.Name = "tabMobs";
            this.tabMobs.Size = new System.Drawing.Size(467, 268);
            this.tabMobs.TabIndex = 1;
            this.tabMobs.Text = "Mobs";
            // 
            // tabScripts
            // 
            this.tabScripts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.tabScripts.ForeColor = System.Drawing.Color.White;
            this.tabScripts.Location = new System.Drawing.Point(4, 25);
            this.tabScripts.Name = "tabScripts";
            this.tabScripts.Size = new System.Drawing.Size(467, 268);
            this.tabScripts.TabIndex = 3;
            this.tabScripts.Text = "Scripts";
            // 
            // West
            // 
            this.West.AllowDrop = true;
            this.West.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.West.ContextMenuStrip = this.contextRooms;
            this.West.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.West.FlatAppearance.BorderSize = 2;
            this.West.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.West.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.West.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.West.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.West.Location = new System.Drawing.Point(2, 155);
            this.West.Name = "West";
            this.West.Size = new System.Drawing.Size(160, 90);
            this.West.TabIndex = 3;
            this.West.Text = "West\r\n(Empty)";
            this.West.UseVisualStyleBackColor = false;
            this.West.DragDrop += new System.Windows.Forms.DragEventHandler(this.Room_DragDrop);
            this.West.DragOver += new System.Windows.Forms.DragEventHandler(this.Room_DragOver);
            // 
            // contextRooms
            // 
            this.contextRooms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.contextRooms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuClearRoom,
            this.toolStripMenuItem1,
            this.mnuLoadRoom});
            this.contextRooms.Name = "contextMenuLoad";
            this.contextRooms.ShowImageMargin = false;
            this.contextRooms.Size = new System.Drawing.Size(127, 54);
            // 
            // mnuClearRoom
            // 
            this.mnuClearRoom.ForeColor = System.Drawing.Color.White;
            this.mnuClearRoom.Name = "mnuClearRoom";
            this.mnuClearRoom.Size = new System.Drawing.Size(126, 22);
            this.mnuClearRoom.Text = "Clear Doorway";
            this.mnuClearRoom.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(123, 6);
            // 
            // mnuLoadRoom
            // 
            this.mnuLoadRoom.ForeColor = System.Drawing.Color.White;
            this.mnuLoadRoom.Name = "mnuLoadRoom";
            this.mnuLoadRoom.Size = new System.Drawing.Size(126, 22);
            this.mnuLoadRoom.Text = "Load Room";
            this.mnuLoadRoom.Click += new System.EventHandler(this.loadRoomToolStripMenuItem_Click);
            // 
            // East
            // 
            this.East.AllowDrop = true;
            this.East.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.East.ContextMenuStrip = this.contextRooms;
            this.East.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.East.FlatAppearance.BorderSize = 2;
            this.East.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.East.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.East.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.East.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.East.Location = new System.Drawing.Point(317, 155);
            this.East.Name = "East";
            this.East.Size = new System.Drawing.Size(160, 90);
            this.East.TabIndex = 2;
            this.East.Text = "East\r\n(Empty)";
            this.East.UseVisualStyleBackColor = false;
            this.East.DragDrop += new System.Windows.Forms.DragEventHandler(this.Room_DragDrop);
            this.East.DragOver += new System.Windows.Forms.DragEventHandler(this.Room_DragOver);
            // 
            // South
            // 
            this.South.AllowDrop = true;
            this.South.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.South.ContextMenuStrip = this.contextRooms;
            this.South.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.South.FlatAppearance.BorderSize = 2;
            this.South.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.South.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.South.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.South.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.South.Location = new System.Drawing.Point(159, 287);
            this.South.Name = "South";
            this.South.Size = new System.Drawing.Size(160, 90);
            this.South.TabIndex = 1;
            this.South.Text = "South\r\n(Empty)";
            this.South.UseVisualStyleBackColor = false;
            this.South.DragDrop += new System.Windows.Forms.DragEventHandler(this.Room_DragDrop);
            this.South.DragOver += new System.Windows.Forms.DragEventHandler(this.Room_DragOver);
            // 
            // North
            // 
            this.North.AllowDrop = true;
            this.North.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.North.ContextMenuStrip = this.contextRooms;
            this.North.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.North.FlatAppearance.BorderSize = 2;
            this.North.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.North.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.North.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.North.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.North.Location = new System.Drawing.Point(159, 25);
            this.North.Name = "North";
            this.North.Size = new System.Drawing.Size(160, 90);
            this.North.TabIndex = 0;
            this.North.Text = "North\r\n(Empty)";
            this.North.UseVisualStyleBackColor = false;
            this.North.DragDrop += new System.Windows.Forms.DragEventHandler(this.Room_DragDrop);
            this.North.DragOver += new System.Windows.Forms.DragEventHandler(this.Room_DragOver);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.GameExplorer);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.SelectedObjectLabel);
            this.splitContainer2.Panel2.Controls.Add(this.objectProperties);
            this.splitContainer2.Size = new System.Drawing.Size(309, 688);
            this.splitContainer2.SplitterDistance = 351;
            this.splitContainer2.TabIndex = 0;
            // 
            // GameExplorer
            // 
            this.GameExplorer.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.GameExplorer.Controls.Add(this.EnvironmentPage);
            this.GameExplorer.Controls.Add(this.tabStaticObjects);
            this.GameExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GameExplorer.Location = new System.Drawing.Point(0, 0);
            this.GameExplorer.Name = "GameExplorer";
            this.GameExplorer.SelectedIndex = 0;
            this.GameExplorer.Size = new System.Drawing.Size(309, 351);
            this.GameExplorer.TabIndex = 0;
            // 
            // EnvironmentPage
            // 
            this.EnvironmentPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.EnvironmentPage.Controls.Add(this.EnvironmentOptions);
            this.EnvironmentPage.ForeColor = System.Drawing.Color.White;
            this.EnvironmentPage.Location = new System.Drawing.Point(4, 25);
            this.EnvironmentPage.Name = "EnvironmentPage";
            this.EnvironmentPage.Padding = new System.Windows.Forms.Padding(3);
            this.EnvironmentPage.Size = new System.Drawing.Size(301, 322);
            this.EnvironmentPage.TabIndex = 0;
            this.EnvironmentPage.Text = "Environment";
            // 
            // EnvironmentOptions
            // 
            this.EnvironmentOptions.Controls.Add(this.RealmTab);
            this.EnvironmentOptions.Controls.Add(this.ZoneTab);
            this.EnvironmentOptions.Controls.Add(this.RoomTab);
            this.EnvironmentOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EnvironmentOptions.Location = new System.Drawing.Point(3, 3);
            this.EnvironmentOptions.Name = "EnvironmentOptions";
            this.EnvironmentOptions.SelectedIndex = 0;
            this.EnvironmentOptions.Size = new System.Drawing.Size(295, 316);
            this.EnvironmentOptions.TabIndex = 0;
            // 
            // RealmTab
            // 
            this.RealmTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.RealmTab.Controls.Add(this.AvailableRealms);
            this.RealmTab.Location = new System.Drawing.Point(4, 22);
            this.RealmTab.Name = "RealmTab";
            this.RealmTab.Padding = new System.Windows.Forms.Padding(3);
            this.RealmTab.Size = new System.Drawing.Size(287, 290);
            this.RealmTab.TabIndex = 1;
            this.RealmTab.Text = "Realms";
            // 
            // AvailableRealms
            // 
            this.AvailableRealms.AllowDrop = true;
            this.AvailableRealms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.AvailableRealms.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AvailableRealms.ContextMenuStrip = this.contextEnvironmentMenu;
            this.AvailableRealms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AvailableRealms.ForeColor = System.Drawing.Color.White;
            this.AvailableRealms.FormattingEnabled = true;
            this.AvailableRealms.Location = new System.Drawing.Point(3, 3);
            this.AvailableRealms.Name = "AvailableRealms";
            this.AvailableRealms.Size = new System.Drawing.Size(281, 284);
            this.AvailableRealms.Sorted = true;
            this.AvailableRealms.TabIndex = 1;
            this.AvailableRealms.SelectedIndexChanged += new System.EventHandler(this.AvailableRealms_SelectedIndexChanged);
            // 
            // contextEnvironmentMenu
            // 
            this.contextEnvironmentMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.contextEnvironmentMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDeleteEnvironmentItem});
            this.contextEnvironmentMenu.Name = "contextEnvironmentMenu";
            this.contextEnvironmentMenu.ShowImageMargin = false;
            this.contextEnvironmentMenu.Size = new System.Drawing.Size(157, 26);
            // 
            // mnuDeleteEnvironmentItem
            // 
            this.mnuDeleteEnvironmentItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.mnuDeleteEnvironmentItem.ForeColor = System.Drawing.Color.White;
            this.mnuDeleteEnvironmentItem.Name = "mnuDeleteEnvironmentItem";
            this.mnuDeleteEnvironmentItem.Size = new System.Drawing.Size(156, 22);
            this.mnuDeleteEnvironmentItem.Text = "Delete Selected Item";
            this.mnuDeleteEnvironmentItem.Click += new System.EventHandler(this.mnuDeleteEnvironmentItem_Click);
            // 
            // ZoneTab
            // 
            this.ZoneTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ZoneTab.Controls.Add(this.AvailableZones);
            this.ZoneTab.Location = new System.Drawing.Point(4, 22);
            this.ZoneTab.Name = "ZoneTab";
            this.ZoneTab.Size = new System.Drawing.Size(283, 287);
            this.ZoneTab.TabIndex = 2;
            this.ZoneTab.Text = "Zones";
            // 
            // AvailableZones
            // 
            this.AvailableZones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.AvailableZones.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AvailableZones.ContextMenuStrip = this.contextEnvironmentMenu;
            this.AvailableZones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AvailableZones.ForeColor = System.Drawing.Color.White;
            this.AvailableZones.FormattingEnabled = true;
            this.AvailableZones.Location = new System.Drawing.Point(0, 0);
            this.AvailableZones.Name = "AvailableZones";
            this.AvailableZones.Size = new System.Drawing.Size(283, 287);
            this.AvailableZones.Sorted = true;
            this.AvailableZones.TabIndex = 2;
            this.AvailableZones.SelectedIndexChanged += new System.EventHandler(this.AvailableZones_SelectedIndexChanged);
            // 
            // RoomTab
            // 
            this.RoomTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.RoomTab.Controls.Add(this.AvailableRooms);
            this.RoomTab.Location = new System.Drawing.Point(4, 22);
            this.RoomTab.Name = "RoomTab";
            this.RoomTab.Size = new System.Drawing.Size(283, 287);
            this.RoomTab.TabIndex = 3;
            this.RoomTab.Text = "Rooms";
            // 
            // AvailableRooms
            // 
            this.AvailableRooms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.AvailableRooms.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AvailableRooms.ContextMenuStrip = this.contextEnvironmentMenu;
            this.AvailableRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AvailableRooms.ForeColor = System.Drawing.Color.White;
            this.AvailableRooms.FormattingEnabled = true;
            this.AvailableRooms.Location = new System.Drawing.Point(0, 0);
            this.AvailableRooms.Name = "AvailableRooms";
            this.AvailableRooms.Size = new System.Drawing.Size(283, 287);
            this.AvailableRooms.Sorted = true;
            this.AvailableRooms.TabIndex = 3;
            this.AvailableRooms.SelectedIndexChanged += new System.EventHandler(this.AvailableRooms_SelectedIndexChanged);
            this.AvailableRooms.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.AvailableRooms_QueryContinueDrag);
            this.AvailableRooms.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AvailableRooms_MouseDown);
            this.AvailableRooms.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AvailableRooms_MouseMove);
            this.AvailableRooms.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AvailableRooms_MouseUp);
            // 
            // tabStaticObjects
            // 
            this.tabStaticObjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.tabStaticObjects.Controls.Add(this.treeStaticObjects);
            this.tabStaticObjects.ForeColor = System.Drawing.Color.White;
            this.tabStaticObjects.Location = new System.Drawing.Point(4, 25);
            this.tabStaticObjects.Name = "tabStaticObjects";
            this.tabStaticObjects.Padding = new System.Windows.Forms.Padding(3);
            this.tabStaticObjects.Size = new System.Drawing.Size(297, 319);
            this.tabStaticObjects.TabIndex = 1;
            this.tabStaticObjects.Text = "Static Object";
            // 
            // treeStaticObjects
            // 
            this.treeStaticObjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.treeStaticObjects.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeStaticObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeStaticObjects.ForeColor = System.Drawing.Color.White;
            this.treeStaticObjects.Location = new System.Drawing.Point(3, 3);
            this.treeStaticObjects.Name = "treeStaticObjects";
            treeNode1.Name = "Node4";
            treeNode1.Text = "Barrel";
            treeNode2.Name = "Node5";
            treeNode2.Text = "Plant";
            treeNode3.Name = "Node0";
            treeNode3.Text = "Decoration";
            treeNode4.Name = "Node6";
            treeNode4.Text = "Counter";
            treeNode5.Name = "Node7";
            treeNode5.Text = "Couch";
            treeNode6.Name = "Node8";
            treeNode6.Text = "Bookshelf";
            treeNode7.Name = "Node9";
            treeNode7.Text = "Bed";
            treeNode8.Name = "Node1";
            treeNode8.Text = "Furniture";
            treeNode9.Name = "Node10";
            treeNode9.Text = "NPC";
            treeNode10.Name = "Node11";
            treeNode10.Text = "Animal";
            treeNode11.Name = "Node2";
            treeNode11.Text = "Mobs";
            this.treeStaticObjects.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode8,
            treeNode11});
            this.treeStaticObjects.Size = new System.Drawing.Size(291, 313);
            this.treeStaticObjects.TabIndex = 0;
            // 
            // SelectedObjectLabel
            // 
            this.SelectedObjectLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.SelectedObjectLabel.ForeColor = System.Drawing.Color.White;
            this.SelectedObjectLabel.Location = new System.Drawing.Point(0, 0);
            this.SelectedObjectLabel.Name = "SelectedObjectLabel";
            this.SelectedObjectLabel.Size = new System.Drawing.Size(309, 16);
            this.SelectedObjectLabel.TabIndex = 2;
            this.SelectedObjectLabel.Text = "Nothing Selected";
            // 
            // objectProperties
            // 
            this.objectProperties.CategoryForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.objectProperties.CategorySplitterColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.objectProperties.CommandsBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.objectProperties.CommandsForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.objectProperties.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectProperties.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.objectProperties.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.objectProperties.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.objectProperties.HelpForeColor = System.Drawing.Color.White;
            this.objectProperties.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.objectProperties.Location = new System.Drawing.Point(0, 23);
            this.objectProperties.Name = "objectProperties";
            this.objectProperties.SelectedItemWithFocusForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.objectProperties.Size = new System.Drawing.Size(309, 310);
            this.objectProperties.TabIndex = 1;
            this.objectProperties.ViewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.objectProperties.ViewBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.objectProperties.ViewForeColor = System.Drawing.Color.White;
            this.objectProperties.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.objectProperties_PropertyValueChanged);
            this.objectProperties.SelectedObjectsChanged += new System.EventHandler(this.objectProperties_SelectedObjectsChanged);
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 740);
            this.Controls.Add(this.editorContainer);
            this.Controls.Add(this.EditorStatus);
            this.Controls.Add(this.MainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.MainMenu;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(16, 768);
            this.Name = "Editor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mud Designer Editor";
            this.Load += new System.EventHandler(this.Editor_Load);
            this.EditorStatus.ResumeLayout(false);
            this.EditorStatus.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.editorContainer.Panel1.ResumeLayout(false);
            this.editorContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.editorContainer)).EndInit();
            this.editorContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.RoomEditor_Properties.Panel1.ResumeLayout(false);
            this.RoomEditor_Properties.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RoomEditor_Properties)).EndInit();
            this.RoomEditor_Properties.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.contextRooms.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.GameExplorer.ResumeLayout(false);
            this.EnvironmentPage.ResumeLayout(false);
            this.EnvironmentOptions.ResumeLayout(false);
            this.RealmTab.ResumeLayout(false);
            this.contextEnvironmentMenu.ResumeLayout(false);
            this.ZoneTab.ResumeLayout(false);
            this.RoomTab.ResumeLayout(false);
            this.tabStaticObjects.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip EditorStatus;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SplitContainer editorContainer;
        private System.Windows.Forms.SplitContainer RoomEditor_Properties;
        private System.Windows.Forms.ListBox objectBrowser;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStripMenuItem worldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newRealmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newZoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newRoomToolStripMenuItem;
        private System.Windows.Forms.TabControl GameExplorer;
        private System.Windows.Forms.TabPage EnvironmentPage;
        private System.Windows.Forms.TabPage tabStaticObjects;
        private System.Windows.Forms.Label SelectedObjectLabel;
        private System.Windows.Forms.TabControl EnvironmentOptions;
        private System.Windows.Forms.TabPage RealmTab;
        private System.Windows.Forms.TabPage ZoneTab;
        private System.Windows.Forms.TabPage RoomTab;
        private System.Windows.Forms.ListBox AvailableRealms;
        private System.Windows.Forms.ListBox AvailableZones;
        private System.Windows.Forms.ListBox AvailableRooms;
        private System.Windows.Forms.PropertyGrid objectProperties;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ContextMenuStrip contextRooms;
        private System.Windows.Forms.ToolStripMenuItem mnuClearRoom;
        private System.Windows.Forms.Button North;
        private System.Windows.Forms.Button West;
        private System.Windows.Forms.Button East;
        private System.Windows.Forms.Button South;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabStaticGameObjects;
        private System.Windows.Forms.TabPage tabMobs;
        private System.Windows.Forms.TabPage tabUsableGameObjects;
        private System.Windows.Forms.TabPage tabScripts;
        private System.Windows.Forms.ToolStripStatusLabel statusSelectedObject;
        private System.Windows.Forms.Label lblRoomName;
        private System.Windows.Forms.TreeView treeStaticObjects;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuLoadRoom;
        private System.Windows.Forms.ContextMenuStrip contextEnvironmentMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuDeleteEnvironmentItem;
    }
}

