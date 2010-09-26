using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using MudEngine.FileSystem;
using MudEngine.GameManagement;
using MudEngine.Scripting;

namespace MudDesigner
{
    public partial class frmProjectManager : Form
    {
        String[] _ProjectFiles;
        String _ProjectPath;
        String _ScriptPath;
        const String SettingsFile = "Settings.ini";

        dynamic _Game;
        ScriptEngine _ScriptEngine;

        public frmProjectManager()
        {
            InitializeComponent();

            _ProjectPath = Path.Combine(Environment.CurrentDirectory, "Projects");
            _ScriptPath = Path.Combine(Environment.CurrentDirectory, "Scripts");

            if (!Directory.Exists(_ProjectPath))
                Directory.CreateDirectory(_ProjectPath);

            if (!Directory.Exists(_ScriptPath))
                Directory.CreateDirectory(_ScriptPath);
            
            if (!File.Exists(SettingsFile))
            {
                Log.Write("Settings.ini missing!", false);
                FileManager.WriteLine(SettingsFile, "Scripts", "ScriptPath");
                FileManager.WriteLine(SettingsFile, ".cs", "ScriptExtension");
                FileManager.WriteLine(SettingsFile, "True", "ServerEnabled");
            }

            _ScriptEngine = new ScriptEngine(new Game(), ScriptEngine.ScriptTypes.Both);
            _ScriptEngine.Initialize();

            GameObject go = _ScriptEngine.GetObject("Game");

            if (go == null)
            {
                _Game = new Game();
                go = new GameObject(_Game, "Game");
                _ScriptEngine = new ScriptEngine(_Game, ScriptEngine.ScriptTypes.Both);
            }
            else
            {
                _Game = (Game)go.Instance;
                _ScriptEngine = new ScriptEngine(_Game, ScriptEngine.ScriptTypes.Both);
            }

            //TODO: Do I need to Re-initialize _ScriptEngine?

            _ProjectFiles = Directory.GetFiles(Environment.CurrentDirectory, "*.ini");

            foreach (String filename in _ProjectFiles)
            {
                if (Path.GetFileNameWithoutExtension(filename).ToLower() == "settings")
                    continue;

                _Game.Load(filename);

                lstProjects.Items.Add(_Game.GameTitle);
            }
        }

        private void btnNewProject_Click(object sender, EventArgs e)
        {
            frmInputBox input = new frmInputBox("Enter the name of your project.");
            input.ShowDialog();

            if (input.IsCancel)
                return;
            else if (String.IsNullOrEmpty(input.Input))
                return;

            lstProjects.Items.Add(input.Input);

            _Game.GameTitle = input.Input;
            //Setup save data paths.
            _Game.DataPaths = new SaveDataPaths(Path.Combine("Projects", _Game.GameTitle, _Game.DataPaths.Environment), Path.Combine("Projects", _Game.GameTitle, _Game.DataPaths.Players));

            _Game.Save();

            input = null;

            ShowDesigner();
        }

        private void ShowDesigner()
        {
            frmDesigner form = new frmDesigner(_Game);

            form.Show();
            this.Hide();
            while (form.Visible)
            {
                Application.DoEvents();
            }

            this.Show();
            form = null;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void editProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(lstProjects.SelectedItem.ToString() + ".ini"))
                _Game.Load(lstProjects.SelectedItem.ToString() + ".ini");

            ShowDesigner();   
        }
    }
}
