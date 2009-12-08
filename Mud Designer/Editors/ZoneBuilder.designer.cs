namespace MudDesigner.Editors
{
    partial class ZoneBuilder
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
            this.containerMain = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstZones = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSelectRealm = new System.Windows.Forms.Button();
            this.btnLoadZone = new System.Windows.Forms.Button();
            this.btnSaveZone = new System.Windows.Forms.Button();
            this.btnDeleteZone = new System.Windows.Forms.Button();
            this.btnNewZone = new System.Windows.Forms.Button();
            this.tabZoneBuilder = new System.Windows.Forms.TabControl();
            this.tabZone = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.propertyZone = new System.Windows.Forms.PropertyGrid();
            this.tabRoomDesigner = new System.Windows.Forms.TabPage();
            this.tabRoomEditor = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.propertyRoom = new System.Windows.Forms.PropertyGrid();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lstRooms = new System.Windows.Forms.ListBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnLoadRoom = new System.Windows.Forms.Button();
            this.btnSaveRoom = new System.Windows.Forms.Button();
            this.btnDeleteRoom = new System.Windows.Forms.Button();
            this.btnNewRoom = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Help = new System.Windows.Forms.ToolTip(this.components);
            this.containerMain.Panel1.SuspendLayout();
            this.containerMain.Panel2.SuspendLayout();
            this.containerMain.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabZoneBuilder.SuspendLayout();
            this.tabZone.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabRoomDesigner.SuspendLayout();
            this.tabRoomEditor.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // containerMain
            // 
            this.containerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerMain.Location = new System.Drawing.Point(0, 0);
            this.containerMain.Name = "containerMain";
            // 
            // containerMain.Panel1
            // 
            this.containerMain.Panel1.Controls.Add(this.groupBox1);
            this.containerMain.Panel1.Controls.Add(this.groupBox2);
            // 
            // containerMain.Panel2
            // 
            this.containerMain.Panel2.Controls.Add(this.tabZoneBuilder);
            this.containerMain.Size = new System.Drawing.Size(700, 471);
            this.containerMain.SplitterDistance = 210;
            this.containerMain.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstZones);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 371);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Zone List";
            // 
            // lstZones
            // 
            this.lstZones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstZones.FormattingEnabled = true;
            this.lstZones.Location = new System.Drawing.Point(3, 16);
            this.lstZones.Name = "lstZones";
            this.lstZones.Size = new System.Drawing.Size(204, 342);
            this.lstZones.Sorted = true;
            this.lstZones.TabIndex = 17;
            this.Help.SetToolTip(this.lstZones, "A collection of Zones that have already been created\r\nand stored within the selec" +
                    "ted Realm.");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSelectRealm);
            this.groupBox2.Controls.Add(this.btnLoadZone);
            this.groupBox2.Controls.Add(this.btnSaveZone);
            this.groupBox2.Controls.Add(this.btnDeleteZone);
            this.groupBox2.Controls.Add(this.btnNewZone);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(210, 100);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Zone Setup";
            // 
            // btnSelectRealm
            // 
            this.btnSelectRealm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSelectRealm.Location = new System.Drawing.Point(3, 74);
            this.btnSelectRealm.Name = "btnSelectRealm";
            this.btnSelectRealm.Size = new System.Drawing.Size(204, 23);
            this.btnSelectRealm.TabIndex = 12;
            this.btnSelectRealm.Text = "Select Realm";
            this.Help.SetToolTip(this.btnSelectRealm, "Selects the Realm that your new Zones will be created within");
            this.btnSelectRealm.UseVisualStyleBackColor = true;
            this.btnSelectRealm.Click += new System.EventHandler(this.btnSelectRealm_Click);
            // 
            // btnLoadZone
            // 
            this.btnLoadZone.Location = new System.Drawing.Point(122, 48);
            this.btnLoadZone.Name = "btnLoadZone";
            this.btnLoadZone.Size = new System.Drawing.Size(85, 23);
            this.btnLoadZone.TabIndex = 11;
            this.btnLoadZone.Text = "Load Zone";
            this.btnLoadZone.UseVisualStyleBackColor = true;
            this.btnLoadZone.Click += new System.EventHandler(this.btnLoadZone_Click);
            // 
            // btnSaveZone
            // 
            this.btnSaveZone.Location = new System.Drawing.Point(123, 19);
            this.btnSaveZone.Name = "btnSaveZone";
            this.btnSaveZone.Size = new System.Drawing.Size(84, 23);
            this.btnSaveZone.TabIndex = 10;
            this.btnSaveZone.Text = "Save Zone";
            this.btnSaveZone.UseVisualStyleBackColor = true;
            this.btnSaveZone.Click += new System.EventHandler(this.btnSaveZone_Click);
            // 
            // btnDeleteZone
            // 
            this.btnDeleteZone.Location = new System.Drawing.Point(6, 48);
            this.btnDeleteZone.Name = "btnDeleteZone";
            this.btnDeleteZone.Size = new System.Drawing.Size(85, 23);
            this.btnDeleteZone.TabIndex = 9;
            this.btnDeleteZone.Text = "Delete Zone";
            this.btnDeleteZone.UseVisualStyleBackColor = true;
            // 
            // btnNewZone
            // 
            this.btnNewZone.Location = new System.Drawing.Point(6, 19);
            this.btnNewZone.Name = "btnNewZone";
            this.btnNewZone.Size = new System.Drawing.Size(85, 23);
            this.btnNewZone.TabIndex = 8;
            this.btnNewZone.Text = "New Zone";
            this.btnNewZone.UseVisualStyleBackColor = true;
            this.btnNewZone.Click += new System.EventHandler(this.btnNewZone_Click);
            // 
            // tabZoneBuilder
            // 
            this.tabZoneBuilder.Controls.Add(this.tabZone);
            this.tabZoneBuilder.Controls.Add(this.tabRoomDesigner);
            this.tabZoneBuilder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabZoneBuilder.Location = new System.Drawing.Point(0, 0);
            this.tabZoneBuilder.Name = "tabZoneBuilder";
            this.tabZoneBuilder.SelectedIndex = 0;
            this.tabZoneBuilder.Size = new System.Drawing.Size(486, 471);
            this.tabZoneBuilder.TabIndex = 0;
            // 
            // tabZone
            // 
            this.tabZone.Controls.Add(this.groupBox4);
            this.tabZone.Controls.Add(this.groupBox3);
            this.tabZone.Location = new System.Drawing.Point(4, 22);
            this.tabZone.Name = "tabZone";
            this.tabZone.Padding = new System.Windows.Forms.Padding(3);
            this.tabZone.Size = new System.Drawing.Size(478, 445);
            this.tabZone.TabIndex = 0;
            this.tabZone.Text = "Zone Editing";
            this.tabZone.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(262, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(208, 436);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.propertyZone);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(253, 439);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Zone Properties";
            // 
            // propertyZone
            // 
            this.propertyZone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyZone.Location = new System.Drawing.Point(3, 16);
            this.propertyZone.Name = "propertyZone";
            this.propertyZone.Size = new System.Drawing.Size(247, 420);
            this.propertyZone.TabIndex = 0;
            this.propertyZone.ToolbarVisible = false;
            // 
            // tabRoomDesigner
            // 
            this.tabRoomDesigner.Controls.Add(this.tabRoomEditor);
            this.tabRoomDesigner.Location = new System.Drawing.Point(4, 22);
            this.tabRoomDesigner.Name = "tabRoomDesigner";
            this.tabRoomDesigner.Padding = new System.Windows.Forms.Padding(3);
            this.tabRoomDesigner.Size = new System.Drawing.Size(478, 445);
            this.tabRoomDesigner.TabIndex = 1;
            this.tabRoomDesigner.Text = "Room Designer";
            this.tabRoomDesigner.UseVisualStyleBackColor = true;
            // 
            // tabRoomEditor
            // 
            this.tabRoomEditor.Controls.Add(this.tabPage1);
            this.tabRoomEditor.Controls.Add(this.tabPage2);
            this.tabRoomEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabRoomEditor.Location = new System.Drawing.Point(3, 3);
            this.tabRoomEditor.Name = "tabRoomEditor";
            this.tabRoomEditor.SelectedIndex = 0;
            this.tabRoomEditor.Size = new System.Drawing.Size(472, 439);
            this.tabRoomEditor.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox7);
            this.tabPage1.Controls.Add(this.groupBox6);
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(464, 413);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Room Setup";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.propertyRoom);
            this.groupBox7.Location = new System.Drawing.Point(222, 6);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(239, 404);
            this.groupBox7.TabIndex = 39;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Room Properties";
            // 
            // propertyRoom
            // 
            this.propertyRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyRoom.Location = new System.Drawing.Point(3, 16);
            this.propertyRoom.Name = "propertyRoom";
            this.propertyRoom.Size = new System.Drawing.Size(233, 385);
            this.propertyRoom.TabIndex = 0;
            this.propertyRoom.ToolbarVisible = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lstRooms);
            this.groupBox6.Location = new System.Drawing.Point(6, 87);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(210, 323);
            this.groupBox6.TabIndex = 38;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Room List";
            // 
            // lstRooms
            // 
            this.lstRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRooms.FormattingEnabled = true;
            this.lstRooms.Location = new System.Drawing.Point(3, 16);
            this.lstRooms.Name = "lstRooms";
            this.lstRooms.Size = new System.Drawing.Size(204, 303);
            this.lstRooms.Sorted = true;
            this.lstRooms.TabIndex = 17;
            this.Help.SetToolTip(this.lstRooms, "A collection of Rooms that have already been created\r\nand stored within the curre" +
                    "ntly loaded Zone.");
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnLoadRoom);
            this.groupBox5.Controls.Add(this.btnSaveRoom);
            this.groupBox5.Controls.Add(this.btnDeleteRoom);
            this.groupBox5.Controls.Add(this.btnNewRoom);
            this.groupBox5.Location = new System.Drawing.Point(6, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(210, 75);
            this.groupBox5.TabIndex = 36;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Room Setup";
            // 
            // btnLoadRoom
            // 
            this.btnLoadRoom.Location = new System.Drawing.Point(122, 46);
            this.btnLoadRoom.Name = "btnLoadRoom";
            this.btnLoadRoom.Size = new System.Drawing.Size(85, 23);
            this.btnLoadRoom.TabIndex = 11;
            this.btnLoadRoom.Text = "Load Room";
            this.btnLoadRoom.UseVisualStyleBackColor = true;
            this.btnLoadRoom.Click += new System.EventHandler(this.btnLoadRoom_Click);
            // 
            // btnSaveRoom
            // 
            this.btnSaveRoom.Location = new System.Drawing.Point(123, 19);
            this.btnSaveRoom.Name = "btnSaveRoom";
            this.btnSaveRoom.Size = new System.Drawing.Size(84, 23);
            this.btnSaveRoom.TabIndex = 10;
            this.btnSaveRoom.Text = "Save Room";
            this.btnSaveRoom.UseVisualStyleBackColor = true;
            this.btnSaveRoom.Click += new System.EventHandler(this.btnSaveRoom_Click);
            // 
            // btnDeleteRoom
            // 
            this.btnDeleteRoom.Location = new System.Drawing.Point(6, 48);
            this.btnDeleteRoom.Name = "btnDeleteRoom";
            this.btnDeleteRoom.Size = new System.Drawing.Size(85, 23);
            this.btnDeleteRoom.TabIndex = 9;
            this.btnDeleteRoom.Text = "Delete Room";
            this.btnDeleteRoom.UseVisualStyleBackColor = true;
            // 
            // btnNewRoom
            // 
            this.btnNewRoom.Location = new System.Drawing.Point(6, 19);
            this.btnNewRoom.Name = "btnNewRoom";
            this.btnNewRoom.Size = new System.Drawing.Size(85, 23);
            this.btnNewRoom.TabIndex = 8;
            this.btnNewRoom.Text = "New Room";
            this.btnNewRoom.UseVisualStyleBackColor = true;
            this.btnNewRoom.Click += new System.EventHandler(this.btnNewRoom_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(464, 413);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Room Doorways";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Help
            // 
            this.Help.AutoPopDelay = 5000;
            this.Help.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Help.InitialDelay = 1000;
            this.Help.IsBalloon = true;
            this.Help.ReshowDelay = 100;
            this.Help.ToolTipTitle = "Zone Designer";
            // 
            // ZoneBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 471);
            this.Controls.Add(this.containerMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ZoneBuilder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zone Builder (No Realm Selected)";
            this.containerMain.Panel1.ResumeLayout(false);
            this.containerMain.Panel2.ResumeLayout(false);
            this.containerMain.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabZoneBuilder.ResumeLayout(false);
            this.tabZone.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tabRoomDesigner.ResumeLayout(false);
            this.tabRoomEditor.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer containerMain;
        private System.Windows.Forms.ToolTip Help;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnLoadZone;
        private System.Windows.Forms.Button btnSaveZone;
        private System.Windows.Forms.Button btnDeleteZone;
        private System.Windows.Forms.Button btnNewZone;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstZones;
        private System.Windows.Forms.TabControl tabZoneBuilder;
        private System.Windows.Forms.TabPage tabZone;
        private System.Windows.Forms.TabPage tabRoomDesigner;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PropertyGrid propertyZone;
        private System.Windows.Forms.Button btnSelectRealm;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TabControl tabRoomEditor;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnLoadRoom;
        private System.Windows.Forms.Button btnSaveRoom;
        private System.Windows.Forms.Button btnDeleteRoom;
        private System.Windows.Forms.Button btnNewRoom;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ListBox lstRooms;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.PropertyGrid propertyRoom;

    }
}