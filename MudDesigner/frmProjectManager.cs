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
        Client client;
        Thread r;

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

            RefreshProjects();

            client = new Client();
            client.Initialize("localhost", 555);

            comServerType.Items.Add("Local Server");
            comServerType.Items.Add("Test Server");
            comServerType.SelectedIndex = 0;
        }

        private void RefreshProjects()
        {
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
            frmDesigner form = new frmDesigner(_Game, client);

            form.Show();
            this.Hide();

            if (comServerType.SelectedItem.ToString() == "Test Server")
            {
            }
            else
            {
                frmInputBox input = new frmInputBox("Enter the Port that your local server is currently running on.");

                input.ShowDialog();

                if (input.IsCancel)
                    return;

                client.Initialize("localhost", Convert.ToInt32(input.Input));
                
                if (!client.Connect() || !client.Send("hello", false)) // test send + client data
                {
                    MessageBox.Show("Failed to connect to a local server. Is the server running?", "Mud Designer");
                    return;
                }
            }

            while (form.Visible)
            {
                Application.DoEvents();
            }

            //Refresh the project list incase the project was renamed.
            lstProjects.Items.Clear();

            RefreshProjects();

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
