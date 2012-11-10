namespace MudDesigner.Editor
{
    partial class frmEngineSettings
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.scriptLibrary = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.loginRoom = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.loginSuccessState = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.defaultPlayerType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.defaultGameType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.initialState = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.scriptsPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAddScriptLibrary = new System.Windows.Forms.Button();
            this.btnRemoveScriptLibrary = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.zoneType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.realmType = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.defaultWorldType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.doorType = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.roomType = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.worldFile = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.playerSavePath = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.btnCancelSettings = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.btnRemoveScriptLibrary);
            this.groupBox1.Controls.Add(this.btnAddScriptLibrary);
            this.groupBox1.Controls.Add(this.scriptsPath);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.scriptLibrary);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(268, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(244, 207);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Script Settings";
            // 
            // scriptLibrary
            // 
            this.scriptLibrary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.scriptLibrary.FormattingEnabled = true;
            this.scriptLibrary.Location = new System.Drawing.Point(3, 109);
            this.scriptLibrary.Name = "scriptLibrary";
            this.scriptLibrary.Size = new System.Drawing.Size(238, 95);
            this.scriptLibrary.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.initialState);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.loginRoom);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.loginSuccessState);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(12, 86);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(244, 101);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Client Login Items";
            // 
            // loginRoom
            // 
            this.loginRoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.loginRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.loginRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loginRoom.ForeColor = System.Drawing.Color.White;
            this.loginRoom.FormattingEnabled = true;
            this.loginRoom.Location = new System.Drawing.Point(114, 40);
            this.loginRoom.Name = "loginRoom";
            this.loginRoom.Size = new System.Drawing.Size(121, 21);
            this.loginRoom.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Login Room";
            // 
            // loginSuccessState
            // 
            this.loginSuccessState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.loginSuccessState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.loginSuccessState.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loginSuccessState.ForeColor = System.Drawing.Color.White;
            this.loginSuccessState.FormattingEnabled = true;
            this.loginSuccessState.Location = new System.Drawing.Point(114, 13);
            this.loginSuccessState.Name = "loginSuccessState";
            this.loginSuccessState.Size = new System.Drawing.Size(121, 21);
            this.loginSuccessState.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Client Login Script";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.defaultPlayerType);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.defaultGameType);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(12, 9);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(244, 71);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Game Defaults";
            // 
            // defaultPlayerType
            // 
            this.defaultPlayerType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.defaultPlayerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.defaultPlayerType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.defaultPlayerType.ForeColor = System.Drawing.Color.White;
            this.defaultPlayerType.FormattingEnabled = true;
            this.defaultPlayerType.Location = new System.Drawing.Point(116, 40);
            this.defaultPlayerType.Name = "defaultPlayerType";
            this.defaultPlayerType.Size = new System.Drawing.Size(121, 21);
            this.defaultPlayerType.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Default Player Script";
            // 
            // defaultGameType
            // 
            this.defaultGameType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.defaultGameType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.defaultGameType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.defaultGameType.ForeColor = System.Drawing.Color.White;
            this.defaultGameType.FormattingEnabled = true;
            this.defaultGameType.Location = new System.Drawing.Point(116, 13);
            this.defaultGameType.Name = "defaultGameType";
            this.defaultGameType.Size = new System.Drawing.Size(121, 21);
            this.defaultGameType.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Default Game Script";
            // 
            // initialState
            // 
            this.initialState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.initialState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.initialState.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.initialState.ForeColor = System.Drawing.Color.White;
            this.initialState.FormattingEnabled = true;
            this.initialState.Location = new System.Drawing.Point(114, 67);
            this.initialState.Name = "initialState";
            this.initialState.Size = new System.Drawing.Size(121, 21);
            this.initialState.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Initial State";
            // 
            // scriptsPath
            // 
            this.scriptsPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.scriptsPath.ForeColor = System.Drawing.Color.White;
            this.scriptsPath.Location = new System.Drawing.Point(110, 14);
            this.scriptsPath.Name = "scriptsPath";
            this.scriptsPath.ReadOnly = true;
            this.scriptsPath.Size = new System.Drawing.Size(127, 20);
            this.scriptsPath.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Script Folder Name";
            // 
            // btnAddScriptLibrary
            // 
            this.btnAddScriptLibrary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddScriptLibrary.Location = new System.Drawing.Point(6, 80);
            this.btnAddScriptLibrary.Name = "btnAddScriptLibrary";
            this.btnAddScriptLibrary.Size = new System.Drawing.Size(105, 23);
            this.btnAddScriptLibrary.TabIndex = 12;
            this.btnAddScriptLibrary.Text = "Add Script Library";
            this.btnAddScriptLibrary.UseVisualStyleBackColor = true;
            // 
            // btnRemoveScriptLibrary
            // 
            this.btnRemoveScriptLibrary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveScriptLibrary.Location = new System.Drawing.Point(132, 80);
            this.btnRemoveScriptLibrary.Name = "btnRemoveScriptLibrary";
            this.btnRemoveScriptLibrary.Size = new System.Drawing.Size(105, 23);
            this.btnRemoveScriptLibrary.TabIndex = 13;
            this.btnRemoveScriptLibrary.Text = "Remove Library";
            this.btnRemoveScriptLibrary.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.doorType);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.roomType);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.zoneType);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.realmType);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.defaultWorldType);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(12, 193);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(244, 156);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Environment Defaults";
            // 
            // zoneType
            // 
            this.zoneType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.zoneType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.zoneType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zoneType.ForeColor = System.Drawing.Color.White;
            this.zoneType.FormattingEnabled = true;
            this.zoneType.Location = new System.Drawing.Point(116, 67);
            this.zoneType.Name = "zoneType";
            this.zoneType.Size = new System.Drawing.Size(121, 21);
            this.zoneType.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Zone Script";
            // 
            // realmType
            // 
            this.realmType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.realmType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.realmType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.realmType.ForeColor = System.Drawing.Color.White;
            this.realmType.FormattingEnabled = true;
            this.realmType.Location = new System.Drawing.Point(116, 40);
            this.realmType.Name = "realmType";
            this.realmType.Size = new System.Drawing.Size(121, 21);
            this.realmType.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 43);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Realm Script";
            // 
            // defaultWorldType
            // 
            this.defaultWorldType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.defaultWorldType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.defaultWorldType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.defaultWorldType.ForeColor = System.Drawing.Color.White;
            this.defaultWorldType.FormattingEnabled = true;
            this.defaultWorldType.Location = new System.Drawing.Point(116, 13);
            this.defaultWorldType.Name = "defaultWorldType";
            this.defaultWorldType.Size = new System.Drawing.Size(121, 21);
            this.defaultWorldType.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "World";
            // 
            // doorType
            // 
            this.doorType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.doorType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.doorType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.doorType.ForeColor = System.Drawing.Color.White;
            this.doorType.FormattingEnabled = true;
            this.doorType.Location = new System.Drawing.Point(116, 123);
            this.doorType.Name = "doorType";
            this.doorType.Size = new System.Drawing.Size(121, 21);
            this.doorType.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 126);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "Door Script";
            // 
            // roomType
            // 
            this.roomType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.roomType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.roomType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roomType.ForeColor = System.Drawing.Color.White;
            this.roomType.FormattingEnabled = true;
            this.roomType.Location = new System.Drawing.Point(116, 96);
            this.roomType.Name = "roomType";
            this.roomType.Size = new System.Drawing.Size(121, 21);
            this.roomType.TabIndex = 13;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 99);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 13);
            this.label12.TabIndex = 12;
            this.label12.Text = "Room Script";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.playerSavePath);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.worldFile);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.ForeColor = System.Drawing.Color.White;
            this.groupBox5.Location = new System.Drawing.Point(268, 222);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(244, 127);
            this.groupBox5.TabIndex = 17;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Save Information";
            // 
            // worldFile
            // 
            this.worldFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.worldFile.ForeColor = System.Drawing.Color.White;
            this.worldFile.Location = new System.Drawing.Point(111, 19);
            this.worldFile.Name = "worldFile";
            this.worldFile.ReadOnly = true;
            this.worldFile.Size = new System.Drawing.Size(127, 20);
            this.worldFile.TabIndex = 13;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 21);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(95, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "World Save Folder";
            // 
            // playerSavePath
            // 
            this.playerSavePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.playerSavePath.ForeColor = System.Drawing.Color.White;
            this.playerSavePath.Location = new System.Drawing.Point(111, 46);
            this.playerSavePath.Name = "playerSavePath";
            this.playerSavePath.ReadOnly = true;
            this.playerSavePath.Size = new System.Drawing.Size(127, 20);
            this.playerSavePath.TabIndex = 15;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 48);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(96, 13);
            this.label14.TabIndex = 14;
            this.label14.Text = "Player Save Folder";
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveSettings.Location = new System.Drawing.Point(23, 355);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(226, 23);
            this.btnSaveSettings.TabIndex = 20;
            this.btnSaveSettings.Text = "Save Settings";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(8, 70);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(229, 31);
            this.label15.TabIndex = 19;
            this.label15.Text = "Note: Leaving blank will save the files into the root server path.";
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(8, 40);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(229, 31);
            this.label16.TabIndex = 20;
            this.label16.Text = "Note: Leaving blank will result in the compiler starting its script scan from the" +
    " server root directory.";
            // 
            // btnCancelSettings
            // 
            this.btnCancelSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelSettings.Location = new System.Drawing.Point(279, 355);
            this.btnCancelSettings.Name = "btnCancelSettings";
            this.btnCancelSettings.Size = new System.Drawing.Size(226, 23);
            this.btnCancelSettings.TabIndex = 21;
            this.btnCancelSettings.Text = "Cancel";
            this.btnCancelSettings.UseVisualStyleBackColor = true;
            // 
            // frmEngineSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(520, 387);
            this.Controls.Add(this.btnCancelSettings);
            this.Controls.Add(this.btnSaveSettings);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEngineSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mud Designer Editor : Engine Settings";
            this.Load += new System.EventHandler(this.frmEngineSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox scriptLibrary;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox loginRoom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox loginSuccessState;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox initialState;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox defaultPlayerType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox defaultGameType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox scriptsPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnRemoveScriptLibrary;
        private System.Windows.Forms.Button btnAddScriptLibrary;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox doorType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox roomType;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox zoneType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox realmType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox defaultWorldType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox playerSavePath;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox worldFile;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.Button btnCancelSettings;
    }
}