namespace RoomDesigner
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
            this.containerMain = new System.Windows.Forms.SplitContainer();
            this.containerSidebar = new System.Windows.Forms.SplitContainer();
            this.tabObjects = new System.Windows.Forms.TabControl();
            this.ContainerDesignerHolderLeft = new System.Windows.Forms.SplitContainer();
            this.ContainerDesignerHolderRight = new System.Windows.Forms.SplitContainer();
            this.ContainerDesignerLeftTop = new System.Windows.Forms.SplitContainer();
            this.ContainerDesignerLeftBottom = new System.Windows.Forms.SplitContainer();
            this.ContainerDesignerMidTop = new System.Windows.Forms.SplitContainer();
            this.ContainerDesignerMidBottom = new System.Windows.Forms.SplitContainer();
            this.ContainerDesignerRightTop = new System.Windows.Forms.SplitContainer();
            this.ContainerDesignerRightBottom = new System.Windows.Forms.SplitContainer();
            this.btnNorth = new System.Windows.Forms.Button();
            this.btnSouth = new System.Windows.Forms.Button();
            this.btnWest = new System.Windows.Forms.Button();
            this.btnEast = new System.Windows.Forms.Button();
            this.btnPlaceholder = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnCurrentRoom = new System.Windows.Forms.Button();
            this.btnCurrentZone = new System.Windows.Forms.Button();
            this.btnDoorConnects = new System.Windows.Forms.Button();
            this.tabRooms = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comRealms = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tabDesignerTools = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.comZones = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstRooms = new System.Windows.Forms.ListBox();
            this.containerMain.Panel1.SuspendLayout();
            this.containerMain.Panel2.SuspendLayout();
            this.containerMain.SuspendLayout();
            this.containerSidebar.Panel1.SuspendLayout();
            this.containerSidebar.Panel2.SuspendLayout();
            this.containerSidebar.SuspendLayout();
            this.tabObjects.SuspendLayout();
            this.ContainerDesignerHolderLeft.Panel1.SuspendLayout();
            this.ContainerDesignerHolderLeft.Panel2.SuspendLayout();
            this.ContainerDesignerHolderLeft.SuspendLayout();
            this.ContainerDesignerHolderRight.Panel1.SuspendLayout();
            this.ContainerDesignerHolderRight.Panel2.SuspendLayout();
            this.ContainerDesignerHolderRight.SuspendLayout();
            this.ContainerDesignerLeftTop.Panel1.SuspendLayout();
            this.ContainerDesignerLeftTop.Panel2.SuspendLayout();
            this.ContainerDesignerLeftTop.SuspendLayout();
            this.ContainerDesignerLeftBottom.Panel1.SuspendLayout();
            this.ContainerDesignerLeftBottom.Panel2.SuspendLayout();
            this.ContainerDesignerLeftBottom.SuspendLayout();
            this.ContainerDesignerMidTop.Panel1.SuspendLayout();
            this.ContainerDesignerMidTop.Panel2.SuspendLayout();
            this.ContainerDesignerMidTop.SuspendLayout();
            this.ContainerDesignerMidBottom.Panel1.SuspendLayout();
            this.ContainerDesignerMidBottom.Panel2.SuspendLayout();
            this.ContainerDesignerMidBottom.SuspendLayout();
            this.ContainerDesignerRightTop.Panel1.SuspendLayout();
            this.ContainerDesignerRightTop.Panel2.SuspendLayout();
            this.ContainerDesignerRightTop.SuspendLayout();
            this.ContainerDesignerRightBottom.Panel1.SuspendLayout();
            this.ContainerDesignerRightBottom.Panel2.SuspendLayout();
            this.ContainerDesignerRightBottom.SuspendLayout();
            this.tabRooms.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabDesignerTools.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // containerMain
            // 
            this.containerMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.containerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerMain.Location = new System.Drawing.Point(0, 0);
            this.containerMain.Name = "containerMain";
            // 
            // containerMain.Panel1
            // 
            this.containerMain.Panel1.Controls.Add(this.containerSidebar);
            // 
            // containerMain.Panel2
            // 
            this.containerMain.Panel2.Controls.Add(this.ContainerDesignerHolderLeft);
            this.containerMain.Size = new System.Drawing.Size(784, 564);
            this.containerMain.SplitterDistance = 225;
            this.containerMain.TabIndex = 0;
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
            this.containerSidebar.Panel1.Controls.Add(this.groupBox4);
            // 
            // containerSidebar.Panel2
            // 
            this.containerSidebar.Panel2.Controls.Add(this.tabObjects);
            this.containerSidebar.Size = new System.Drawing.Size(225, 564);
            this.containerSidebar.SplitterDistance = 294;
            this.containerSidebar.TabIndex = 0;
            // 
            // tabObjects
            // 
            this.tabObjects.Controls.Add(this.tabDesignerTools);
            this.tabObjects.Controls.Add(this.tabRooms);
            this.tabObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabObjects.Location = new System.Drawing.Point(0, 0);
            this.tabObjects.Name = "tabObjects";
            this.tabObjects.SelectedIndex = 0;
            this.tabObjects.Size = new System.Drawing.Size(223, 264);
            this.tabObjects.TabIndex = 0;
            // 
            // ContainerDesignerHolderLeft
            // 
            this.ContainerDesignerHolderLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContainerDesignerHolderLeft.Location = new System.Drawing.Point(0, 0);
            this.ContainerDesignerHolderLeft.Name = "ContainerDesignerHolderLeft";
            // 
            // ContainerDesignerHolderLeft.Panel1
            // 
            this.ContainerDesignerHolderLeft.Panel1.Controls.Add(this.ContainerDesignerLeftTop);
            // 
            // ContainerDesignerHolderLeft.Panel2
            // 
            this.ContainerDesignerHolderLeft.Panel2.Controls.Add(this.ContainerDesignerHolderRight);
            this.ContainerDesignerHolderLeft.Size = new System.Drawing.Size(553, 562);
            this.ContainerDesignerHolderLeft.SplitterDistance = 175;
            this.ContainerDesignerHolderLeft.TabIndex = 0;
            // 
            // ContainerDesignerHolderRight
            // 
            this.ContainerDesignerHolderRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContainerDesignerHolderRight.Location = new System.Drawing.Point(0, 0);
            this.ContainerDesignerHolderRight.Name = "ContainerDesignerHolderRight";
            // 
            // ContainerDesignerHolderRight.Panel1
            // 
            this.ContainerDesignerHolderRight.Panel1.Controls.Add(this.ContainerDesignerMidTop);
            // 
            // ContainerDesignerHolderRight.Panel2
            // 
            this.ContainerDesignerHolderRight.Panel2.Controls.Add(this.ContainerDesignerRightTop);
            this.ContainerDesignerHolderRight.Size = new System.Drawing.Size(374, 562);
            this.ContainerDesignerHolderRight.SplitterDistance = 182;
            this.ContainerDesignerHolderRight.TabIndex = 1;
            // 
            // ContainerDesignerLeftTop
            // 
            this.ContainerDesignerLeftTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContainerDesignerLeftTop.Location = new System.Drawing.Point(0, 0);
            this.ContainerDesignerLeftTop.Name = "ContainerDesignerLeftTop";
            this.ContainerDesignerLeftTop.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ContainerDesignerLeftTop.Panel1
            // 
            this.ContainerDesignerLeftTop.Panel1.Controls.Add(this.btnUp);
            // 
            // ContainerDesignerLeftTop.Panel2
            // 
            this.ContainerDesignerLeftTop.Panel2.Controls.Add(this.ContainerDesignerLeftBottom);
            this.ContainerDesignerLeftTop.Size = new System.Drawing.Size(175, 562);
            this.ContainerDesignerLeftTop.SplitterDistance = 194;
            this.ContainerDesignerLeftTop.TabIndex = 0;
            // 
            // ContainerDesignerLeftBottom
            // 
            this.ContainerDesignerLeftBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContainerDesignerLeftBottom.Location = new System.Drawing.Point(0, 0);
            this.ContainerDesignerLeftBottom.Name = "ContainerDesignerLeftBottom";
            this.ContainerDesignerLeftBottom.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ContainerDesignerLeftBottom.Panel1
            // 
            this.ContainerDesignerLeftBottom.Panel1.Controls.Add(this.btnWest);
            // 
            // ContainerDesignerLeftBottom.Panel2
            // 
            this.ContainerDesignerLeftBottom.Panel2.Controls.Add(this.btnCurrentRoom);
            this.ContainerDesignerLeftBottom.Size = new System.Drawing.Size(175, 364);
            this.ContainerDesignerLeftBottom.SplitterDistance = 178;
            this.ContainerDesignerLeftBottom.TabIndex = 1;
            // 
            // ContainerDesignerMidTop
            // 
            this.ContainerDesignerMidTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContainerDesignerMidTop.Location = new System.Drawing.Point(0, 0);
            this.ContainerDesignerMidTop.Name = "ContainerDesignerMidTop";
            this.ContainerDesignerMidTop.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ContainerDesignerMidTop.Panel1
            // 
            this.ContainerDesignerMidTop.Panel1.Controls.Add(this.btnNorth);
            // 
            // ContainerDesignerMidTop.Panel2
            // 
            this.ContainerDesignerMidTop.Panel2.Controls.Add(this.ContainerDesignerMidBottom);
            this.ContainerDesignerMidTop.Size = new System.Drawing.Size(182, 562);
            this.ContainerDesignerMidTop.SplitterDistance = 194;
            this.ContainerDesignerMidTop.TabIndex = 1;
            // 
            // ContainerDesignerMidBottom
            // 
            this.ContainerDesignerMidBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContainerDesignerMidBottom.Location = new System.Drawing.Point(0, 0);
            this.ContainerDesignerMidBottom.Name = "ContainerDesignerMidBottom";
            this.ContainerDesignerMidBottom.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ContainerDesignerMidBottom.Panel1
            // 
            this.ContainerDesignerMidBottom.Panel1.Controls.Add(this.btnDoorConnects);
            // 
            // ContainerDesignerMidBottom.Panel2
            // 
            this.ContainerDesignerMidBottom.Panel2.Controls.Add(this.btnSouth);
            this.ContainerDesignerMidBottom.Size = new System.Drawing.Size(182, 364);
            this.ContainerDesignerMidBottom.SplitterDistance = 178;
            this.ContainerDesignerMidBottom.TabIndex = 1;
            // 
            // ContainerDesignerRightTop
            // 
            this.ContainerDesignerRightTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContainerDesignerRightTop.Location = new System.Drawing.Point(0, 0);
            this.ContainerDesignerRightTop.Name = "ContainerDesignerRightTop";
            this.ContainerDesignerRightTop.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ContainerDesignerRightTop.Panel1
            // 
            this.ContainerDesignerRightTop.Panel1.Controls.Add(this.btnDown);
            this.ContainerDesignerRightTop.Panel1.Controls.Add(this.btnPlaceholder);
            // 
            // ContainerDesignerRightTop.Panel2
            // 
            this.ContainerDesignerRightTop.Panel2.Controls.Add(this.ContainerDesignerRightBottom);
            this.ContainerDesignerRightTop.Size = new System.Drawing.Size(188, 562);
            this.ContainerDesignerRightTop.SplitterDistance = 194;
            this.ContainerDesignerRightTop.TabIndex = 1;
            // 
            // ContainerDesignerRightBottom
            // 
            this.ContainerDesignerRightBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContainerDesignerRightBottom.Location = new System.Drawing.Point(0, 0);
            this.ContainerDesignerRightBottom.Name = "ContainerDesignerRightBottom";
            this.ContainerDesignerRightBottom.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ContainerDesignerRightBottom.Panel1
            // 
            this.ContainerDesignerRightBottom.Panel1.Controls.Add(this.btnEast);
            // 
            // ContainerDesignerRightBottom.Panel2
            // 
            this.ContainerDesignerRightBottom.Panel2.Controls.Add(this.btnCurrentZone);
            this.ContainerDesignerRightBottom.Size = new System.Drawing.Size(188, 364);
            this.ContainerDesignerRightBottom.SplitterDistance = 178;
            this.ContainerDesignerRightBottom.TabIndex = 1;
            // 
            // btnNorth
            // 
            this.btnNorth.BackColor = System.Drawing.Color.Silver;
            this.btnNorth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNorth.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnNorth.FlatAppearance.BorderSize = 2;
            this.btnNorth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNorth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNorth.Location = new System.Drawing.Point(0, 0);
            this.btnNorth.Name = "btnNorth";
            this.btnNorth.Size = new System.Drawing.Size(182, 194);
            this.btnNorth.TabIndex = 1;
            this.btnNorth.Text = "North";
            this.btnNorth.UseVisualStyleBackColor = false;
            // 
            // btnSouth
            // 
            this.btnSouth.BackColor = System.Drawing.Color.Silver;
            this.btnSouth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSouth.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSouth.FlatAppearance.BorderSize = 2;
            this.btnSouth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSouth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSouth.Location = new System.Drawing.Point(0, 0);
            this.btnSouth.Name = "btnSouth";
            this.btnSouth.Size = new System.Drawing.Size(182, 182);
            this.btnSouth.TabIndex = 1;
            this.btnSouth.Text = "South";
            this.btnSouth.UseVisualStyleBackColor = false;
            // 
            // btnWest
            // 
            this.btnWest.BackColor = System.Drawing.Color.Silver;
            this.btnWest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnWest.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnWest.FlatAppearance.BorderSize = 2;
            this.btnWest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWest.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWest.Location = new System.Drawing.Point(0, 0);
            this.btnWest.Name = "btnWest";
            this.btnWest.Size = new System.Drawing.Size(175, 178);
            this.btnWest.TabIndex = 1;
            this.btnWest.Text = "West";
            this.btnWest.UseVisualStyleBackColor = false;
            // 
            // btnEast
            // 
            this.btnEast.BackColor = System.Drawing.Color.Silver;
            this.btnEast.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEast.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnEast.FlatAppearance.BorderSize = 2;
            this.btnEast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEast.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEast.Location = new System.Drawing.Point(0, 0);
            this.btnEast.Name = "btnEast";
            this.btnEast.Size = new System.Drawing.Size(188, 178);
            this.btnEast.TabIndex = 1;
            this.btnEast.Text = "East";
            this.btnEast.UseVisualStyleBackColor = false;
            // 
            // btnPlaceholder
            // 
            this.btnPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPlaceholder.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnPlaceholder.FlatAppearance.BorderSize = 2;
            this.btnPlaceholder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlaceholder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlaceholder.Location = new System.Drawing.Point(0, 0);
            this.btnPlaceholder.Name = "btnPlaceholder";
            this.btnPlaceholder.Size = new System.Drawing.Size(188, 194);
            this.btnPlaceholder.TabIndex = 2;
            this.btnPlaceholder.UseVisualStyleBackColor = true;
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.Silver;
            this.btnUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUp.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnUp.FlatAppearance.BorderSize = 2;
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUp.Location = new System.Drawing.Point(0, 0);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(175, 194);
            this.btnUp.TabIndex = 1;
            this.btnUp.Text = "Up";
            this.btnUp.UseVisualStyleBackColor = false;
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.Silver;
            this.btnDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDown.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnDown.FlatAppearance.BorderSize = 2;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDown.Location = new System.Drawing.Point(0, 0);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(188, 194);
            this.btnDown.TabIndex = 3;
            this.btnDown.Text = "Down";
            this.btnDown.UseVisualStyleBackColor = false;
            // 
            // btnCurrentRoom
            // 
            this.btnCurrentRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCurrentRoom.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnCurrentRoom.FlatAppearance.BorderSize = 2;
            this.btnCurrentRoom.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnCurrentRoom.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnCurrentRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCurrentRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCurrentRoom.Location = new System.Drawing.Point(0, 0);
            this.btnCurrentRoom.Name = "btnCurrentRoom";
            this.btnCurrentRoom.Size = new System.Drawing.Size(175, 182);
            this.btnCurrentRoom.TabIndex = 7;
            this.btnCurrentRoom.Text = "Current Room:\r\nNone";
            this.btnCurrentRoom.UseVisualStyleBackColor = true;
            // 
            // btnCurrentZone
            // 
            this.btnCurrentZone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCurrentZone.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnCurrentZone.FlatAppearance.BorderSize = 2;
            this.btnCurrentZone.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnCurrentZone.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnCurrentZone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCurrentZone.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCurrentZone.Location = new System.Drawing.Point(0, 0);
            this.btnCurrentZone.Name = "btnCurrentZone";
            this.btnCurrentZone.Size = new System.Drawing.Size(188, 182);
            this.btnCurrentZone.TabIndex = 6;
            this.btnCurrentZone.Text = "Current Zone:\r\nNone";
            this.btnCurrentZone.UseVisualStyleBackColor = true;
            // 
            // btnDoorConnects
            // 
            this.btnDoorConnects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDoorConnects.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnDoorConnects.FlatAppearance.BorderSize = 2;
            this.btnDoorConnects.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.btnDoorConnects.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnDoorConnects.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDoorConnects.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDoorConnects.Location = new System.Drawing.Point(0, 0);
            this.btnDoorConnects.Name = "btnDoorConnects";
            this.btnDoorConnects.Size = new System.Drawing.Size(182, 178);
            this.btnDoorConnects.TabIndex = 5;
            this.btnDoorConnects.Text = "Door Connects To:\r\nNo Door Selected";
            this.btnDoorConnects.UseVisualStyleBackColor = true;
            // 
            // tabRooms
            // 
            this.tabRooms.Controls.Add(this.groupBox2);
            this.tabRooms.Controls.Add(this.groupBox5);
            this.tabRooms.Controls.Add(this.groupBox1);
            this.tabRooms.Location = new System.Drawing.Point(4, 22);
            this.tabRooms.Name = "tabRooms";
            this.tabRooms.Padding = new System.Windows.Forms.Padding(3);
            this.tabRooms.Size = new System.Drawing.Size(215, 238);
            this.tabRooms.TabIndex = 0;
            this.tabRooms.Text = "Existing Rooms";
            this.tabRooms.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comRealms);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(209, 47);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Realms";
            // 
            // comRealms
            // 
            this.comRealms.Dock = System.Windows.Forms.DockStyle.Top;
            this.comRealms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comRealms.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comRealms.FormattingEnabled = true;
            this.comRealms.Location = new System.Drawing.Point(3, 16);
            this.comRealms.Name = "comRealms";
            this.comRealms.Size = new System.Drawing.Size(203, 21);
            this.comRealms.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.propertyGrid1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(223, 292);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Room Settings";
            // 
            // tabDesignerTools
            // 
            this.tabDesignerTools.Controls.Add(this.groupBox3);
            this.tabDesignerTools.Location = new System.Drawing.Point(4, 22);
            this.tabDesignerTools.Name = "tabDesignerTools";
            this.tabDesignerTools.Padding = new System.Windows.Forms.Padding(3);
            this.tabDesignerTools.Size = new System.Drawing.Size(215, 238);
            this.tabDesignerTools.TabIndex = 1;
            this.tabDesignerTools.Text = "Designer Tools";
            this.tabDesignerTools.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.flowLayoutPanel1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(209, 79);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Editing Options";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(3, 16);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(217, 273);
            this.propertyGrid1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnNew);
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.Controls.Add(this.btnDelete);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(203, 60);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnNew
            // 
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Location = new System.Drawing.Point(3, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(93, 23);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "New Room";
            this.btnNew.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(102, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(93, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save Room";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(3, 32);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(93, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete Room";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.comZones);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(3, 50);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(209, 47);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Zones";
            // 
            // comZones
            // 
            this.comZones.Dock = System.Windows.Forms.DockStyle.Top;
            this.comZones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comZones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comZones.FormattingEnabled = true;
            this.comZones.Location = new System.Drawing.Point(3, 16);
            this.comZones.Name = "comZones";
            this.comZones.Size = new System.Drawing.Size(203, 21);
            this.comZones.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstRooms);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(209, 138);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rooms within Zone";
            // 
            // lstRooms
            // 
            this.lstRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRooms.FormattingEnabled = true;
            this.lstRooms.Location = new System.Drawing.Point(3, 16);
            this.lstRooms.Name = "lstRooms";
            this.lstRooms.Size = new System.Drawing.Size(203, 108);
            this.lstRooms.TabIndex = 0;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 564);
            this.Controls.Add(this.containerMain);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mud Designer: Room Designer";
            this.containerMain.Panel1.ResumeLayout(false);
            this.containerMain.Panel2.ResumeLayout(false);
            this.containerMain.ResumeLayout(false);
            this.containerSidebar.Panel1.ResumeLayout(false);
            this.containerSidebar.Panel2.ResumeLayout(false);
            this.containerSidebar.ResumeLayout(false);
            this.tabObjects.ResumeLayout(false);
            this.ContainerDesignerHolderLeft.Panel1.ResumeLayout(false);
            this.ContainerDesignerHolderLeft.Panel2.ResumeLayout(false);
            this.ContainerDesignerHolderLeft.ResumeLayout(false);
            this.ContainerDesignerHolderRight.Panel1.ResumeLayout(false);
            this.ContainerDesignerHolderRight.Panel2.ResumeLayout(false);
            this.ContainerDesignerHolderRight.ResumeLayout(false);
            this.ContainerDesignerLeftTop.Panel1.ResumeLayout(false);
            this.ContainerDesignerLeftTop.Panel2.ResumeLayout(false);
            this.ContainerDesignerLeftTop.ResumeLayout(false);
            this.ContainerDesignerLeftBottom.Panel1.ResumeLayout(false);
            this.ContainerDesignerLeftBottom.Panel2.ResumeLayout(false);
            this.ContainerDesignerLeftBottom.ResumeLayout(false);
            this.ContainerDesignerMidTop.Panel1.ResumeLayout(false);
            this.ContainerDesignerMidTop.Panel2.ResumeLayout(false);
            this.ContainerDesignerMidTop.ResumeLayout(false);
            this.ContainerDesignerMidBottom.Panel1.ResumeLayout(false);
            this.ContainerDesignerMidBottom.Panel2.ResumeLayout(false);
            this.ContainerDesignerMidBottom.ResumeLayout(false);
            this.ContainerDesignerRightTop.Panel1.ResumeLayout(false);
            this.ContainerDesignerRightTop.Panel2.ResumeLayout(false);
            this.ContainerDesignerRightTop.ResumeLayout(false);
            this.ContainerDesignerRightBottom.Panel1.ResumeLayout(false);
            this.ContainerDesignerRightBottom.Panel2.ResumeLayout(false);
            this.ContainerDesignerRightBottom.ResumeLayout(false);
            this.tabRooms.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.tabDesignerTools.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer containerMain;
        private System.Windows.Forms.SplitContainer containerSidebar;
        private System.Windows.Forms.TabControl tabObjects;
        private System.Windows.Forms.SplitContainer ContainerDesignerHolderLeft;
        private System.Windows.Forms.SplitContainer ContainerDesignerHolderRight;
        private System.Windows.Forms.SplitContainer ContainerDesignerLeftTop;
        private System.Windows.Forms.SplitContainer ContainerDesignerLeftBottom;
        private System.Windows.Forms.SplitContainer ContainerDesignerMidTop;
        private System.Windows.Forms.SplitContainer ContainerDesignerMidBottom;
        private System.Windows.Forms.SplitContainer ContainerDesignerRightBottom;
        private System.Windows.Forms.SplitContainer ContainerDesignerRightTop;
        private System.Windows.Forms.Button btnNorth;
        private System.Windows.Forms.Button btnSouth;
        private System.Windows.Forms.Button btnWest;
        private System.Windows.Forms.Button btnEast;
        private System.Windows.Forms.Button btnPlaceholder;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnCurrentRoom;
        private System.Windows.Forms.Button btnDoorConnects;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnCurrentZone;
        private System.Windows.Forms.TabPage tabRooms;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comRealms;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TabPage tabDesignerTools;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox comZones;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lstRooms;
    }
}

