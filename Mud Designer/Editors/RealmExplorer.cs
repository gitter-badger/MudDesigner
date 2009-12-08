using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ManagedScripting;
//MudEngine
using MudDesigner.MudEngine;
using MudDesigner.MudEngine.Attributes;
using MudDesigner.MudEngine.FileSystem;
using MudDesigner.MudEngine.GameObjects;
using MudDesigner.MudEngine.GameObjects.Environment;


namespace MudDesigner.Editors
{
    public partial class RealmExplorer : Form
    {
        List<Realm> realms;
        List<Zone> zones;
        List<Room> rooms;

        public RealmExplorer()
        {
            //build our UI
            InitializeComponent();

            //instance our collections
            realms = new List<Realm>();
            zones = new List<Zone>();
            rooms = new List<Room>();

            //instance the environments
            Program.Zone = new Zone();
            Program.Realm = new Realm();
            Program.Room = new Room();

            //instance the script engine
            Program.ScriptEngine = new ScriptingEngine();
            Program.ScriptEngine.CompileStyle = ManagedScripting.Compilers.BaseCompiler.ScriptCompileStyle.CompileToMemory;
            Program.ScriptEngine.KeepTempFiles = false;

            //Build an array of realm directories
            string[] directories = System.IO.Directory.GetDirectories(FileManager.GetDataPath(SaveDataTypes.Realms));
            //loop through each realm folder and get it's realm file
            foreach (string dir in directories)
            {
                //instance a new realm
                Realm r = new Realm();
                
                //Split the path to the realm folder into an array
                //so we can get the final folder name (our realm directory)
                string[] folders = dir.Split('\\');

                //Build our realm file paths.
                if (!folders.Length.Equals(0))
                {
                    string realmFile = folders[folders.Length - 1] + ".realm";
                    string realmPath = System.IO.Path.Combine(dir, realmFile);

                    //if the realm path exists, load the realm and add it to the listbox
                    if (System.IO.File.Exists(realmPath))
                    {
                        r = (Realm)FileManager.Load(realmPath, r);
                        realms.Add(r);
                        lstRealms.Items.Add(r.Name);
                    }
                    else
                    {
                        MessageBox.Show("Failed to load Realm '" + System.IO.Path.GetFileNameWithoutExtension(realmFile)
                            + "'.\nThis error is generated due to the Realm folder existing, but the Realm file missing.", "Realm Explorer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }
        }

        internal bool RealmExists(string realm)
        {
            string realmPath = Path.Combine(FileManager.GetDataPath(SaveDataTypes.Realms), realm);
            if (Directory.Exists(realm))
                return true;
            else
                return false;
        }

        private void btnNewRealm_Click(object sender, EventArgs e)
        {
            //Reinstance all of our environments
            Program.Realm = new Realm();
            Program.Zone = new Zone();
            Program.Room = new Room();
 
            propertyRealm.SelectedObject = Program.Realm;
            txtScript.Text = Program.Realm.Script;
        }

        private void btnSaveRealm_Click(object sender, EventArgs e)
        {
            //get our paths first.
            string realmPath = System.IO.Path.Combine(FileManager.GetDataPath(SaveDataTypes.Realms), Program.Realm.Name);
            string realmFile = System.IO.Path.Combine(realmPath, Program.Realm.Filename);

            //check if the directory exists
            if (!RealmExists(Program.Realm.Name))
                System.IO.Directory.CreateDirectory(realmPath);

            //save the realm
            FileManager.Save(realmFile, Program.Realm);

            //add it to the list box if it isn't already there
            if (!lstRealms.Items.Contains(Program.Realm.Name))
                lstRealms.Items.Add(Program.Realm.Name);

            //sets to true if the realm already exists (incase we are editing)
            bool realmFound = false;
            foreach (Realm r in realms)
            {
                //if the current realm in the loop matches our currently loaded realm
                if (r.Name == Program.Realm.Name)
                {
                    //we found it.
                    realmFound = true;
                    break;
                }
            }

            //if the currently loaded room is already in our collection
            //don't add it again.
            if (!realmFound)
                realms.Add(Program.Realm);

            //Select our new realm in the listbox
            lstRealms.SelectedIndex = lstRealms.Items.IndexOf(Program.Realm.Name);
        }

        private void btnDeleteRealm_Click(object sender, EventArgs e)
        {
            //Make sure we have our realm selected
            if (lstRealms.SelectedIndex == -1)
            {
                MessageBox.Show("Select a Realm to delete first!", "Realm Exporer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //Ask to make sure its ok to delete the realm and all of its
            //zones/rooms that are contained within it.
            DialogResult result = MessageBox.Show("Are you sure you want to delete the " + lstRealms.SelectedItem.ToString() + " Realm?\n\nWarning! All Zones & Rooms contained within this Realm will be deleted!",
                "Realm Explorer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //cancel the delete
            if (result == DialogResult.No)
                return;

            //get our paths
            string realmPath = Path.Combine(FileManager.GetDataPath(SaveDataTypes.Realms), Program.Realm.Name);
            
            //Delete the realm and all of its contents (zones/rooms)
            Directory.Delete(realmPath, true);            

            //remove it from the listbox
            lstRealms.Items.Remove(lstRealms.SelectedItem);
            //clear the room
            Program.Realm = new Realm();
            propertyRealm.SelectedObject = null;
        }
        private void btnLoadRealm_Click(object sender, EventArgs e)
        {
            //Incase an item was selected & removed from the listbox
            //this event gets triggered, even though nothing is selected.
            if (lstRealms.SelectedIndex == -1)
                return;

            //Loop through the realms collection to find the selected realm
            foreach (Realm r in realms)
            {
                //check if we have a match
                if (r.Name == lstRealms.SelectedItem.ToString())
                {
                    //load it.
                    Program.Realm = r;
                    txtScript.Text = Program.Realm.Script;
                    propertyRealm.SelectedObject = r;
                    break;
                }
            }

            //now that the realm is loaded, we need to loop through each
            //zone within the realm, and load it's rooms, scanning for available
            //exists for the realm to use.
            foreach (string zone in Program.Realm.Zones)
            {
                Zone z = new Zone();
                z = Program.Realm.GetZone(zone);
            }
        }

        private void txtScript_TextChanged(object sender, EventArgs e)
        {
            Program.Realm.Script = txtScript.Text;
        }
    }
}
