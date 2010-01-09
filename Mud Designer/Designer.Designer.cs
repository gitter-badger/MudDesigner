namespace MudDesigner
{
    partial class Designer
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Game Objects");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Designer));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDuplicateObject = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.findObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byDescriptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byFilenameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findInTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuPreferences = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProject = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGameManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.currencyEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProjectInformation = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuestBuilder = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGameObjects = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEnvironments = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRealmEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuZoneBuilder = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItems = new System.Windows.Forms.ToolStripMenuItem();
            this.customObjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.compileScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.objectRepositoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.freshLoginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customCharacterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playFromCurrentRoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.containerMain = new System.Windows.Forms.SplitContainer();
            this.containerSidebar = new System.Windows.Forms.SplitContainer();
            this.treeExplorer = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuEditObject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnRefreshObjects = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.propertyObject = new System.Windows.Forms.PropertyGrid();
            this.toolstripObjectProperties = new System.Windows.Forms.ToolStrip();
            this.btnSaveObject = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnValidate = new System.Windows.Forms.ToolStripButton();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.flowContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.realmExplorer1 = new MudDesigner.UIControls.RealmExplorer();
            this.menuStrip1.SuspendLayout();
            this.containerMain.Panel1.SuspendLayout();
            this.containerMain.Panel2.SuspendLayout();
            this.containerMain.SuspendLayout();
            this.containerSidebar.Panel1.SuspendLayout();
            this.containerSidebar.Panel2.SuspendLayout();
            this.containerSidebar.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolstripObjectProperties.SuspendLayout();
            this.flowContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuProject,
            this.mnuHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "File";
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(92, 22);
            this.mnuExit.Text = "Exit";
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDuplicateObject,
            this.mnuDelete,
            this.toolStripMenuItem3,
            this.mnuSearch,
            this.toolStripMenuItem4,
            this.mnuPreferences});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(39, 20);
            this.mnuEdit.Text = "Edit";
            // 
            // mnuDuplicateObject
            // 
            this.mnuDuplicateObject.Name = "mnuDuplicateObject";
            this.mnuDuplicateObject.Size = new System.Drawing.Size(162, 22);
            this.mnuDuplicateObject.Text = "Duplicate Object";
            // 
            // mnuDelete
            // 
            this.mnuDelete.Name = "mnuDelete";
            this.mnuDelete.Size = new System.Drawing.Size(162, 22);
            this.mnuDelete.Text = "Delete";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(159, 6);
            // 
            // mnuSearch
            // 
            this.mnuSearch.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findObjectToolStripMenuItem,
            this.findInTextToolStripMenuItem});
            this.mnuSearch.Name = "mnuSearch";
            this.mnuSearch.Size = new System.Drawing.Size(162, 22);
            this.mnuSearch.Text = "Search";
            // 
            // findObjectToolStripMenuItem
            // 
            this.findObjectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.byNameToolStripMenuItem,
            this.byDescriptionToolStripMenuItem,
            this.byFilenameToolStripMenuItem,
            this.byTypeToolStripMenuItem});
            this.findObjectToolStripMenuItem.Name = "findObjectToolStripMenuItem";
            this.findObjectToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.findObjectToolStripMenuItem.Text = "Find Object";
            // 
            // byNameToolStripMenuItem
            // 
            this.byNameToolStripMenuItem.Name = "byNameToolStripMenuItem";
            this.byNameToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.byNameToolStripMenuItem.Text = "By Name";
            // 
            // byDescriptionToolStripMenuItem
            // 
            this.byDescriptionToolStripMenuItem.Name = "byDescriptionToolStripMenuItem";
            this.byDescriptionToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.byDescriptionToolStripMenuItem.Text = "By Description";
            // 
            // byFilenameToolStripMenuItem
            // 
            this.byFilenameToolStripMenuItem.Name = "byFilenameToolStripMenuItem";
            this.byFilenameToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.byFilenameToolStripMenuItem.Text = "By Filename";
            // 
            // byTypeToolStripMenuItem
            // 
            this.byTypeToolStripMenuItem.Name = "byTypeToolStripMenuItem";
            this.byTypeToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.byTypeToolStripMenuItem.Text = "By Type";
            // 
            // findInTextToolStripMenuItem
            // 
            this.findInTextToolStripMenuItem.Name = "findInTextToolStripMenuItem";
            this.findInTextToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.findInTextToolStripMenuItem.Text = "Find Text";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(159, 6);
            // 
            // mnuPreferences
            // 
            this.mnuPreferences.Name = "mnuPreferences";
            this.mnuPreferences.Size = new System.Drawing.Size(162, 22);
            this.mnuPreferences.Text = "Preferences";
            // 
            // mnuProject
            // 
            this.mnuProject.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGameManagement,
            this.mnuGameObjects,
            this.customObjectsToolStripMenuItem,
            this.toolStripMenuItem5,
            this.compileScriptToolStripMenuItem,
            this.toolStripMenuItem6,
            this.objectRepositoryToolStripMenuItem,
            this.testProjectToolStripMenuItem});
            this.mnuProject.Name = "mnuProject";
            this.mnuProject.Size = new System.Drawing.Size(56, 20);
            this.mnuProject.Text = "Project";
            // 
            // mnuGameManagement
            // 
            this.mnuGameManagement.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.currencyEditorToolStripMenuItem,
            this.mnuProjectInformation,
            this.mnuQuestBuilder});
            this.mnuGameManagement.Name = "mnuGameManagement";
            this.mnuGameManagement.Size = new System.Drawing.Size(179, 22);
            this.mnuGameManagement.Text = "Game Management";
            // 
            // currencyEditorToolStripMenuItem
            // 
            this.currencyEditorToolStripMenuItem.Name = "currencyEditorToolStripMenuItem";
            this.currencyEditorToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.currencyEditorToolStripMenuItem.Text = "Currency Editor";
            this.currencyEditorToolStripMenuItem.Click += new System.EventHandler(this.currencyEditorToolStripMenuItem_Click);
            // 
            // mnuProjectInformation
            // 
            this.mnuProjectInformation.Name = "mnuProjectInformation";
            this.mnuProjectInformation.Size = new System.Drawing.Size(177, 22);
            this.mnuProjectInformation.Text = "Project Information";
            this.mnuProjectInformation.Click += new System.EventHandler(this.mnuProjectInformation_Click);
            // 
            // mnuQuestBuilder
            // 
            this.mnuQuestBuilder.Name = "mnuQuestBuilder";
            this.mnuQuestBuilder.Size = new System.Drawing.Size(177, 22);
            this.mnuQuestBuilder.Text = "Quest Builder";
            // 
            // mnuGameObjects
            // 
            this.mnuGameObjects.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEnvironments,
            this.mnuItems});
            this.mnuGameObjects.Name = "mnuGameObjects";
            this.mnuGameObjects.Size = new System.Drawing.Size(179, 22);
            this.mnuGameObjects.Text = "Game Objects";
            // 
            // mnuEnvironments
            // 
            this.mnuEnvironments.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRealmEditor,
            this.mnuZoneBuilder});
            this.mnuEnvironments.Name = "mnuEnvironments";
            this.mnuEnvironments.Size = new System.Drawing.Size(152, 22);
            this.mnuEnvironments.Text = "Environments";
            // 
            // mnuRealmEditor
            // 
            this.mnuRealmEditor.Name = "mnuRealmEditor";
            this.mnuRealmEditor.Size = new System.Drawing.Size(152, 22);
            this.mnuRealmEditor.Text = "Realm Editor";
            this.mnuRealmEditor.Click += new System.EventHandler(this.mnuRealmEditor_Click);
            // 
            // mnuZoneBuilder
            // 
            this.mnuZoneBuilder.Name = "mnuZoneBuilder";
            this.mnuZoneBuilder.Size = new System.Drawing.Size(152, 22);
            this.mnuZoneBuilder.Text = "Zone Builder";
            // 
            // mnuItems
            // 
            this.mnuItems.Enabled = false;
            this.mnuItems.Name = "mnuItems";
            this.mnuItems.Size = new System.Drawing.Size(152, 22);
            this.mnuItems.Text = "Items";
            // 
            // customObjectsToolStripMenuItem
            // 
            this.customObjectsToolStripMenuItem.Enabled = false;
            this.customObjectsToolStripMenuItem.Name = "customObjectsToolStripMenuItem";
            this.customObjectsToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.customObjectsToolStripMenuItem.Text = "Custom Objects";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(176, 6);
            // 
            // compileScriptToolStripMenuItem
            // 
            this.compileScriptToolStripMenuItem.Enabled = false;
            this.compileScriptToolStripMenuItem.Name = "compileScriptToolStripMenuItem";
            this.compileScriptToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.compileScriptToolStripMenuItem.Text = "Compile Script";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(176, 6);
            // 
            // objectRepositoryToolStripMenuItem
            // 
            this.objectRepositoryToolStripMenuItem.Enabled = false;
            this.objectRepositoryToolStripMenuItem.Name = "objectRepositoryToolStripMenuItem";
            this.objectRepositoryToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.objectRepositoryToolStripMenuItem.Text = "Object Repository";
            // 
            // testProjectToolStripMenuItem
            // 
            this.testProjectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.freshLoginToolStripMenuItem,
            this.customCharacterToolStripMenuItem,
            this.playFromCurrentRoomToolStripMenuItem});
            this.testProjectToolStripMenuItem.Enabled = false;
            this.testProjectToolStripMenuItem.Name = "testProjectToolStripMenuItem";
            this.testProjectToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.testProjectToolStripMenuItem.Text = "Test Project";
            // 
            // freshLoginToolStripMenuItem
            // 
            this.freshLoginToolStripMenuItem.Name = "freshLoginToolStripMenuItem";
            this.freshLoginToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.freshLoginToolStripMenuItem.Text = "Fresh Login";
            // 
            // customCharacterToolStripMenuItem
            // 
            this.customCharacterToolStripMenuItem.Name = "customCharacterToolStripMenuItem";
            this.customCharacterToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.customCharacterToolStripMenuItem.Text = "Custom Character";
            // 
            // playFromCurrentRoomToolStripMenuItem
            // 
            this.playFromCurrentRoomToolStripMenuItem.Name = "playFromCurrentRoomToolStripMenuItem";
            this.playFromCurrentRoomToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.playFromCurrentRoomToolStripMenuItem.Text = "Play From Current Room";
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "Help";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(107, 22);
            this.mnuAbout.Text = "About";
            // 
            // containerMain
            // 
            this.containerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.containerMain.Location = new System.Drawing.Point(0, 24);
            this.containerMain.Name = "containerMain";
            // 
            // containerMain.Panel1
            // 
            this.containerMain.Panel1.Controls.Add(this.flowContainer);
            // 
            // containerMain.Panel2
            // 
            this.containerMain.Panel2.Controls.Add(this.containerSidebar);
            this.containerMain.Size = new System.Drawing.Size(784, 540);
            this.containerMain.SplitterDistance = 526;
            this.containerMain.TabIndex = 1;
            // 
            // containerSidebar
            // 
            this.containerSidebar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.containerSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerSidebar.Location = new System.Drawing.Point(0, 0);
            this.containerSidebar.Name = "containerSidebar";
            this.containerSidebar.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // containerSidebar.Panel1
            // 
            this.containerSidebar.Panel1.Controls.Add(this.treeExplorer);
            this.containerSidebar.Panel1.Controls.Add(this.toolStrip1);
            this.containerSidebar.Panel1.Controls.Add(this.label1);
            // 
            // containerSidebar.Panel2
            // 
            this.containerSidebar.Panel2.Controls.Add(this.propertyObject);
            this.containerSidebar.Panel2.Controls.Add(this.toolstripObjectProperties);
            this.containerSidebar.Panel2.Controls.Add(this.label2);
            this.containerSidebar.Size = new System.Drawing.Size(254, 540);
            this.containerSidebar.SplitterDistance = 244;
            this.containerSidebar.TabIndex = 0;
            // 
            // treeExplorer
            // 
            this.treeExplorer.ContextMenuStrip = this.contextMenuStrip1;
            this.treeExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeExplorer.Location = new System.Drawing.Point(0, 38);
            this.treeExplorer.Name = "treeExplorer";
            treeNode1.Name = "nodeGameObjects";
            treeNode1.Text = "Game Objects";
            this.treeExplorer.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeExplorer.Size = new System.Drawing.Size(252, 204);
            this.treeExplorer.TabIndex = 5;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditObject});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(133, 26);
            // 
            // mnuEditObject
            // 
            this.mnuEditObject.Name = "mnuEditObject";
            this.mnuEditObject.Size = new System.Drawing.Size(132, 22);
            this.mnuEditObject.Text = "Edit Object";
            this.mnuEditObject.Click += new System.EventHandler(this.mnuEditObject_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefreshObjects,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.toolStripTextBox1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 13);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(252, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnRefreshObjects
            // 
            this.btnRefreshObjects.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRefreshObjects.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshObjects.Image")));
            this.btnRefreshObjects.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefreshObjects.Name = "btnRefreshObjects";
            this.btnRefreshObjects.Size = new System.Drawing.Size(93, 22);
            this.btnRefreshObjects.Text = "Refresh Objects";
            this.btnRefreshObjects.ToolTipText = "Save the current object";
            this.btnRefreshObjects.Click += new System.EventHandler(this.btnRefreshObjects_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(33, 22);
            this.toolStripLabel1.Text = "Find:";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 25);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(252, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Project Explorer";
            // 
            // propertyObject
            // 
            this.propertyObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyObject.Location = new System.Drawing.Point(0, 38);
            this.propertyObject.Name = "propertyObject";
            this.propertyObject.Size = new System.Drawing.Size(252, 252);
            this.propertyObject.TabIndex = 5;
            this.propertyObject.ToolbarVisible = false;
            // 
            // toolstripObjectProperties
            // 
            this.toolstripObjectProperties.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSaveObject,
            this.toolStripSeparator1,
            this.btnValidate});
            this.toolstripObjectProperties.Location = new System.Drawing.Point(0, 13);
            this.toolstripObjectProperties.Name = "toolstripObjectProperties";
            this.toolstripObjectProperties.Size = new System.Drawing.Size(252, 25);
            this.toolstripObjectProperties.TabIndex = 4;
            this.toolstripObjectProperties.Text = "toolStrip1";
            // 
            // btnSaveObject
            // 
            this.btnSaveObject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSaveObject.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveObject.Image")));
            this.btnSaveObject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveObject.Name = "btnSaveObject";
            this.btnSaveObject.Size = new System.Drawing.Size(73, 22);
            this.btnSaveObject.Text = "Save Object";
            this.btnSaveObject.ToolTipText = "Save the current object";
            this.btnSaveObject.Click += new System.EventHandler(this.btnSaveObject_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnValidate
            // 
            this.btnValidate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnValidate.Image = ((System.Drawing.Image)(resources.GetObject("btnValidate.Image")));
            this.btnValidate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(91, 22);
            this.btnValidate.Text = "Validate Object";
            this.btnValidate.ToolTipText = "Checks the current object to ensure that there are no errors. \r\nThis is just an e" +
                "rror check, and is not required.";
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(252, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Object Properties";
            // 
            // flowContainer
            // 
            this.flowContainer.Controls.Add(this.realmExplorer1);
            this.flowContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowContainer.Location = new System.Drawing.Point(0, 0);
            this.flowContainer.Name = "flowContainer";
            this.flowContainer.Size = new System.Drawing.Size(526, 540);
            this.flowContainer.TabIndex = 0;
            // 
            // realmExplorer1
            // 
            this.realmExplorer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.realmExplorer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.realmExplorer1.IsSplashVisible = true;
            this.realmExplorer1.Location = new System.Drawing.Point(3, 3);
            this.realmExplorer1.Name = "realmExplorer1";
            this.realmExplorer1.Size = new System.Drawing.Size(526, 0);
            this.realmExplorer1.TabIndex = 1;
            // 
            // Designer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(784, 564);
            this.Controls.Add(this.containerMain);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Designer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mud Designer Replacement Test";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.containerMain.Panel1.ResumeLayout(false);
            this.containerMain.Panel2.ResumeLayout(false);
            this.containerMain.ResumeLayout(false);
            this.containerSidebar.Panel1.ResumeLayout(false);
            this.containerSidebar.Panel1.PerformLayout();
            this.containerSidebar.Panel2.ResumeLayout(false);
            this.containerSidebar.Panel2.PerformLayout();
            this.containerSidebar.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolstripObjectProperties.ResumeLayout(false);
            this.toolstripObjectProperties.PerformLayout();
            this.flowContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuDuplicateObject;
        private System.Windows.Forms.ToolStripMenuItem mnuDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mnuSearch;
        private System.Windows.Forms.ToolStripMenuItem findObjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byDescriptionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byFilenameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findInTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem mnuPreferences;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuProject;
        private System.Windows.Forms.ToolStripMenuItem mnuGameManagement;
        private System.Windows.Forms.ToolStripMenuItem mnuQuestBuilder;
        private System.Windows.Forms.ToolStripMenuItem mnuProjectInformation;
        private System.Windows.Forms.ToolStripMenuItem mnuGameObjects;
        private System.Windows.Forms.ToolStripMenuItem mnuEnvironments;
        private System.Windows.Forms.ToolStripMenuItem mnuItems;
        private System.Windows.Forms.ToolStripMenuItem mnuRealmEditor;
        private System.Windows.Forms.ToolStripMenuItem mnuZoneBuilder;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem compileScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem testProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem freshLoginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customCharacterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playFromCurrentRoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem objectRepositoryToolStripMenuItem;
        private System.Windows.Forms.SplitContainer containerMain;
        private System.Windows.Forms.SplitContainer containerSidebar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem customObjectsToolStripMenuItem;
        private System.Windows.Forms.TreeView treeExplorer;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnRefreshObjects;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStrip toolstripObjectProperties;
        private System.Windows.Forms.ToolStripButton btnSaveObject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnValidate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PropertyGrid propertyObject;
        private System.Windows.Forms.ToolStripMenuItem currencyEditorToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuEditObject;
        private System.Windows.Forms.FlowLayoutPanel flowContainer;
        private MudDesigner.UIControls.RealmExplorer realmExplorer1;



    }
}