using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using MudEngine.GameManagement;
using MudEngine.FileSystem;

namespace MudDesigner
{
    public partial class frmDesigner : Form
    {
        private Game _Game;
        private Client _Client;
        private Thread _ClientThread;

        private Boolean _IsRenaming;
        private String _OldName;

        private Boolean _TimeOut;
        private Object _SelectedObject;

        public frmDesigner(Game game, Client client)
        {
            InitializeComponent();

            _Game = game;
            _Client = client;

            MudEngine.GameObjects.Environment.Room r = new MudEngine.GameObjects.Environment.Room(_Game);
            this.propertyGrid1.SelectedObject = _Game;

            _TimeOut = false;
            _Client.Send("Hello world", false);
        }

        /// <summary>
        /// Saves the project.
        /// </summary>
        public void Save()
        {
            if ((String.IsNullOrEmpty(_Game.GameTitle)) || (_Game.GameTitle == new Game().GameTitle))
                return;

            if ((_IsRenaming) && (Directory.Exists(Path.Combine("Projects", _OldName))))
            {
                Directory.Delete(Path.Combine("Projects", _OldName), true);
                File.Delete(Path.Combine(Environment.CurrentDirectory, _OldName + ".ini"));
            }
            _Game.Save();
        }

        private void frmDesigner_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Save Changes?", this.Text, MessageBoxButtons.YesNoCancel);

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }
            else if (result == System.Windows.Forms.DialogResult.Yes)
                this.Save();
            
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "GameTitle")
            {
                String Env = _Game.DataPaths.Environment.Replace(e.OldValue.ToString(), e.ChangedItem.Value.ToString());
                String plyr = _Game.DataPaths.Players.Replace(e.OldValue.ToString(), e.ChangedItem.Value.ToString());

                _Game.DataPaths = new SaveDataPaths(Env, plyr);
                
                _IsRenaming = true;
                _OldName = e.OldValue.ToString();
            }
        }

        private void ChangeObject(Object obj)
        {
            _SelectedObject = obj;
            propertyGrid1.SelectedObject = obj;
        }

        private void btnNewRealm_Click(object sender, EventArgs e)
        {
            ChangeObject(new MudEngine.GameObjects.Environment.Realm(_Game));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangeObject(new MudEngine.GameObjects.Environment.Zone(_Game));
        }

        private void txtCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _Client.Send(txtCommand.Text, true);

                System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
                t.Interval = 10000;
                t.Tick += new EventHandler(timerTick);

                _TimeOut = false;
                String result;

                while (!_TimeOut)
                {
                    if (_Client.Receive(out result, 1))
                    {
                        txtConsole.Text += result;
                    }
                    else
                        _TimeOut = true;
                }

                txtConsole.Text += "\n";
            }
        }

        void timerTick(object sender, EventArgs e)
        {
            _TimeOut = true;
        }

        void UpdateConsole(String message)
        {
            txtConsole.Text += message;
        }
    }
}
