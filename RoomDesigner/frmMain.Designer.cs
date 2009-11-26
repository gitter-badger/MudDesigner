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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSaveRoom = new System.Windows.Forms.Button();
            this.btnNewRoom = new System.Windows.Forms.Button();
            this.btnCloseEditor = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.propertyRoom = new System.Windows.Forms.PropertyGrid();
            this.containerDesigner = new System.Windows.Forms.SplitContainer();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabBooks = new System.Windows.Forms.TabPage();
            this.tabEquipment = new System.Windows.Forms.TabPage();
            this.tabItems = new System.Windows.Forms.TabPage();
            this.tabScript = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabFunctions = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.txtScript = new System.Windows.Forms.RichTextBox();
            this.tabVariables = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.propertyDoor = new System.Windows.Forms.PropertyGrid();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lstDirections = new System.Windows.Forms.ListBox();
            this.containerMain.Panel1.SuspendLayout();
            this.containerMain.Panel2.SuspendLayout();
            this.containerMain.SuspendLayout();
            this.containerSidebar.Panel1.SuspendLayout();
            this.containerSidebar.Panel2.SuspendLayout();
            this.containerSidebar.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.containerDesigner.Panel1.SuspendLayout();
            this.containerDesigner.Panel2.SuspendLayout();
            this.containerDesigner.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabScript.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabFunctions.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox6.SuspendLayout();
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
            this.containerMain.Panel2.Controls.Add(this.containerDesigner);
            this.containerMain.Size = new System.Drawing.Size(792, 564);
            this.containerMain.SplitterDistance = 238;
            this.containerMain.TabIndex = 0;
            // 
            // containerSidebar
            // 
            this.containerSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerSidebar.Location = new System.Drawing.Point(0, 0);
            this.containerSidebar.Name = "containerSidebar";
            this.containerSidebar.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // containerSidebar.Panel1
            // 
            this.containerSidebar.Panel1.Controls.Add(this.groupBox3);
            this.containerSidebar.Panel1.Controls.Add(this.groupBox1);
            this.containerSidebar.Panel1.Controls.Add(this.btnCloseEditor);
            // 
            // containerSidebar.Panel2
            // 
            this.containerSidebar.Panel2.Controls.Add(this.groupBox2);
            this.containerSidebar.Size = new System.Drawing.Size(236, 562);
            this.containerSidebar.SplitterDistance = 109;
            this.containerSidebar.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(106, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(125, 74);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSaveRoom);
            this.groupBox1.Controls.Add(this.btnNewRoom);
            this.groupBox1.Location = new System.Drawing.Point(3, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(100, 74);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Room Options";
            // 
            // btnSaveRoom
            // 
            this.btnSaveRoom.Location = new System.Drawing.Point(3, 45);
            this.btnSaveRoom.Name = "btnSaveRoom";
            this.btnSaveRoom.Size = new System.Drawing.Size(91, 23);
            this.btnSaveRoom.TabIndex = 1;
            this.btnSaveRoom.Text = "Save Room";
            this.btnSaveRoom.UseVisualStyleBackColor = true;
            this.btnSaveRoom.Click += new System.EventHandler(this.btnSaveRoom_Click);
            // 
            // btnNewRoom
            // 
            this.btnNewRoom.Location = new System.Drawing.Point(3, 16);
            this.btnNewRoom.Name = "btnNewRoom";
            this.btnNewRoom.Size = new System.Drawing.Size(91, 23);
            this.btnNewRoom.TabIndex = 0;
            this.btnNewRoom.Text = "New Room";
            this.btnNewRoom.UseVisualStyleBackColor = true;
            this.btnNewRoom.Click += new System.EventHandler(this.btnNewRoom_Click);
            // 
            // btnCloseEditor
            // 
            this.btnCloseEditor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCloseEditor.Location = new System.Drawing.Point(0, 86);
            this.btnCloseEditor.Name = "btnCloseEditor";
            this.btnCloseEditor.Size = new System.Drawing.Size(236, 23);
            this.btnCloseEditor.TabIndex = 3;
            this.btnCloseEditor.Text = "Close Editor";
            this.btnCloseEditor.UseVisualStyleBackColor = true;
            this.btnCloseEditor.Click += new System.EventHandler(this.btnCloseEditor_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.propertyRoom);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(236, 449);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Room Setup";
            // 
            // propertyRoom
            // 
            this.propertyRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyRoom.Location = new System.Drawing.Point(3, 16);
            this.propertyRoom.Name = "propertyRoom";
            this.propertyRoom.Size = new System.Drawing.Size(230, 430);
            this.propertyRoom.TabIndex = 3;
            // 
            // containerDesigner
            // 
            this.containerDesigner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerDesigner.Location = new System.Drawing.Point(0, 0);
            this.containerDesigner.Name = "containerDesigner";
            this.containerDesigner.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // containerDesigner.Panel1
            // 
            this.containerDesigner.Panel1.Controls.Add(this.groupBox4);
            // 
            // containerDesigner.Panel2
            // 
            this.containerDesigner.Panel2.Controls.Add(this.groupBox5);
            this.containerDesigner.Size = new System.Drawing.Size(548, 562);
            this.containerDesigner.SplitterDistance = 318;
            this.containerDesigner.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tabControl1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(548, 318);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Object Management";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabBooks);
            this.tabControl1.Controls.Add(this.tabEquipment);
            this.tabControl1.Controls.Add(this.tabItems);
            this.tabControl1.Controls.Add(this.tabScript);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 16);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(542, 299);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabBooks
            // 
            this.tabBooks.Location = new System.Drawing.Point(4, 22);
            this.tabBooks.Name = "tabBooks";
            this.tabBooks.Padding = new System.Windows.Forms.Padding(3);
            this.tabBooks.Size = new System.Drawing.Size(534, 273);
            this.tabBooks.TabIndex = 0;
            this.tabBooks.Text = "Books";
            this.tabBooks.UseVisualStyleBackColor = true;
            // 
            // tabEquipment
            // 
            this.tabEquipment.Location = new System.Drawing.Point(4, 22);
            this.tabEquipment.Name = "tabEquipment";
            this.tabEquipment.Padding = new System.Windows.Forms.Padding(3);
            this.tabEquipment.Size = new System.Drawing.Size(534, 273);
            this.tabEquipment.TabIndex = 1;
            this.tabEquipment.Text = "Equipment";
            this.tabEquipment.UseVisualStyleBackColor = true;
            // 
            // tabItems
            // 
            this.tabItems.Location = new System.Drawing.Point(4, 22);
            this.tabItems.Name = "tabItems";
            this.tabItems.Size = new System.Drawing.Size(534, 273);
            this.tabItems.TabIndex = 2;
            this.tabItems.Text = "Items";
            this.tabItems.UseVisualStyleBackColor = true;
            // 
            // tabScript
            // 
            this.tabScript.Controls.Add(this.tabControl2);
            this.tabScript.Location = new System.Drawing.Point(4, 22);
            this.tabScript.Name = "tabScript";
            this.tabScript.Size = new System.Drawing.Size(534, 273);
            this.tabScript.TabIndex = 3;
            this.tabScript.Text = "Script";
            this.tabScript.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabFunctions);
            this.tabControl2.Controls.Add(this.tabVariables);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(534, 273);
            this.tabControl2.TabIndex = 0;
            // 
            // tabFunctions
            // 
            this.tabFunctions.Controls.Add(this.groupBox8);
            this.tabFunctions.Location = new System.Drawing.Point(4, 22);
            this.tabFunctions.Name = "tabFunctions";
            this.tabFunctions.Padding = new System.Windows.Forms.Padding(3);
            this.tabFunctions.Size = new System.Drawing.Size(526, 247);
            this.tabFunctions.TabIndex = 0;
            this.tabFunctions.Text = "Functions";
            this.tabFunctions.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.txtScript);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox8.Location = new System.Drawing.Point(3, 3);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(520, 241);
            this.groupBox8.TabIndex = 2;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Room Script";
            // 
            // txtScript
            // 
            this.txtScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtScript.Location = new System.Drawing.Point(3, 16);
            this.txtScript.Name = "txtScript";
            this.txtScript.Size = new System.Drawing.Size(514, 222);
            this.txtScript.TabIndex = 0;
            this.txtScript.Text = "";
            // 
            // tabVariables
            // 
            this.tabVariables.Location = new System.Drawing.Point(4, 22);
            this.tabVariables.Name = "tabVariables";
            this.tabVariables.Padding = new System.Windows.Forms.Padding(3);
            this.tabVariables.Size = new System.Drawing.Size(526, 247);
            this.tabVariables.TabIndex = 1;
            this.tabVariables.Text = "Variables";
            this.tabVariables.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.groupBox7);
            this.groupBox5.Controls.Add(this.groupBox6);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(548, 240);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Door Installation";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.groupBox10);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Location = new System.Drawing.Point(174, 16);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(371, 221);
            this.groupBox7.TabIndex = 1;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Install Options";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.propertyDoor);
            this.groupBox10.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox10.Location = new System.Drawing.Point(12, 16);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(356, 202);
            this.groupBox10.TabIndex = 7;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Door Setup";
            // 
            // propertyDoor
            // 
            this.propertyDoor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyDoor.Location = new System.Drawing.Point(3, 16);
            this.propertyDoor.Name = "propertyDoor";
            this.propertyDoor.Size = new System.Drawing.Size(350, 183);
            this.propertyDoor.TabIndex = 4;
            this.propertyDoor.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyDoor_PropertyValueChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lstDirections);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox6.Location = new System.Drawing.Point(3, 16);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(171, 221);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Available Directions";
            // 
            // lstDirections
            // 
            this.lstDirections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDirections.FormattingEnabled = true;
            this.lstDirections.Location = new System.Drawing.Point(3, 16);
            this.lstDirections.Name = "lstDirections";
            this.lstDirections.Size = new System.Drawing.Size(165, 199);
            this.lstDirections.TabIndex = 0;
            this.lstDirections.SelectedIndexChanged += new System.EventHandler(this.lstDirections_SelectedIndexChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 564);
            this.Controls.Add(this.containerMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mud Designer: Room Designer";
            this.containerMain.Panel1.ResumeLayout(false);
            this.containerMain.Panel2.ResumeLayout(false);
            this.containerMain.ResumeLayout(false);
            this.containerSidebar.Panel1.ResumeLayout(false);
            this.containerSidebar.Panel2.ResumeLayout(false);
            this.containerSidebar.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.containerDesigner.Panel1.ResumeLayout(false);
            this.containerDesigner.Panel2.ResumeLayout(false);
            this.containerDesigner.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabScript.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabFunctions.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer containerMain;
        private System.Windows.Forms.SplitContainer containerSidebar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PropertyGrid propertyRoom;
        private System.Windows.Forms.Button btnCloseEditor;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSaveRoom;
        private System.Windows.Forms.Button btnNewRoom;
        private System.Windows.Forms.SplitContainer containerDesigner;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabBooks;
        private System.Windows.Forms.TabPage tabEquipment;
        private System.Windows.Forms.TabPage tabItems;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ListBox lstDirections;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.PropertyGrid propertyDoor;
        private System.Windows.Forms.TabPage tabScript;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabFunctions;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.RichTextBox txtScript;
        private System.Windows.Forms.TabPage tabVariables;
    }
}

