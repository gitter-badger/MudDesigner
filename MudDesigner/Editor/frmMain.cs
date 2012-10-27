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
            CompileEngine.Compile(EngineSettings.Default.ScriptsPath);

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
            var game = (IGame)ScriptFactory.FindInheritedScripted("MudDesigner.Engine.Core.Game", null);
            
            IServer server = new Server(4000);
            
            //Initialize the Game. Null reference to a server is passed because we don't need a server.
            game.Initialize(server);
            
            //Load the save game file.
            game.Load();

            EngineEditor.Game = game;

            mainPropertyGame.SelectedObject = EngineEditor.Game;
            mainPropertyServer.SelectedObject = EngineEditor.Game.Server;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Logger.Cache.Count == 0)
                return;

            foreach (string message in Logger.Cache)
            {
                mainTxtServerInfo.Text += message + "\n";
                mainTxtServerInfo.Select(mainTxtServerInfo.Text.Length, 0);
            }

            //Reset for the next time we queury
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
                    break;
                case false:
                    mainTxtServerInfo.Text += "======== Server Starting ========\n\n";
                    var server = EngineEditor.Game.Server;
                    server.Start(server.MaxConnections, server.MaxQueuedConnections, server.Game);
                    menuStartStopServer.Text = "Stop Server";
                    break;
            }
        }

        private void menuRealms_Click(object sender, EventArgs e)
        {
            Environment.frmRealms realms = new Environment.frmRealms();

            realms.ShowDialog();
            while (realms.Visible)
            {
                Application.DoEvents();
            }
            realms = null;
        }
    }
}
