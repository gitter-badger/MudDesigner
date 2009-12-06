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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.propertyZone = new System.Windows.Forms.PropertyGrid();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCloseBuilder = new System.Windows.Forms.Button();
            this.btnSaveZone = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabZoneCreation = new System.Windows.Forms.TabPage();
            this.tabScript = new System.Windows.Forms.TabPage();
            this.txtScript = new System.Windows.Forms.RichTextBox();
            this.btnValidateScript = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnUnselectRoom = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnRoomEditor = new System.Windows.Forms.Button();
            this.lstRooms = new System.Windows.Forms.ListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.propertyRoom = new System.Windows.Forms.PropertyGrid();
            this.btnNorth = new System.Windows.Forms.Button();
            this.btnWest = new System.Windows.Forms.Button();
            this.btnEast = new System.Windows.Forms.Button();
            this.btnSouth = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.containerAvailableRooms = new System.Windows.Forms.SplitContainer();
            this.comRealms = new System.Windows.Forms.ComboBox();
            this.btnCurrentRealm = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.lstZonesInRealm = new System.Windows.Forms.ListBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.lstRoomsInZone = new System.Windows.Forms.ListBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabZoneCreation.SuspendLayout();
            this.tabScript.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.containerAvailableRooms.Panel1.SuspendLayout();
            this.containerAvailableRooms.Panel2.SuspendLayout();
            this.containerAvailableRooms.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.propertyZone);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(794, 574);
            this.splitContainer1.SplitterDistance = 238;
            this.splitContainer1.TabIndex = 0;
            // 
            // propertyZone
            // 
            this.propertyZone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyZone.Location = new System.Drawing.Point(0, 71);
            this.propertyZone.Name = "propertyZone";
            this.propertyZone.Size = new System.Drawing.Size(238, 503);
            this.propertyZone.TabIndex = 1;
            this.propertyZone.ToolbarVisible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCloseBuilder);
            this.groupBox1.Controls.Add(this.btnSaveZone);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(238, 71);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Zone Setup";
            // 
            // btnCloseBuilder
            // 
            this.btnCloseBuilder.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCloseBuilder.Location = new System.Drawing.Point(3, 39);
            this.btnCloseBuilder.Name = "btnCloseBuilder";
            this.btnCloseBuilder.Size = new System.Drawing.Size(232, 23);
            this.btnCloseBuilder.TabIndex = 12;
            this.btnCloseBuilder.Text = "Close Builder";
            this.btnCloseBuilder.UseVisualStyleBackColor = true;
            this.btnCloseBuilder.Click += new System.EventHandler(this.btnCloseBuilder_Click);
            // 
            // btnSaveZone
            // 
            this.btnSaveZone.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSaveZone.Location = new System.Drawing.Point(3, 16);
            this.btnSaveZone.Name = "btnSaveZone";
            this.btnSaveZone.Size = new System.Drawing.Size(232, 23);
            this.btnSaveZone.TabIndex = 11;
            this.btnSaveZone.Text = "Save Zone";
            this.btnSaveZone.UseVisualStyleBackColor = true;
            this.btnSaveZone.Click += new System.EventHandler(this.btnSaveZone_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Size = new System.Drawing.Size(552, 574);
            this.splitContainer2.SplitterDistance = 347;
            this.splitContainer2.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabZoneCreation);
            this.tabControl1.Controls.Add(this.tabScript);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(347, 574);
            this.tabControl1.TabIndex = 0;
            // 
            // tabZoneCreation
            // 
            this.tabZoneCreation.Controls.Add(this.groupBox5);
            this.tabZoneCreation.Controls.Add(this.groupBox3);
            this.tabZoneCreation.Location = new System.Drawing.Point(4, 22);
            this.tabZoneCreation.Name = "tabZoneCreation";
            this.tabZoneCreation.Padding = new System.Windows.Forms.Padding(3);
            this.tabZoneCreation.Size = new System.Drawing.Size(339, 548);
            this.tabZoneCreation.TabIndex = 0;
            this.tabZoneCreation.Text = "Zone Creation";
            this.tabZoneCreation.UseVisualStyleBackColor = true;
            // 
            // tabScript
            // 
            this.tabScript.Controls.Add(this.txtScript);
            this.tabScript.Controls.Add(this.btnValidateScript);
            this.tabScript.Location = new System.Drawing.Point(4, 22);
            this.tabScript.Name = "tabScript";
            this.tabScript.Padding = new System.Windows.Forms.Padding(3);
            this.tabScript.Size = new System.Drawing.Size(357, 548);
            this.tabScript.TabIndex = 1;
            this.tabScript.Text = "Script";
            this.tabScript.UseVisualStyleBackColor = true;
            // 
            // txtScript
            // 
            this.txtScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtScript.Location = new System.Drawing.Point(3, 26);
            this.txtScript.Name = "txtScript";
            this.txtScript.Size = new System.Drawing.Size(351, 519);
            this.txtScript.TabIndex = 15;
            this.txtScript.Text = "";
            this.txtScript.TextChanged += new System.EventHandler(this.txtScript_TextChanged);
            // 
            // btnValidateScript
            // 
            this.btnValidateScript.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnValidateScript.Location = new System.Drawing.Point(3, 3);
            this.btnValidateScript.Name = "btnValidateScript";
            this.btnValidateScript.Size = new System.Drawing.Size(351, 23);
            this.btnValidateScript.TabIndex = 14;
            this.btnValidateScript.Text = "Validate Script";
            this.btnValidateScript.UseVisualStyleBackColor = true;
            this.btnValidateScript.Click += new System.EventHandler(this.btnValidateScript_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstRooms);
            this.groupBox2.Controls.Add(this.btnRoomEditor);
            this.groupBox2.Controls.Add(this.btnUnselectRoom);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(201, 303);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Available Rooms";
            // 
            // btnUnselectRoom
            // 
            this.btnUnselectRoom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnUnselectRoom.Location = new System.Drawing.Point(3, 277);
            this.btnUnselectRoom.Name = "btnUnselectRoom";
            this.btnUnselectRoom.Size = new System.Drawing.Size(195, 23);
            this.btnUnselectRoom.TabIndex = 2;
            this.btnUnselectRoom.Text = "Unselect Room";
            this.btnUnselectRoom.UseVisualStyleBackColor = true;
            this.btnUnselectRoom.Click += new System.EventHandler(this.btnUnselectRoom_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnDown);
            this.groupBox3.Controls.Add(this.btnUp);
            this.groupBox3.Controls.Add(this.btnSouth);
            this.groupBox3.Controls.Add(this.btnEast);
            this.groupBox3.Controls.Add(this.btnWest);
            this.groupBox3.Controls.Add(this.btnNorth);
            this.groupBox3.Location = new System.Drawing.Point(3, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(333, 236);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Room Installed Doorways";
            // 
            // btnRoomEditor
            // 
            this.btnRoomEditor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnRoomEditor.Location = new System.Drawing.Point(3, 254);
            this.btnRoomEditor.Name = "btnRoomEditor";
            this.btnRoomEditor.Size = new System.Drawing.Size(195, 23);
            this.btnRoomEditor.TabIndex = 4;
            this.btnRoomEditor.Text = "Build A Room";
            this.btnRoomEditor.UseVisualStyleBackColor = true;
            this.btnRoomEditor.Click += new System.EventHandler(this.btnRoomEditor_Click);
            // 
            // lstRooms
            // 
            this.lstRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRooms.FormattingEnabled = true;
            this.lstRooms.Location = new System.Drawing.Point(3, 16);
            this.lstRooms.Name = "lstRooms";
            this.lstRooms.Size = new System.Drawing.Size(195, 238);
            this.lstRooms.Sorted = true;
            this.lstRooms.TabIndex = 5;
            this.lstRooms.SelectedIndexChanged += new System.EventHandler(this.lstRooms_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.propertyRoom);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox4.Location = new System.Drawing.Point(0, 306);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(201, 268);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Room Preview";
            // 
            // propertyRoom
            // 
            this.propertyRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyRoom.Enabled = false;
            this.propertyRoom.HelpVisible = false;
            this.propertyRoom.Location = new System.Drawing.Point(3, 16);
            this.propertyRoom.Name = "propertyRoom";
            this.propertyRoom.Size = new System.Drawing.Size(195, 249);
            this.propertyRoom.TabIndex = 0;
            this.propertyRoom.ToolbarVisible = false;
            // 
            // btnNorth
            // 
            this.btnNorth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnNorth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNorth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNorth.Location = new System.Drawing.Point(124, 19);
            this.btnNorth.Name = "btnNorth";
            this.btnNorth.Size = new System.Drawing.Size(85, 56);
            this.btnNorth.TabIndex = 0;
            this.btnNorth.Text = "North";
            this.btnNorth.UseVisualStyleBackColor = false;
            // 
            // btnWest
            // 
            this.btnWest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnWest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWest.Location = new System.Drawing.Point(6, 60);
            this.btnWest.Name = "btnWest";
            this.btnWest.Size = new System.Drawing.Size(85, 56);
            this.btnWest.TabIndex = 1;
            this.btnWest.Text = "West";
            this.btnWest.UseVisualStyleBackColor = false;
            // 
            // btnEast
            // 
            this.btnEast.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnEast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEast.Location = new System.Drawing.Point(242, 60);
            this.btnEast.Name = "btnEast";
            this.btnEast.Size = new System.Drawing.Size(85, 56);
            this.btnEast.TabIndex = 2;
            this.btnEast.Text = "East";
            this.btnEast.UseVisualStyleBackColor = false;
            // 
            // btnSouth
            // 
            this.btnSouth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSouth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSouth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSouth.Location = new System.Drawing.Point(124, 114);
            this.btnSouth.Name = "btnSouth";
            this.btnSouth.Size = new System.Drawing.Size(85, 56);
            this.btnSouth.TabIndex = 3;
            this.btnSouth.Text = "South";
            this.btnSouth.UseVisualStyleBackColor = false;
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUp.Location = new System.Drawing.Point(6, 170);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(85, 56);
            this.btnUp.TabIndex = 4;
            this.btnUp.Text = "Up";
            this.btnUp.UseVisualStyleBackColor = false;
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDown.Location = new System.Drawing.Point(242, 170);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(85, 56);
            this.btnDown.TabIndex = 5;
            this.btnDown.Text = "Down";
            this.btnDown.UseVisualStyleBackColor = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.groupBox8);
            this.groupBox5.Controls.Add(this.groupBox7);
            this.groupBox5.Controls.Add(this.groupBox6);
            this.groupBox5.Location = new System.Drawing.Point(3, 248);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(333, 297);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Available Rooms";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.containerAvailableRooms);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox6.Location = new System.Drawing.Point(3, 16);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(327, 43);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Realms";
            // 
            // containerAvailableRooms
            // 
            this.containerAvailableRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerAvailableRooms.Location = new System.Drawing.Point(3, 16);
            this.containerAvailableRooms.Name = "containerAvailableRooms";
            // 
            // containerAvailableRooms.Panel1
            // 
            this.containerAvailableRooms.Panel1.Controls.Add(this.comRealms);
            // 
            // containerAvailableRooms.Panel2
            // 
            this.containerAvailableRooms.Panel2.Controls.Add(this.btnCurrentRealm);
            this.containerAvailableRooms.Size = new System.Drawing.Size(321, 24);
            this.containerAvailableRooms.SplitterDistance = 191;
            this.containerAvailableRooms.TabIndex = 0;
            // 
            // comRealms
            // 
            this.comRealms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comRealms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comRealms.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comRealms.FormattingEnabled = true;
            this.comRealms.Location = new System.Drawing.Point(0, 0);
            this.comRealms.Name = "comRealms";
            this.comRealms.Size = new System.Drawing.Size(191, 21);
            this.comRealms.Sorted = true;
            this.comRealms.TabIndex = 1;
            this.comRealms.SelectedIndexChanged += new System.EventHandler(this.comRealms_SelectedIndexChanged);
            // 
            // btnCurrentRealm
            // 
            this.btnCurrentRealm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCurrentRealm.Location = new System.Drawing.Point(0, 0);
            this.btnCurrentRealm.Name = "btnCurrentRealm";
            this.btnCurrentRealm.Size = new System.Drawing.Size(126, 24);
            this.btnCurrentRealm.TabIndex = 0;
            this.btnCurrentRealm.Text = "Select Owning Realm";
            this.btnCurrentRealm.UseVisualStyleBackColor = true;
            this.btnCurrentRealm.Click += new System.EventHandler(this.btnCurrentRealm_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.lstZonesInRealm);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox7.Location = new System.Drawing.Point(3, 59);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(327, 105);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Zones Within Selected Realm";
            // 
            // lstZonesInRealm
            // 
            this.lstZonesInRealm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstZonesInRealm.FormattingEnabled = true;
            this.lstZonesInRealm.Location = new System.Drawing.Point(3, 16);
            this.lstZonesInRealm.Name = "lstZonesInRealm";
            this.lstZonesInRealm.Size = new System.Drawing.Size(321, 82);
            this.lstZonesInRealm.Sorted = true;
            this.lstZonesInRealm.TabIndex = 0;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.lstRoomsInZone);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox8.Location = new System.Drawing.Point(3, 164);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(327, 127);
            this.groupBox8.TabIndex = 3;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Rooms within Selected Zone";
            // 
            // lstRoomsInZone
            // 
            this.lstRoomsInZone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRoomsInZone.FormattingEnabled = true;
            this.lstRoomsInZone.Location = new System.Drawing.Point(3, 16);
            this.lstRoomsInZone.Name = "lstRoomsInZone";
            this.lstRoomsInZone.Size = new System.Drawing.Size(321, 108);
            this.lstRoomsInZone.Sorted = true;
            this.lstRoomsInZone.TabIndex = 0;
            // 
            // ZoneBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 574);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ZoneBuilder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zone Builder";
            this.Load += new System.EventHandler(this.ZoneBuilder_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabZoneCreation.ResumeLayout(false);
            this.tabScript.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.containerAvailableRooms.Panel1.ResumeLayout(false);
            this.containerAvailableRooms.Panel2.ResumeLayout(false);
            this.containerAvailableRooms.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSaveZone;
        private System.Windows.Forms.PropertyGrid propertyZone;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabZoneCreation;
        private System.Windows.Forms.TabPage tabScript;
        private System.Windows.Forms.RichTextBox txtScript;
        private System.Windows.Forms.Button btnValidateScript;
        private System.Windows.Forms.Button btnCloseBuilder;
        private System.Windows.Forms.Button btnUnselectRoom;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.ListBox lstRooms;
        private System.Windows.Forms.Button btnRoomEditor;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.PropertyGrid propertyRoom;
        private System.Windows.Forms.Button btnNorth;
        private System.Windows.Forms.Button btnSouth;
        private System.Windows.Forms.Button btnEast;
        private System.Windows.Forms.Button btnWest;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.SplitContainer containerAvailableRooms;
        private System.Windows.Forms.ComboBox comRealms;
        private System.Windows.Forms.Button btnCurrentRealm;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ListBox lstZonesInRealm;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ListBox lstRoomsInZone;

    }
}