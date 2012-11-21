/* frmMain
 * Product: Mud Designer Editor
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: Provides access to Game and Server settings as well as access to all of the 
 *                   editors that make up the tool kit.
 */

//Microsoft .NET using statements
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

//AllocateThis! Mud Designer using statements
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Properties;
using MudDesigner.Engine.Scripting;
using MudDesigner.Engine.Networking;
using log4net;

namespace MudDesigner.Editor
{
    public partial class frmMain : Form
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(frmMain)); 
        public frmMain()
        {
            //EngineSettings.Default.Reset();

            InitializeComponent();
        }
        
        private void frmMain_Load(object sender, EventArgs e)
        {
            //Setup our logger so we can access it within the editor.
            //Logger.CacheContent = true;
            //Logger.Enabled = true;
            
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
                    Log.Error(string.Format("{0}", CompileEngine.Errors));
                    //Logger.WriteLine(CompileEngine.Errors, Logger.Importance.Critical);

                    menuWorld.Enabled = false;
                    menuGame.Enabled = false;
                    menuSave.Enabled = false;

                    return;
                }
            }
            IServer server = new Server(4000);
            
            //Initialize the Game. Null reference to a server is passed because we don't need a server.
            game.Initialize(server);
            
            //Load the save game file.
            game.RestoreWorld();

            //Store a reference to the current game to the static Editor Type
            Editor.Game = game;

            //Update the GUI
            mainPropertyGame.SelectedObject = Editor.Game;
            mainPropertyServer.SelectedObject = Editor.Game.Server;
            RefreshUI();
        }

        private void RefreshUI()
        {
            lblProjectName.Text = Editor.Game.Name;
            grpGameSettings.Text = "Game Settings (" + Editor.Game.GetType().Name + ")";
            grpServerSettings.Text = "Server Settings (" + Editor.Game.Server.GetType().Name + ")";
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuSave_Click(object sender, EventArgs e)
        {
            //Save the game world.
            if (Editor.Game != null)
                Editor.Game.SaveWorld();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //If we have no new log messages, abort.
            if(MenuAppender.MessageCache.Count ==0)
               return;

            int cacheSize = MenuAppender.MessageCache.Count;

            //Loop through each cached log message and send it to the 
            //server console text box.
            foreach (string message in MenuAppender.MessageCache)
            {
                mainTxtServerInfo.Text += message + "\n";

                //Auto-scroll to the last line.
                mainTxtServerInfo.SelectionStart = mainTxtServerInfo.Text.Length;
                mainTxtServerInfo.ScrollToCaret();
                mainTxtServerInfo.Refresh();

                //Fail safe in the event that the collection was changed in the middle of our loop.
                if (cacheSize != MenuAppender.MessageCache.Count)
                    break;
            }

            //Reset for the next time we queury
            if (cacheSize != MenuAppender.MessageCache.Count)
                return; //We want to pick back up where we left off last loop.
            else //If we printed all the contents already, then clear them
                MenuAppender.MessageCache.Clear();
        }

        private void menuStartStopServer_Click(object sender, EventArgs e)
        {
            //Check if the server is enabled or disabled.
            switch (Editor.Game.Server.Enabled)
            {
                    //Server is enabled, so we will stop it.
                case true:
                    mainTxtServerInfo.Text += "\n======== Server Stopped ========\n\n";
                    Editor.Game.Server.Stop(); //Stops the server
                    //Reset the UI so users know they can start it again.
                    menuStartStopServer.Text = "Start Server";
                    //No need to grab engine log messages anymore.
                    //TODO: - We might want to just keep this enabled
                    //so we can catch errors when editing objects.
                    timerLogger.Enabled = false;
                    break;
                    //Server is disabled, so we will start it.
                case false:
                    timerLogger.Enabled = true;
                    mainTxtServerInfo.Text += "======== Server Starting ========\n\n";
                    //Get a reference to the server
                    var server = Editor.Game.Server;
                    //Start the server
                    server.Start(server.MaxConnections, server.MaxQueuedConnections, Editor.Game);
                    //Update the UI so users know they can stop it.
                    menuStartStopServer.Text = "Stop Server";
                    break;
            }
        }

        private void menuRealms_Click(object sender, EventArgs e)
        {
            //Create a new instance of the Realms editor.
            frmRealms realms = new frmRealms();

            //Show it as a dialog. This prevents other editors from starting at the same time.
            //Ensures that the static Editor Type will only be accessed by one editor at a time.
            realms.ShowDialog();

            //While the editor is visible, just keep the App responsive.
            while (realms.Visible)
            {
                Application.DoEvents();
            }

            //Null the reference we have.
            realms = null;
        }

        private void menuZones_Click(object sender, EventArgs e)
        {
            //Create a new instance of the Zones editor
            frmZones zones = new frmZones();

            //Show it as a dialog. This prevents other editors from starting at the same time.
            //Ensures that the static Editor Type will only be accessed by one editor at a time.
            zones.ShowDialog();

            //While the editor is visible, just keep the App responsive.
            while (zones.Visible)
            {
                Application.DoEvents();
            }

            //Null the reference we have.
            zones = null;
        }

        private void menuRooms_Click(object sender, EventArgs e)
        {
            //Creates
            Rooms.frmRooms rooms = new Rooms.frmRooms();
            rooms.ShowDialog();

            while (rooms.Visible)
                Application.DoEvents();
            rooms = null;
        }


        private void mainPropertyGame_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            //We need to update the Game Name label on the Editor if the user changes
            //the name of the Game.
            if (e.ChangedItem.Label == "Name")
            {
                lblProjectName.Text = Editor.Game.Name;
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Editor.Game == null)
                return;

            //If the server is currently running...
            if (Editor.Game.Server.Enabled)
            {
                //Tell the user the server is running and ask if they are sure they want to quit.
                DialogResult serverResults = MessageBox.Show("You are currently running the game server, are you sure you want to quit?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                switch (serverResults)
                {
                        //If yes, stop the server.
                    case System.Windows.Forms.DialogResult.Yes:
                        menuStartStopServer_Click(null, null);
                        break;
                        //If no, cancel closing the editor and resume.
                    case System.Windows.Forms.DialogResult.No:
                        e.Cancel = true;
                        return;
                }
            }

            //Ask if we should save prior to closing.
            DialogResult saveResults = MessageBox.Show("Would you like to save prior to closing?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            switch (saveResults)
            {
                    //If yes, save the game world
                case System.Windows.Forms.DialogResult.Yes:
                    Editor.Game.SaveWorld();
                    break;
                    //If no, ignore
                case System.Windows.Forms.DialogResult.No:
                    break;
                    //If cancel, abort closing the editor
                case System.Windows.Forms.DialogResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }

        private void mnuEngineSettings_Click(object sender, EventArgs e)
        {
            //If the server is running, don't allow editing of the Engine Settings.
            //This can cause instability and corruption.
            if (Editor.Game.Server.Enabled)
            {
                MessageBox.Show("You can not change settings while the server is running!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //Create a instance of the Engine Settings editor
            frmEngineSettings engineSettings = new frmEngineSettings();

            //Show it as a dialog. This prevents other editors from starting at the same time.
            //Ensures that the static Editor Type will only be accessed by one editor at a time.
            engineSettings.ShowDialog();

            //While the editor is visible, just keep the App responsive.
            while (engineSettings.Visible)
            {
                Application.DoEvents();
            }

            //Null the reference we have.
            engineSettings = null;

            //Refresh the UI in the event the Game or Server types were changed.
            RefreshUI();
        }
    }
}
