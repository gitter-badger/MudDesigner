using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using MudEngine.GameManagement;
using MudEngine.FileSystem;

namespace MudDesigner
{
    public partial class frmDesigner : Form
    {
        private Game _Game;

        private Boolean _IsRenaming;
        private String _OldName;

        public frmDesigner(Game game)
        {
            InitializeComponent();

            _Game = game;

            MudEngine.GameObjects.Environment.Room r = new MudEngine.GameObjects.Environment.Room(_Game);
            this.propertyGrid1.SelectedObject = _Game;
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
    }
}
