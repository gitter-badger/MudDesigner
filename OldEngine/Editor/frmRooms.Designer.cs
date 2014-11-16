namespace MudDesigner.Editor.Rooms
{
    partial class frmRooms
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
            this.splitEditorContainer = new System.Windows.Forms.SplitContainer();
            this.roomsBtnCloseEditor = new System.Windows.Forms.Button();
            this.roomsBtnLoadRoom = new System.Windows.Forms.Button();
            this.roomsBtnDeleteRoom = new System.Windows.Forms.Button();
            this.roomsBtnCreateRoom = new System.Windows.Forms.Button();
            this.roomsLblRealmAndZone = new System.Windows.Forms.Label();
            this.roomsBtnChangeZone = new System.Windows.Forms.Button();
            this.roomsLblCurrentRoom = new System.Windows.Forms.Label();
            this.roomsBtnDown = new System.Windows.Forms.Button();
            this.roomsBtnWest = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuClearDoorway = new System.Windows.Forms.ToolStripMenuItem();
            this.loadRoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.roomsBtnSouth = new System.Windows.Forms.Button();
            this.roomsBtnEast = new System.Windows.Forms.Button();
            this.roomsBtnNorth = new System.Windows.Forms.Button();
            this.roomsBtnUp = new System.Windows.Forms.Button();
            this.roomsTabMain = new System.Windows.Forms.TabControl();
            this.roomsTabEnvironments = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.roomsLstExistingRooms = new System.Windows.Forms.ListBox();
            this.roomsComZones = new System.Windows.Forms.ComboBox();
            this.roomsComRealms = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.roomsTabRoomProperties = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.roomsPropertiesDoorways = new System.Windows.Forms.PropertyGrid();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.roomsPropertiesRoom = new System.Windows.Forms.PropertyGrid();
            this.roomsMnuRoomObject = new System.Windows.Forms.ToolStripMenuItem();
            this.roomsMnuItems = new System.Windows.Forms.ToolStripMenuItem();
            this.roomsMnuMobs = new System.Windows.Forms.ToolStripMenuItem();
            this.roomsMnuQuests = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            ((System.ComponentModel.ISupportInitialize)(this.splitEditorContainer)).BeginInit();
            this.splitEditorContainer.Panel1.SuspendLayout();
            this.splitEditorContainer.Panel2.SuspendLayout();
            this.splitEditorContainer.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.roomsTabMain.SuspendLayout();
            this.roomsTabEnvironments.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.roomsTabRoomProperties.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitEditorContainer
            // 
            this.splitEditorContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitEditorContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitEditorContainer.Location = new System.Drawing.Point(0, 24);
            this.splitEditorContainer.Name = "splitEditorContainer";
            // 
            // splitEditorContainer.Panel1
            // 
            this.splitEditorContainer.Panel1.Controls.Add(this.roomsBtnCloseEditor);
            this.splitEditorContainer.Panel1.Controls.Add(this.roomsBtnLoadRoom);
            this.splitEditorContainer.Panel1.Controls.Add(this.roomsBtnDeleteRoom);
            this.splitEditorContainer.Panel1.Controls.Add(this.roomsBtnCreateRoom);
            this.splitEditorContainer.Panel1.Controls.Add(this.roomsLblRealmAndZone);
            this.splitEditorContainer.Panel1.Controls.Add(this.roomsBtnChangeZone);
            this.splitEditorContainer.Panel1.Controls.Add(this.roomsLblCurrentRoom);
            this.splitEditorContainer.Panel1.Controls.Add(this.roomsBtnDown);
            this.splitEditorContainer.Panel1.Controls.Add(this.roomsBtnWest);
            this.splitEditorContainer.Panel1.Controls.Add(this.roomsBtnSouth);
            this.splitEditorContainer.Panel1.Controls.Add(this.roomsBtnEast);
            this.splitEditorContainer.Panel1.Controls.Add(this.roomsBtnNorth);
            this.splitEditorContainer.Panel1.Controls.Add(this.roomsBtnUp);
            // 
            // splitEditorContainer.Panel2
            // 
            this.splitEditorContainer.Panel2.Controls.Add(this.roomsTabMain);
            this.splitEditorContainer.Size = new System.Drawing.Size(596, 464);
            this.splitEditorContainer.SplitterDistance = 325;
            this.splitEditorContainer.TabIndex = 0;
            // 
            // roomsBtnCloseEditor
            // 
            this.roomsBtnCloseEditor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roomsBtnCloseEditor.Location = new System.Drawing.Point(218, 78);
            this.roomsBtnCloseEditor.Name = "roomsBtnCloseEditor";
            this.roomsBtnCloseEditor.Size = new System.Drawing.Size(100, 24);
            this.roomsBtnCloseEditor.TabIndex = 20;
            this.roomsBtnCloseEditor.Text = "Close Editor";
            this.roomsBtnCloseEditor.UseVisualStyleBackColor = true;
            this.roomsBtnCloseEditor.Click += new System.EventHandler(this.roomsBtnCloseEditor_Click);
            // 
            // roomsBtnLoadRoom
            // 
            this.roomsBtnLoadRoom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.roomsBtnLoadRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roomsBtnLoadRoom.Location = new System.Drawing.Point(112, 47);
            this.roomsBtnLoadRoom.Name = "roomsBtnLoadRoom";
            this.roomsBtnLoadRoom.Size = new System.Drawing.Size(100, 25);
            this.roomsBtnLoadRoom.TabIndex = 18;
            this.roomsBtnLoadRoom.Text = "Load Room";
            this.roomsBtnLoadRoom.UseVisualStyleBackColor = true;
            this.roomsBtnLoadRoom.Click += new System.EventHandler(this.roomsBtnLoadRoom_Click);
            // 
            // roomsBtnDeleteRoom
            // 
            this.roomsBtnDeleteRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.roomsBtnDeleteRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roomsBtnDeleteRoom.Location = new System.Drawing.Point(218, 47);
            this.roomsBtnDeleteRoom.Name = "roomsBtnDeleteRoom";
            this.roomsBtnDeleteRoom.Size = new System.Drawing.Size(100, 25);
            this.roomsBtnDeleteRoom.TabIndex = 17;
            this.roomsBtnDeleteRoom.Text = "Delete Room";
            this.roomsBtnDeleteRoom.UseVisualStyleBackColor = true;
            this.roomsBtnDeleteRoom.Click += new System.EventHandler(this.roomsBtnDeleteRoom_Click);
            // 
            // roomsBtnCreateRoom
            // 
            this.roomsBtnCreateRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roomsBtnCreateRoom.Location = new System.Drawing.Point(6, 47);
            this.roomsBtnCreateRoom.Name = "roomsBtnCreateRoom";
            this.roomsBtnCreateRoom.Size = new System.Drawing.Size(100, 25);
            this.roomsBtnCreateRoom.TabIndex = 16;
            this.roomsBtnCreateRoom.Text = "Create Room";
            this.roomsBtnCreateRoom.UseVisualStyleBackColor = true;
            this.roomsBtnCreateRoom.Click += new System.EventHandler(this.roomsBtnCreateRoom_Click);
            // 
            // roomsLblRealmAndZone
            // 
            this.roomsLblRealmAndZone.AutoSize = true;
            this.roomsLblRealmAndZone.Location = new System.Drawing.Point(3, 18);
            this.roomsLblRealmAndZone.Name = "roomsLblRealmAndZone";
            this.roomsLblRealmAndZone.Size = new System.Drawing.Size(146, 13);
            this.roomsLblRealmAndZone.TabIndex = 15;
            this.roomsLblRealmAndZone.Text = "Current Zone: None Selected";
            // 
            // roomsBtnChangeZone
            // 
            this.roomsBtnChangeZone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roomsBtnChangeZone.Location = new System.Drawing.Point(6, 78);
            this.roomsBtnChangeZone.Name = "roomsBtnChangeZone";
            this.roomsBtnChangeZone.Size = new System.Drawing.Size(100, 24);
            this.roomsBtnChangeZone.TabIndex = 14;
            this.roomsBtnChangeZone.Text = "Change Zone";
            this.roomsBtnChangeZone.UseVisualStyleBackColor = true;
            this.roomsBtnChangeZone.Click += new System.EventHandler(this.roomsBtnChangeZone_Click);
            // 
            // roomsLblCurrentRoom
            // 
            this.roomsLblCurrentRoom.Dock = System.Windows.Forms.DockStyle.Top;
            this.roomsLblCurrentRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roomsLblCurrentRoom.Location = new System.Drawing.Point(0, 0);
            this.roomsLblCurrentRoom.Name = "roomsLblCurrentRoom";
            this.roomsLblCurrentRoom.Size = new System.Drawing.Size(325, 18);
            this.roomsLblCurrentRoom.TabIndex = 13;
            this.roomsLblCurrentRoom.Text = "Current Room: None Loaded";
            this.roomsLblCurrentRoom.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // roomsBtnDown
            // 
            this.roomsBtnDown.AllowDrop = true;
            this.roomsBtnDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.roomsBtnDown.FlatAppearance.BorderSize = 2;
            this.roomsBtnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roomsBtnDown.Location = new System.Drawing.Point(217, 387);
            this.roomsBtnDown.Name = "roomsBtnDown";
            this.roomsBtnDown.Size = new System.Drawing.Size(101, 67);
            this.roomsBtnDown.TabIndex = 12;
            this.roomsBtnDown.Text = "Down";
            this.roomsBtnDown.UseVisualStyleBackColor = false;
            this.roomsBtnDown.DragDrop += new System.Windows.Forms.DragEventHandler(this.Room_DragDrop);
            this.roomsBtnDown.DragOver += new System.Windows.Forms.DragEventHandler(this.Room_DragOver);
            // 
            // roomsBtnWest
            // 
            this.roomsBtnWest.AllowDrop = true;
            this.roomsBtnWest.ContextMenuStrip = this.contextMenuStrip1;
            this.roomsBtnWest.FlatAppearance.BorderSize = 2;
            this.roomsBtnWest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roomsBtnWest.Location = new System.Drawing.Point(6, 206);
            this.roomsBtnWest.Name = "roomsBtnWest";
            this.roomsBtnWest.Size = new System.Drawing.Size(101, 67);
            this.roomsBtnWest.TabIndex = 11;
            this.roomsBtnWest.Text = "West";
            this.roomsBtnWest.UseVisualStyleBackColor = true;
            this.roomsBtnWest.DragDrop += new System.Windows.Forms.DragEventHandler(this.Room_DragDrop);
            this.roomsBtnWest.DragOver += new System.Windows.Forms.DragEventHandler(this.Room_DragOver);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuClearDoorway,
            this.loadRoomToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 70);
            // 
            // mnuClearDoorway
            // 
            this.mnuClearDoorway.Name = "mnuClearDoorway";
            this.mnuClearDoorway.Size = new System.Drawing.Size(152, 22);
            this.mnuClearDoorway.Text = "Clear Doorway";
            this.mnuClearDoorway.Click += new System.EventHandler(this.mnuClearDoorway_Click);
            // 
            // loadRoomToolStripMenuItem
            // 
            this.loadRoomToolStripMenuItem.Name = "loadRoomToolStripMenuItem";
            this.loadRoomToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadRoomToolStripMenuItem.Text = "Load Room";
            this.loadRoomToolStripMenuItem.Click += new System.EventHandler(this.mnuLoadRoom_Click);
            // 
            // roomsBtnSouth
            // 
            this.roomsBtnSouth.AllowDrop = true;
            this.roomsBtnSouth.ContextMenuStrip = this.contextMenuStrip1;
            this.roomsBtnSouth.FlatAppearance.BorderSize = 2;
            this.roomsBtnSouth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roomsBtnSouth.Location = new System.Drawing.Point(111, 288);
            this.roomsBtnSouth.Name = "roomsBtnSouth";
            this.roomsBtnSouth.Size = new System.Drawing.Size(101, 67);
            this.roomsBtnSouth.TabIndex = 10;
            this.roomsBtnSouth.Text = "South";
            this.roomsBtnSouth.UseVisualStyleBackColor = true;
            this.roomsBtnSouth.DragDrop += new System.Windows.Forms.DragEventHandler(this.Room_DragDrop);
            this.roomsBtnSouth.DragOver += new System.Windows.Forms.DragEventHandler(this.Room_DragOver);
            // 
            // roomsBtnEast
            // 
            this.roomsBtnEast.AllowDrop = true;
            this.roomsBtnEast.ContextMenuStrip = this.contextMenuStrip1;
            this.roomsBtnEast.FlatAppearance.BorderSize = 2;
            this.roomsBtnEast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roomsBtnEast.Location = new System.Drawing.Point(217, 206);
            this.roomsBtnEast.Name = "roomsBtnEast";
            this.roomsBtnEast.Size = new System.Drawing.Size(100, 67);
            this.roomsBtnEast.TabIndex = 9;
            this.roomsBtnEast.Text = "East";
            this.roomsBtnEast.UseVisualStyleBackColor = true;
            this.roomsBtnEast.DragDrop += new System.Windows.Forms.DragEventHandler(this.Room_DragDrop);
            this.roomsBtnEast.DragOver += new System.Windows.Forms.DragEventHandler(this.Room_DragOver);
            // 
            // roomsBtnNorth
            // 
            this.roomsBtnNorth.AllowDrop = true;
            this.roomsBtnNorth.ContextMenuStrip = this.contextMenuStrip1;
            this.roomsBtnNorth.FlatAppearance.BorderSize = 2;
            this.roomsBtnNorth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roomsBtnNorth.Location = new System.Drawing.Point(112, 127);
            this.roomsBtnNorth.Name = "roomsBtnNorth";
            this.roomsBtnNorth.Size = new System.Drawing.Size(100, 67);
            this.roomsBtnNorth.TabIndex = 8;
            this.roomsBtnNorth.Text = "North";
            this.roomsBtnNorth.UseVisualStyleBackColor = true;
            this.roomsBtnNorth.DragDrop += new System.Windows.Forms.DragEventHandler(this.Room_DragDrop);
            this.roomsBtnNorth.DragOver += new System.Windows.Forms.DragEventHandler(this.Room_DragOver);
            // 
            // roomsBtnUp
            // 
            this.roomsBtnUp.AllowDrop = true;
            this.roomsBtnUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.roomsBtnUp.FlatAppearance.BorderSize = 2;
            this.roomsBtnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roomsBtnUp.Location = new System.Drawing.Point(6, 390);
            this.roomsBtnUp.Name = "roomsBtnUp";
            this.roomsBtnUp.Size = new System.Drawing.Size(101, 67);
            this.roomsBtnUp.TabIndex = 7;
            this.roomsBtnUp.Text = "Up";
            this.roomsBtnUp.UseVisualStyleBackColor = false;
            this.roomsBtnUp.DragDrop += new System.Windows.Forms.DragEventHandler(this.Room_DragDrop);
            this.roomsBtnUp.DragOver += new System.Windows.Forms.DragEventHandler(this.Room_DragOver);
            // 
            // roomsTabMain
            // 
            this.roomsTabMain.Controls.Add(this.roomsTabEnvironments);
            this.roomsTabMain.Controls.Add(this.roomsTabRoomProperties);
            this.roomsTabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roomsTabMain.Location = new System.Drawing.Point(0, 0);
            this.roomsTabMain.Name = "roomsTabMain";
            this.roomsTabMain.SelectedIndex = 0;
            this.roomsTabMain.Size = new System.Drawing.Size(267, 464);
            this.roomsTabMain.TabIndex = 0;
            // 
            // roomsTabEnvironments
            // 
            this.roomsTabEnvironments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.roomsTabEnvironments.Controls.Add(this.groupBox1);
            this.roomsTabEnvironments.ForeColor = System.Drawing.Color.White;
            this.roomsTabEnvironments.Location = new System.Drawing.Point(4, 22);
            this.roomsTabEnvironments.Name = "roomsTabEnvironments";
            this.roomsTabEnvironments.Padding = new System.Windows.Forms.Padding(3);
            this.roomsTabEnvironments.Size = new System.Drawing.Size(259, 438);
            this.roomsTabEnvironments.TabIndex = 0;
            this.roomsTabEnvironments.Text = "Environments";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.roomsComZones);
            this.groupBox1.Controls.Add(this.roomsComRealms);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(253, 432);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Available Environments To Link";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.roomsLstExistingRooms);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(3, 83);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(247, 346);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Available Rooms within Zone";
            // 
            // roomsLstExistingRooms
            // 
            this.roomsLstExistingRooms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.roomsLstExistingRooms.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.roomsLstExistingRooms.ForeColor = System.Drawing.Color.White;
            this.roomsLstExistingRooms.FormattingEnabled = true;
            this.roomsLstExistingRooms.Location = new System.Drawing.Point(3, 27);
            this.roomsLstExistingRooms.Name = "roomsLstExistingRooms";
            this.roomsLstExistingRooms.Size = new System.Drawing.Size(241, 316);
            this.roomsLstExistingRooms.Sorted = true;
            this.roomsLstExistingRooms.TabIndex = 0;
            this.roomsLstExistingRooms.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.roomsLstExistingRooms_QueryContinueDrag);
            this.roomsLstExistingRooms.MouseDown += new System.Windows.Forms.MouseEventHandler(this.roomsLstExistingRooms_MouseDown);
            this.roomsLstExistingRooms.MouseMove += new System.Windows.Forms.MouseEventHandler(this.roomsLstExistingRooms_MouseMove);
            this.roomsLstExistingRooms.MouseUp += new System.Windows.Forms.MouseEventHandler(this.roomsLstExistingRooms_MouseUp);
            // 
            // roomsComZones
            // 
            this.roomsComZones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.roomsComZones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.roomsComZones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.roomsComZones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roomsComZones.ForeColor = System.Drawing.Color.White;
            this.roomsComZones.FormattingEnabled = true;
            this.roomsComZones.Location = new System.Drawing.Point(125, 56);
            this.roomsComZones.Name = "roomsComZones";
            this.roomsComZones.Size = new System.Drawing.Size(121, 21);
            this.roomsComZones.Sorted = true;
            this.roomsComZones.TabIndex = 3;
            this.roomsComZones.SelectedIndexChanged += new System.EventHandler(this.roomsComZones_SelectedIndexChanged);
            // 
            // roomsComRealms
            // 
            this.roomsComRealms.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.roomsComRealms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.roomsComRealms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.roomsComRealms.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roomsComRealms.ForeColor = System.Drawing.Color.White;
            this.roomsComRealms.FormattingEnabled = true;
            this.roomsComRealms.Location = new System.Drawing.Point(125, 22);
            this.roomsComRealms.Name = "roomsComRealms";
            this.roomsComRealms.Size = new System.Drawing.Size(121, 21);
            this.roomsComRealms.Sorted = true;
            this.roomsComRealms.TabIndex = 2;
            this.roomsComRealms.SelectedIndexChanged += new System.EventHandler(this.roomsComRealms_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Zones within Realm";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Existing Realms";
            // 
            // roomsTabRoomProperties
            // 
            this.roomsTabRoomProperties.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.roomsTabRoomProperties.Controls.Add(this.groupBox4);
            this.roomsTabRoomProperties.Controls.Add(this.groupBox3);
            this.roomsTabRoomProperties.ForeColor = System.Drawing.Color.White;
            this.roomsTabRoomProperties.Location = new System.Drawing.Point(4, 22);
            this.roomsTabRoomProperties.Name = "roomsTabRoomProperties";
            this.roomsTabRoomProperties.Padding = new System.Windows.Forms.Padding(3);
            this.roomsTabRoomProperties.Size = new System.Drawing.Size(259, 438);
            this.roomsTabRoomProperties.TabIndex = 1;
            this.roomsTabRoomProperties.Text = "Room Properties";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.roomsPropertiesDoorways);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(3, 254);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(253, 181);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Doorway";
            // 
            // roomsPropertiesDoorways
            // 
            this.roomsPropertiesDoorways.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.roomsPropertiesDoorways.CommandsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.roomsPropertiesDoorways.CommandsForeColor = System.Drawing.Color.White;
            this.roomsPropertiesDoorways.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roomsPropertiesDoorways.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.roomsPropertiesDoorways.HelpForeColor = System.Drawing.Color.White;
            this.roomsPropertiesDoorways.HelpVisible = false;
            this.roomsPropertiesDoorways.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.roomsPropertiesDoorways.Location = new System.Drawing.Point(3, 16);
            this.roomsPropertiesDoorways.Name = "roomsPropertiesDoorways";
            this.roomsPropertiesDoorways.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.roomsPropertiesDoorways.Size = new System.Drawing.Size(247, 162);
            this.roomsPropertiesDoorways.TabIndex = 2;
            this.roomsPropertiesDoorways.ToolbarVisible = false;
            this.roomsPropertiesDoorways.ViewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.roomsPropertiesDoorways.ViewForeColor = System.Drawing.Color.White;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.roomsPropertiesRoom);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(253, 248);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Room";
            // 
            // roomsPropertiesRoom
            // 
            this.roomsPropertiesRoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.roomsPropertiesRoom.CommandsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.roomsPropertiesRoom.CommandsForeColor = System.Drawing.Color.White;
            this.roomsPropertiesRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roomsPropertiesRoom.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.roomsPropertiesRoom.HelpForeColor = System.Drawing.Color.White;
            this.roomsPropertiesRoom.HelpVisible = false;
            this.roomsPropertiesRoom.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.roomsPropertiesRoom.Location = new System.Drawing.Point(3, 16);
            this.roomsPropertiesRoom.Name = "roomsPropertiesRoom";
            this.roomsPropertiesRoom.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.roomsPropertiesRoom.Size = new System.Drawing.Size(247, 229);
            this.roomsPropertiesRoom.TabIndex = 2;
            this.roomsPropertiesRoom.ToolbarVisible = false;
            this.roomsPropertiesRoom.ViewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.roomsPropertiesRoom.ViewForeColor = System.Drawing.Color.White;
            this.roomsPropertiesRoom.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.roomsPropertiesRoom_PropertyValueChanged);
            // 
            // roomsMnuRoomObject
            // 
            this.roomsMnuRoomObject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.roomsMnuRoomObject.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.roomsMnuItems,
            this.roomsMnuMobs,
            this.roomsMnuQuests});
            this.roomsMnuRoomObject.Enabled = false;
            this.roomsMnuRoomObject.ForeColor = System.Drawing.Color.White;
            this.roomsMnuRoomObject.Name = "roomsMnuRoomObject";
            this.roomsMnuRoomObject.Size = new System.Drawing.Size(94, 20);
            this.roomsMnuRoomObject.Text = "Room Objects";
            // 
            // roomsMnuItems
            // 
            this.roomsMnuItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.roomsMnuItems.ForeColor = System.Drawing.Color.White;
            this.roomsMnuItems.Name = "roomsMnuItems";
            this.roomsMnuItems.Size = new System.Drawing.Size(110, 22);
            this.roomsMnuItems.Text = "Items";
            // 
            // roomsMnuMobs
            // 
            this.roomsMnuMobs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.roomsMnuMobs.ForeColor = System.Drawing.Color.White;
            this.roomsMnuMobs.Name = "roomsMnuMobs";
            this.roomsMnuMobs.Size = new System.Drawing.Size(110, 22);
            this.roomsMnuMobs.Text = "Mobs";
            // 
            // roomsMnuQuests
            // 
            this.roomsMnuQuests.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.roomsMnuQuests.ForeColor = System.Drawing.Color.White;
            this.roomsMnuQuests.Name = "roomsMnuQuests";
            this.roomsMnuQuests.Size = new System.Drawing.Size(110, 22);
            this.roomsMnuQuests.Text = "Quests";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.roomsMnuRoomObject});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(596, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // frmRooms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(596, 488);
            this.Controls.Add(this.splitEditorContainer);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.White;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmRooms";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mud Designer Editor : Rooms";
            this.Load += new System.EventHandler(this.frmRooms_Load);
            this.splitEditorContainer.Panel1.ResumeLayout(false);
            this.splitEditorContainer.Panel1.PerformLayout();
            this.splitEditorContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitEditorContainer)).EndInit();
            this.splitEditorContainer.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.roomsTabMain.ResumeLayout(false);
            this.roomsTabEnvironments.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.roomsTabRoomProperties.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitEditorContainer;
        private System.Windows.Forms.TabControl roomsTabMain;
        private System.Windows.Forms.TabPage roomsTabEnvironments;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox roomsLstExistingRooms;
        private System.Windows.Forms.ComboBox roomsComZones;
        private System.Windows.Forms.ComboBox roomsComRealms;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage roomsTabRoomProperties;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.PropertyGrid roomsPropertiesDoorways;
        private System.Windows.Forms.PropertyGrid roomsPropertiesRoom;
        private System.Windows.Forms.Label roomsLblCurrentRoom;
        private System.Windows.Forms.Button roomsBtnDown;
        private System.Windows.Forms.Button roomsBtnWest;
        private System.Windows.Forms.Button roomsBtnSouth;
        private System.Windows.Forms.Button roomsBtnEast;
        private System.Windows.Forms.Button roomsBtnNorth;
        private System.Windows.Forms.Button roomsBtnUp;
        private System.Windows.Forms.ToolStripMenuItem roomsMnuRoomObject;
        private System.Windows.Forms.ToolStripMenuItem roomsMnuItems;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem roomsMnuMobs;
        private System.Windows.Forms.ToolStripMenuItem roomsMnuQuests;
        private System.Windows.Forms.Label roomsLblRealmAndZone;
        private System.Windows.Forms.Button roomsBtnChangeZone;
        private System.Windows.Forms.Button roomsBtnLoadRoom;
        private System.Windows.Forms.Button roomsBtnDeleteRoom;
        private System.Windows.Forms.Button roomsBtnCreateRoom;
        private System.Windows.Forms.Button roomsBtnCloseEditor;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuClearDoorway;
        private System.Windows.Forms.ToolStripMenuItem loadRoomToolStripMenuItem;
    }
}