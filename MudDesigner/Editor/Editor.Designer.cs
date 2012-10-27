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
            this.EditorStatus = new System.Windows.Forms.StatusStrip();
            this.lblSaveStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusSelectedObject = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.worldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newRealmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newZoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newRoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextRooms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuClearRoom = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuLoadRoom = new System.Windows.Forms.ToolStripMenuItem();
            this.contextEnvironmentMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuDeleteEnvironmentItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RoomEditor_Properties = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.GameExplorer = new System.Windows.Forms.TabControl();
            this.EnvironmentPage = new System.Windows.Forms.TabPage();
            this.EnvironmentOptions = new System.Windows.Forms.TabControl();
            this.RealmTab = new System.Windows.Forms.TabPage();
            this.AvailableRealms = new System.Windows.Forms.ListBox();
            this.ZoneTab = new System.Windows.Forms.TabPage();
            this.AvailableZones = new System.Windows.Forms.ListBox();
            this.RoomTab = new System.Windows.Forms.TabPage();
            this.AvailableRooms = new System.Windows.Forms.ListBox();
            this.ObjectsPage = new System.Windows.Forms.TabPage();
            this.tabObjectEditor = new System.Windows.Forms.TabControl();
            this.tabIItem = new System.Windows.Forms.TabPage();
            this.treeStaticObjects = new System.Windows.Forms.TreeView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.MobsPage = new System.Windows.Forms.TabPage();
            this.objectProperties = new System.Windows.Forms.PropertyGrid();
            this.SelectedObjectLabel = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabRoomEditor = new System.Windows.Forms.TabPage();
            this.tabRoomObjects = new System.Windows.Forms.TabPage();
            this.lblRoomName = new System.Windows.Forms.Label();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabStaticGameObjects = new System.Windows.Forms.TabPage();
            this.tabUsableGameObjects = new System.Windows.Forms.TabPage();
            this.tabMobs = new System.Windows.Forms.TabPage();
            this.tabScripts = new System.Windows.Forms.TabPage();
            this.West = new System.Windows.Forms.Button();
            this.East = new System.Windows.Forms.Button();
            this.South = new System.Windows.Forms.Button();
            this.North = new System.Windows.Forms.Button();
            this.tabRoomMobs = new System.Windows.Forms.TabPage();
            this.EditorStatus.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.contextRooms.SuspendLayout();
            this.contextEnvironmentMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RoomEditor_Properties)).BeginInit();
            this.RoomEditor_Properties.Panel1.SuspendLayout();
            this.RoomEditor_Properties.Panel2.SuspendLayout();
            this.RoomEditor_Properties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.GameExplorer.SuspendLayout();
            this.EnvironmentPage.SuspendLayout();
            this.EnvironmentOptions.SuspendLayout();
            this.RealmTab.SuspendLayout();
            this.ZoneTab.SuspendLayout();
            this.RoomTab.SuspendLayout();
            this.ObjectsPage.SuspendLayout();
            this.tabObjectEditor.SuspendLayout();
            this.tabIItem.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabRoomEditor.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // EditorStatus
            // 
            this.EditorStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.EditorStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblSaveStatus,
            this.statusSelectedObject});
            this.EditorStatus.Location = new System.Drawing.Point(0, 716);
            this.EditorStatus.Name = "EditorStatus";
            this.EditorStatus.Size = new System.Drawing.Size(1008, 24);
            this.EditorStatus.TabIndex = 0;
            this.EditorStatus.Text = "statusStrip1";
            // 
            // lblSaveStatus
            // 
            this.lblSaveStatus.ForeColor = System.Drawing.Color.White;
            this.lblSaveStatus.Name = "lblSaveStatus";
            this.lblSaveStatus.Size = new System.Drawing.Size(61, 19);
            this.lblSaveStatus.Text = "Not Saved";
            this.lblSaveStatus.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
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
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click_1);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.saveToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.exitToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
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
            // RoomEditor_Properties
            // 
            this.RoomEditor_Properties.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.RoomEditor_Properties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RoomEditor_Properties.Location = new System.Drawing.Point(0, 24);
            this.RoomEditor_Properties.MinimumSize = new System.Drawing.Size(0, 680);
            this.RoomEditor_Properties.Name = "RoomEditor_Properties";
            // 
            // RoomEditor_Properties.Panel1
            // 
            this.RoomEditor_Properties.Panel1.Controls.Add(this.tabControl1);
            // 
            // RoomEditor_Properties.Panel2
            // 
            this.RoomEditor_Properties.Panel2.Controls.Add(this.splitContainer2);
            this.RoomEditor_Properties.Size = new System.Drawing.Size(1008, 692);
            this.RoomEditor_Properties.SplitterDistance = 675;
            this.RoomEditor_Properties.SplitterIncrement = 2;
            this.RoomEditor_Properties.SplitterWidth = 5;
            this.RoomEditor_Properties.TabIndex = 2;
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
            this.splitContainer2.Panel2.Controls.Add(this.objectProperties);
            this.splitContainer2.Panel2.Controls.Add(this.SelectedObjectLabel);
            this.splitContainer2.Size = new System.Drawing.Size(324, 688);
            this.splitContainer2.SplitterDistance = 351;
            this.splitContainer2.TabIndex = 0;
            // 
            // GameExplorer
            // 
            this.GameExplorer.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.GameExplorer.Controls.Add(this.EnvironmentPage);
            this.GameExplorer.Controls.Add(this.ObjectsPage);
            this.GameExplorer.Controls.Add(this.MobsPage);
            this.GameExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GameExplorer.Location = new System.Drawing.Point(0, 0);
            this.GameExplorer.Name = "GameExplorer";
            this.GameExplorer.SelectedIndex = 0;
            this.GameExplorer.Size = new System.Drawing.Size(324, 351);
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
            this.EnvironmentPage.Size = new System.Drawing.Size(316, 322);
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
            this.EnvironmentOptions.Size = new System.Drawing.Size(310, 316);
            this.EnvironmentOptions.TabIndex = 0;
            // 
            // RealmTab
            // 
            this.RealmTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.RealmTab.Controls.Add(this.AvailableRealms);
            this.RealmTab.Location = new System.Drawing.Point(4, 22);
            this.RealmTab.Name = "RealmTab";
            this.RealmTab.Padding = new System.Windows.Forms.Padding(3);
            this.RealmTab.Size = new System.Drawing.Size(302, 290);
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
            this.AvailableRealms.Size = new System.Drawing.Size(296, 284);
            this.AvailableRealms.Sorted = true;
            this.AvailableRealms.TabIndex = 1;
            // 
            // ZoneTab
            // 
            this.ZoneTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ZoneTab.Controls.Add(this.AvailableZones);
            this.ZoneTab.Location = new System.Drawing.Point(4, 22);
            this.ZoneTab.Name = "ZoneTab";
            this.ZoneTab.Size = new System.Drawing.Size(287, 290);
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
            this.AvailableZones.Size = new System.Drawing.Size(287, 290);
            this.AvailableZones.Sorted = true;
            this.AvailableZones.TabIndex = 2;
            // 
            // RoomTab
            // 
            this.RoomTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.RoomTab.Controls.Add(this.AvailableRooms);
            this.RoomTab.Location = new System.Drawing.Point(4, 22);
            this.RoomTab.Name = "RoomTab";
            this.RoomTab.Size = new System.Drawing.Size(287, 290);
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
            this.AvailableRooms.Size = new System.Drawing.Size(287, 290);
            this.AvailableRooms.Sorted = true;
            this.AvailableRooms.TabIndex = 3;
            // 
            // ObjectsPage
            // 
            this.ObjectsPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ObjectsPage.Controls.Add(this.tabObjectEditor);
            this.ObjectsPage.ForeColor = System.Drawing.Color.White;
            this.ObjectsPage.Location = new System.Drawing.Point(4, 25);
            this.ObjectsPage.Name = "ObjectsPage";
            this.ObjectsPage.Padding = new System.Windows.Forms.Padding(3);
            this.ObjectsPage.Size = new System.Drawing.Size(301, 322);
            this.ObjectsPage.TabIndex = 1;
            this.ObjectsPage.Text = "Objects";
            // 
            // tabObjectEditor
            // 
            this.tabObjectEditor.Controls.Add(this.tabIItem);
            this.tabObjectEditor.Controls.Add(this.tabPage2);
            this.tabObjectEditor.Controls.Add(this.tabPage1);
            this.tabObjectEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabObjectEditor.Location = new System.Drawing.Point(3, 3);
            this.tabObjectEditor.Name = "tabObjectEditor";
            this.tabObjectEditor.SelectedIndex = 0;
            this.tabObjectEditor.Size = new System.Drawing.Size(295, 316);
            this.tabObjectEditor.TabIndex = 0;
            // 
            // tabIItem
            // 
            this.tabIItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.tabIItem.Controls.Add(this.treeStaticObjects);
            this.tabIItem.Location = new System.Drawing.Point(4, 22);
            this.tabIItem.Name = "tabIItem";
            this.tabIItem.Padding = new System.Windows.Forms.Padding(3);
            this.tabIItem.Size = new System.Drawing.Size(287, 290);
            this.tabIItem.TabIndex = 0;
            this.tabIItem.Text = "Items";
            // 
            // treeStaticObjects
            // 
            this.treeStaticObjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.treeStaticObjects.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeStaticObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeStaticObjects.ForeColor = System.Drawing.Color.White;
            this.treeStaticObjects.Location = new System.Drawing.Point(3, 3);
            this.treeStaticObjects.Name = "treeStaticObjects";
            this.treeStaticObjects.Size = new System.Drawing.Size(281, 284);
            this.treeStaticObjects.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(283, 287);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Equipable";
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(283, 287);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Usable";
            // 
            // MobsPage
            // 
            this.MobsPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.MobsPage.ForeColor = System.Drawing.Color.White;
            this.MobsPage.Location = new System.Drawing.Point(4, 25);
            this.MobsPage.Name = "MobsPage";
            this.MobsPage.Size = new System.Drawing.Size(301, 322);
            this.MobsPage.TabIndex = 2;
            this.MobsPage.Text = "Mobs";
            // 
            // objectProperties
            // 
            this.objectProperties.CategoryForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.objectProperties.CategorySplitterColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.objectProperties.CommandsBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.objectProperties.CommandsForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.objectProperties.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.objectProperties.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.objectProperties.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.objectProperties.HelpForeColor = System.Drawing.Color.White;
            this.objectProperties.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.objectProperties.Location = new System.Drawing.Point(0, 23);
            this.objectProperties.Name = "objectProperties";
            this.objectProperties.SelectedItemWithFocusForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.objectProperties.Size = new System.Drawing.Size(324, 310);
            this.objectProperties.TabIndex = 3;
            this.objectProperties.ViewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.objectProperties.ViewBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.objectProperties.ViewForeColor = System.Drawing.Color.White;
            // 
            // SelectedObjectLabel
            // 
            this.SelectedObjectLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.SelectedObjectLabel.ForeColor = System.Drawing.Color.White;
            this.SelectedObjectLabel.Location = new System.Drawing.Point(0, 0);
            this.SelectedObjectLabel.Name = "SelectedObjectLabel";
            this.SelectedObjectLabel.Size = new System.Drawing.Size(324, 16);
            this.SelectedObjectLabel.TabIndex = 2;
            this.SelectedObjectLabel.Text = "Nothing Selected";
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl1.Controls.Add(this.tabRoomEditor);
            this.tabControl1.Controls.Add(this.tabRoomObjects);
            this.tabControl1.Controls.Add(this.tabRoomMobs);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(671, 688);
            this.tabControl1.TabIndex = 0;
            // 
            // tabRoomEditor
            // 
            this.tabRoomEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.tabRoomEditor.Controls.Add(this.lblRoomName);
            this.tabRoomEditor.Controls.Add(this.tabControl2);
            this.tabRoomEditor.Controls.Add(this.West);
            this.tabRoomEditor.Controls.Add(this.East);
            this.tabRoomEditor.Controls.Add(this.South);
            this.tabRoomEditor.Controls.Add(this.North);
            this.tabRoomEditor.Location = new System.Drawing.Point(23, 4);
            this.tabRoomEditor.Name = "tabRoomEditor";
            this.tabRoomEditor.Padding = new System.Windows.Forms.Padding(3);
            this.tabRoomEditor.Size = new System.Drawing.Size(644, 680);
            this.tabRoomEditor.TabIndex = 0;
            this.tabRoomEditor.Text = "Room";
            // 
            // tabRoomObjects
            // 
            this.tabRoomObjects.Location = new System.Drawing.Point(23, 4);
            this.tabRoomObjects.Name = "tabRoomObjects";
            this.tabRoomObjects.Padding = new System.Windows.Forms.Padding(3);
            this.tabRoomObjects.Size = new System.Drawing.Size(644, 680);
            this.tabRoomObjects.TabIndex = 1;
            this.tabRoomObjects.Text = "tabPage4";
            this.tabRoomObjects.UseVisualStyleBackColor = true;
            // 
            // lblRoomName
            // 
            this.lblRoomName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRoomName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoomName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblRoomName.Location = new System.Drawing.Point(3, 3);
            this.lblRoomName.Name = "lblRoomName";
            this.lblRoomName.Size = new System.Drawing.Size(638, 23);
            this.lblRoomName.TabIndex = 11;
            this.lblRoomName.Text = "No Room Loaded";
            this.lblRoomName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl2.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl2.Controls.Add(this.tabStaticGameObjects);
            this.tabControl2.Controls.Add(this.tabUsableGameObjects);
            this.tabControl2.Controls.Add(this.tabMobs);
            this.tabControl2.Controls.Add(this.tabScripts);
            this.tabControl2.Location = new System.Drawing.Point(-1, 373);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(646, 319);
            this.tabControl2.TabIndex = 10;
            // 
            // tabStaticGameObjects
            // 
            this.tabStaticGameObjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.tabStaticGameObjects.ForeColor = System.Drawing.Color.White;
            this.tabStaticGameObjects.Location = new System.Drawing.Point(4, 25);
            this.tabStaticGameObjects.Name = "tabStaticGameObjects";
            this.tabStaticGameObjects.Size = new System.Drawing.Size(638, 290);
            this.tabStaticGameObjects.TabIndex = 0;
            this.tabStaticGameObjects.Text = "Static Game Objects";
            // 
            // tabUsableGameObjects
            // 
            this.tabUsableGameObjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.tabUsableGameObjects.ForeColor = System.Drawing.Color.White;
            this.tabUsableGameObjects.Location = new System.Drawing.Point(4, 25);
            this.tabUsableGameObjects.Name = "tabUsableGameObjects";
            this.tabUsableGameObjects.Size = new System.Drawing.Size(480, 290);
            this.tabUsableGameObjects.TabIndex = 2;
            this.tabUsableGameObjects.Text = "Usable Game Objects";
            // 
            // tabMobs
            // 
            this.tabMobs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.tabMobs.ForeColor = System.Drawing.Color.White;
            this.tabMobs.Location = new System.Drawing.Point(4, 25);
            this.tabMobs.Name = "tabMobs";
            this.tabMobs.Size = new System.Drawing.Size(480, 290);
            this.tabMobs.TabIndex = 1;
            this.tabMobs.Text = "Mobs";
            // 
            // tabScripts
            // 
            this.tabScripts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.tabScripts.ForeColor = System.Drawing.Color.White;
            this.tabScripts.Location = new System.Drawing.Point(4, 25);
            this.tabScripts.Name = "tabScripts";
            this.tabScripts.Size = new System.Drawing.Size(480, 290);
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
            this.West.Location = new System.Drawing.Point(-2, 141);
            this.West.Name = "West";
            this.West.Size = new System.Drawing.Size(160, 90);
            this.West.TabIndex = 9;
            this.West.Text = "West\r\n(Empty)";
            this.West.UseVisualStyleBackColor = false;
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
            this.East.Location = new System.Drawing.Point(313, 141);
            this.East.Name = "East";
            this.East.Size = new System.Drawing.Size(160, 90);
            this.East.TabIndex = 8;
            this.East.Text = "East\r\n(Empty)";
            this.East.UseVisualStyleBackColor = false;
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
            this.South.Location = new System.Drawing.Point(155, 273);
            this.South.Name = "South";
            this.South.Size = new System.Drawing.Size(160, 90);
            this.South.TabIndex = 7;
            this.South.Text = "South\r\n(Empty)";
            this.South.UseVisualStyleBackColor = false;
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
            this.North.Location = new System.Drawing.Point(155, 11);
            this.North.Name = "North";
            this.North.Size = new System.Drawing.Size(160, 90);
            this.North.TabIndex = 6;
            this.North.Text = "North\r\n(Empty)";
            this.North.UseVisualStyleBackColor = false;
            // 
            // tabRoomMobs
            // 
            this.tabRoomMobs.Location = new System.Drawing.Point(23, 4);
            this.tabRoomMobs.Name = "tabRoomMobs";
            this.tabRoomMobs.Size = new System.Drawing.Size(644, 680);
            this.tabRoomMobs.TabIndex = 2;
            this.tabRoomMobs.Text = "Room Mobs";
            this.tabRoomMobs.UseVisualStyleBackColor = true;
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(1008, 740);
            this.Controls.Add(this.RoomEditor_Properties);
            this.Controls.Add(this.EditorStatus);
            this.Controls.Add(this.MainMenu);
            this.ForeColor = System.Drawing.Color.White;
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
            this.contextRooms.ResumeLayout(false);
            this.contextEnvironmentMenu.ResumeLayout(false);
            this.RoomEditor_Properties.Panel1.ResumeLayout(false);
            this.RoomEditor_Properties.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RoomEditor_Properties)).EndInit();
            this.RoomEditor_Properties.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.GameExplorer.ResumeLayout(false);
            this.EnvironmentPage.ResumeLayout(false);
            this.EnvironmentOptions.ResumeLayout(false);
            this.RealmTab.ResumeLayout(false);
            this.ZoneTab.ResumeLayout(false);
            this.RoomTab.ResumeLayout(false);
            this.ObjectsPage.ResumeLayout(false);
            this.tabObjectEditor.ResumeLayout(false);
            this.tabIItem.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabRoomEditor.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip EditorStatus;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem worldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newRealmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newZoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newRoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lblSaveStatus;
        private System.Windows.Forms.ContextMenuStrip contextRooms;
        private System.Windows.Forms.ToolStripMenuItem mnuClearRoom;
        private System.Windows.Forms.ToolStripStatusLabel statusSelectedObject;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuLoadRoom;
        private System.Windows.Forms.ContextMenuStrip contextEnvironmentMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuDeleteEnvironmentItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.SplitContainer RoomEditor_Properties;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabRoomEditor;
        private System.Windows.Forms.Label lblRoomName;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabStaticGameObjects;
        private System.Windows.Forms.TabPage tabUsableGameObjects;
        private System.Windows.Forms.TabPage tabMobs;
        private System.Windows.Forms.TabPage tabScripts;
        private System.Windows.Forms.Button West;
        private System.Windows.Forms.Button East;
        private System.Windows.Forms.Button South;
        private System.Windows.Forms.Button North;
        private System.Windows.Forms.TabPage tabRoomObjects;
        private System.Windows.Forms.TabPage tabRoomMobs;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl GameExplorer;
        private System.Windows.Forms.TabPage EnvironmentPage;
        private System.Windows.Forms.TabControl EnvironmentOptions;
        private System.Windows.Forms.TabPage RealmTab;
        private System.Windows.Forms.ListBox AvailableRealms;
        private System.Windows.Forms.TabPage ZoneTab;
        private System.Windows.Forms.ListBox AvailableZones;
        private System.Windows.Forms.TabPage RoomTab;
        private System.Windows.Forms.ListBox AvailableRooms;
        private System.Windows.Forms.TabPage ObjectsPage;
        private System.Windows.Forms.TabControl tabObjectEditor;
        private System.Windows.Forms.TabPage tabIItem;
        private System.Windows.Forms.TreeView treeStaticObjects;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage MobsPage;
        private System.Windows.Forms.PropertyGrid objectProperties;
        private System.Windows.Forms.Label SelectedObjectLabel;
    }
}

