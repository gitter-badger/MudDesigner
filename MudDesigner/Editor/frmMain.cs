using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Properties;
using MudDesigner.Engine.Scripting;
using MudDesigner.Engine.Networking;

namespace MudDesigner.Editor
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        //
        private void frmMain_Load(object sender, EventArgs e)
        {
            //Setup our logger so we can access it within the editor.
            Logger.CacheContent = true;
            Logger.Enabled = true;

            //Add the engine to the compiler for referencing.
            CompileEngine.AddAssemblyReference("MudDesigner.Engine.dll");
            
            //Loop through each reference mentioned in the engines properties and add them.
            //This provides support for 3rd party pre-compiled *mods* scripts
            foreach(string reference in EngineSettings.Default.ScriptLibrary)
            {
                string path = Path.Combine(System.Environment.CurrentDirectory, reference);
                CompileEngine.AddAssemblyReference(path);
            }

            //Compile the scripts.
            bool result = CompileEngine.Compile(EngineSettings.Default.ScriptsPath);
            if (!result)
            {
                this.mainTxtServerInfo.Text = CompileEngine.Errors;
            }

            //Add the compiled script assembly to the script factory.
            ScriptFactory.AddAssembly(CompileEngine.CompiledAssembly);
            
            //Add the third party references.
            foreach (string reference in EngineSettings.Default.ScriptLibrary)
            {
                ScriptFactory.AddAssembly(reference);
            }

            if (File.Exists("MudDesigner.Scripts.dll"))
            {
                string path = Path.Combine(System.Environment.CurrentDirectory, "MudDesigner.Scripts.dll");
                ScriptFactory.AddAssembly(Assembly.LoadFile(path));
            }

            //Get a reference to a scripted instance of IGame.
            var game = (IGame)ScriptFactory.GetScript(EngineSettings.Default.GameScript, null);
            
            //In the event that the scripted Game class specified as the default is missing,
            //just search the engine for another one.
            if (game == null)
            {
                game = (IGame)ScriptFactory.FindInheritedScript("MudDesigner.Engine.Core.Game", null);

                if (game == null) //still could not find anything
                {
                    MessageBox.Show("Critical error: Could not locate a Game script to use. This can cause instability in the editor.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteLine(CompileEngine.Errors, Logger.Importance.Critical);

                    MessageBox.Show("The editor can not run without a script present that inherits from MudDesigner.Engine.Core.Game.  The editor will now shut down.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }
            IServer server = new Server(4000);
            
            //Initialize the Game. Null reference to a server is passed because we don't need a server.
            game.Initialize(server);
            
            //Load the save game file.
            game.RestoreWorld();

            EngineEditor.Game = game;

            //Update the GUI
            mainPropertyGame.SelectedObject = EngineEditor.Game;
            mainPropertyServer.SelectedObject = EngineEditor.Game.Server;
            RefreshUI();
        }

        private void RefreshUI()
        {
            lblProjectName.Text = EngineEditor.Game.Name;
            grpGameSettings.Text = "Game Settings (" + EngineEditor.Game.GetType().Name + ")";
            grpServerSettings.Text = "Server Settings (" + EngineEditor.Game.Server.GetType().Name + ")";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Logger.Cache.Count == 0)
                return;

            int cacheSize = Logger.Cache.Count;

            foreach (string message in Logger.Cache)
            {
                mainTxtServerInfo.Text += message + "\n";

                //Auto-scroll to the last line.
                mainTxtServerInfo.SelectionStart = mainTxtServerInfo.Text.Length;
                mainTxtServerInfo.ScrollToCaret();
                mainTxtServerInfo.Refresh();

                //Fail safe in the event that the collection was changed in the middle of our loop.
                if (cacheSize != Logger.Cache.Count)
                    break;
            }

            //Reset for the next time we queury
            if (cacheSize != Logger.Cache.Count)
                return; //We want to pick back up where we left off last loop.
            else //If we printed all the contents already, then clear them
                Logger.Cache.Clear();
        }

        private void menuStartStopServer_Click(object sender, EventArgs e)
        {
            switch (EngineEditor.Game.Server.Enabled)
            {
                case true:
                    mainTxtServerInfo.Text += "\n======== Server Stopped ========\n\n";
                    EngineEditor.Game.Server.Stop();
                    menuStartStopServer.Text = "Start Server";
                    timerLogger.Enabled = false;
                    break;
                case false:
                    timerLogger.Enabled = true;
                    mainTxtServerInfo.Text += "======== Server Starting ========\n\n";
                    var server = EngineEditor.Game.Server;
                    server.Start(server.MaxConnections, server.MaxQueuedConnections, EngineEditor.Game);
                    menuStartStopServer.Text = "Stop Server";
                    break;
            }
        }

        private void menuRealms_Click(object sender, EventArgs e)
        {
            frmRealms realms = new frmRealms();

            realms.ShowDialog();
            while (realms.Visible)
            {
                Application.DoEvents();
            }
            realms = null;
        }

        private void menuZones_Click(object sender, EventArgs e)
        {
            frmZones zones = new frmZones();
            zones.ShowDialog();

            while (zones.Visible)
            {
                Application.DoEvents();
            }
            zones = null;
        }

        private void menuSave_Click(object sender, EventArgs e)
        {
            EngineEditor.Game.SaveWorld();
        }

        private void menuRooms_Click(object sender, EventArgs e)
        {
            Rooms.frmRooms rooms = new Rooms.frmRooms();
            rooms.ShowDialog();

            while (rooms.Visible)
                Application.DoEvents();
            rooms = null;
        }

        private void mainPropertyGame_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "Name")
            {
                lblProjectName.Text = EngineEditor.Game.Name;
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (EngineEditor.Game.Server.Enabled)
            {
                DialogResult serverResults = MessageBox.Show("You are currently running the game server, are you sure you want to quit?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                switch (serverResults)
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        menuStartStopServer_Click(null, null);
                        break;
                    case System.Windows.Forms.DialogResult.No:
                        e.Cancel = true; //cancel killing the app.
                        return;
                }
            }

            DialogResult saveResults = MessageBox.Show("Would you like to save prior to closing?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            switch (saveResults)
            {
                case System.Windows.Forms.DialogResult.Yes:
                    EngineEditor.Game.SaveWorld();
                    break;
                case System.Windows.Forms.DialogResult.No:
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }

        private void engineSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EngineEditor.Game.Server.Enabled)
            {
                MessageBox.Show("You can not change settings while the server is running!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            frmEngineSettings engineSettings = new frmEngineSettings();
            engineSettings.ShowDialog();

            while (engineSettings.Visible)
            {
                Application.DoEvents();
            }

            engineSettings = null;
            RefreshUI();
        }
    }
}
