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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.SelectedObjectLabel = new System.Windows.Forms.Label();
            this.objectProperties = new System.Windows.Forms.PropertyGrid();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editorContainer)).BeginInit();
            this.editorContainer.Panel1.SuspendLayout();
            this.editorContainer.Panel2.SuspendLayout();
            this.editorContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RoomEditor_Properties)).BeginInit();
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
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.statusStrip1.Location = new System.Drawing.Point(0, 708);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.worldToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
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
            this.newRealmToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.newRealmToolStripMenuItem.Text = "New Realm";
            this.newRealmToolStripMenuItem.Click += new System.EventHandler(this.newRealmToolStripMenuItem_Click);
            // 
            // newZoneToolStripMenuItem
            // 
            this.newZoneToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.newZoneToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.newZoneToolStripMenuItem.Name = "newZoneToolStripMenuItem";
            this.newZoneToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.newZoneToolStripMenuItem.Text = "New Zone";
            this.newZoneToolStripMenuItem.Click += new System.EventHandler(this.newZoneToolStripMenuItem_Click);
            // 
            // newRoomToolStripMenuItem
            // 
            this.newRoomToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.newRoomToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.newRoomToolStripMenuItem.Name = "newRoomToolStripMenuItem";
            this.newRoomToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
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
            this.editorContainer.Size = new System.Drawing.Size(1008, 684);
            this.editorContainer.SplitterDistance = 195;
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
            this.objectBrowser.Size = new System.Drawing.Size(191, 665);
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
            this.panel1.Size = new System.Drawing.Size(191, 15);
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
            this.textBox1.Location = new System.Drawing.Point(72, 0);
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
            this.RoomEditor_Properties.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.RoomEditor_Properties.Location = new System.Drawing.Point(0, 0);
            this.RoomEditor_Properties.Name = "RoomEditor_Properties";
            // 
            // RoomEditor_Properties.Panel2
            // 
            this.RoomEditor_Properties.Panel2.Controls.Add(this.splitContainer2);
            this.RoomEditor_Properties.Size = new System.Drawing.Size(808, 684);
            this.RoomEditor_Properties.SplitterDistance = 499;
            this.RoomEditor_Properties.SplitterIncrement = 2;
            this.RoomEditor_Properties.SplitterWidth = 5;
            this.RoomEditor_Properties.TabIndex = 0;
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
            this.splitContainer2.Size = new System.Drawing.Size(300, 680);
            this.splitContainer2.SplitterDistance = 347;
            this.splitContainer2.TabIndex = 0;
            // 
            // GameExplorer
            // 
            this.GameExplorer.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.GameExplorer.Controls.Add(this.EnvironmentPage);
            this.GameExplorer.Controls.Add(this.tabPage2);
            this.GameExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GameExplorer.Location = new System.Drawing.Point(0, 0);
            this.GameExplorer.Name = "GameExplorer";
            this.GameExplorer.SelectedIndex = 0;
            this.GameExplorer.Size = new System.Drawing.Size(300, 347);
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
            this.EnvironmentPage.Size = new System.Drawing.Size(292, 318);
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
            this.EnvironmentOptions.Size = new System.Drawing.Size(286, 312);
            this.EnvironmentOptions.TabIndex = 0;
            // 
            // RealmTab
            // 
            this.RealmTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.RealmTab.Controls.Add(this.AvailableRealms);
            this.RealmTab.Location = new System.Drawing.Point(4, 22);
            this.RealmTab.Name = "RealmTab";
            this.RealmTab.Padding = new System.Windows.Forms.Padding(3);
            this.RealmTab.Size = new System.Drawing.Size(278, 286);
            this.RealmTab.TabIndex = 1;
            this.RealmTab.Text = "Realms";
            // 
            // AvailableRealms
            // 
            this.AvailableRealms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.AvailableRealms.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AvailableRealms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AvailableRealms.ForeColor = System.Drawing.Color.White;
            this.AvailableRealms.FormattingEnabled = true;
            this.AvailableRealms.Location = new System.Drawing.Point(3, 3);
            this.AvailableRealms.Name = "AvailableRealms";
            this.AvailableRealms.Size = new System.Drawing.Size(272, 280);
            this.AvailableRealms.Sorted = true;
            this.AvailableRealms.TabIndex = 1;
            this.AvailableRealms.SelectedIndexChanged += new System.EventHandler(this.AvailableRealms_SelectedIndexChanged);
            // 
            // ZoneTab
            // 
            this.ZoneTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ZoneTab.Controls.Add(this.AvailableZones);
            this.ZoneTab.Location = new System.Drawing.Point(4, 22);
            this.ZoneTab.Name = "ZoneTab";
            this.ZoneTab.Size = new System.Drawing.Size(278, 286);
            this.ZoneTab.TabIndex = 2;
            this.ZoneTab.Text = "Zones";
            // 
            // AvailableZones
            // 
            this.AvailableZones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.AvailableZones.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AvailableZones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AvailableZones.ForeColor = System.Drawing.Color.White;
            this.AvailableZones.FormattingEnabled = true;
            this.AvailableZones.Location = new System.Drawing.Point(0, 0);
            this.AvailableZones.Name = "AvailableZones";
            this.AvailableZones.Size = new System.Drawing.Size(278, 286);
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
            this.RoomTab.Size = new System.Drawing.Size(278, 283);
            this.RoomTab.TabIndex = 3;
            this.RoomTab.Text = "Rooms";
            // 
            // AvailableRooms
            // 
            this.AvailableRooms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.AvailableRooms.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AvailableRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AvailableRooms.ForeColor = System.Drawing.Color.White;
            this.AvailableRooms.FormattingEnabled = true;
            this.AvailableRooms.Location = new System.Drawing.Point(0, 0);
            this.AvailableRooms.Name = "AvailableRooms";
            this.AvailableRooms.Size = new System.Drawing.Size(278, 283);
            this.AvailableRooms.Sorted = true;
            this.AvailableRooms.TabIndex = 3;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(292, 315);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // SelectedObjectLabel
            // 
            this.SelectedObjectLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.SelectedObjectLabel.ForeColor = System.Drawing.Color.White;
            this.SelectedObjectLabel.Location = new System.Drawing.Point(0, 0);
            this.SelectedObjectLabel.Name = "SelectedObjectLabel";
            this.SelectedObjectLabel.Size = new System.Drawing.Size(300, 16);
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
            this.objectProperties.Location = new System.Drawing.Point(0, 19);
            this.objectProperties.Name = "objectProperties";
            this.objectProperties.SelectedItemWithFocusForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.objectProperties.Size = new System.Drawing.Size(300, 310);
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
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.editorContainer);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Editor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mud Designer Editor";
            this.Load += new System.EventHandler(this.Editor_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.editorContainer.Panel1.ResumeLayout(false);
            this.editorContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.editorContainer)).EndInit();
            this.editorContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
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
        private System.Windows.Forms.TabPage tabPage2;
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
    }
}

