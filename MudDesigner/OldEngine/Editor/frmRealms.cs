/* frmRealms
 * Product: Mud Designer Editor
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: Allows creating and editing Realms.
 */

//Microsoft .NET using statements
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//AllocateThis! Mud Designer using statements
using MudDesigner.Editor;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Scripting;

namespace MudDesigner.Editor
{
    public partial class frmRealms : Form
    {
        /// <summary>
        /// Gets or Sets if we are just using this editor to quickly change the currently
        /// loaded Realm. This will enable double-clicking the Realm and auto-closing the editor.
        /// </summary>
        internal bool ChangingRealm { get; set; }

        public frmRealms()
        {
            InitializeComponent();
        }

        private void frmRealms_Load(object sender, EventArgs e)
        {
            //Loop through all of the Realms in the current Game world
            //and place them into our List collection.
            foreach (IRealm realm in Editor.Game.World.GetRealms())
            {
                realmsLstExistingRealms.Items.Add(realm.Name);
            }

            //If we currently have a Realm loaded, we should select it by default
            if (Editor.CurrentRealm != null)
            {
                //If the currently loaded Realm still exists within our list collection,
                //select it
                if (realmsLstExistingRealms.Items.Contains(Editor.CurrentRealm.Name))
                    realmsLstExistingRealms.SelectedItem = Editor.CurrentRealm.Name;
            }

            //Update the UI.
            UpdateUI();
        }

        /// <summary>
        /// Updates the "Realm Properties" label to display the currently loaded Realms Type.
        /// </summary>
        void UpdateUI()
        {
            if (Editor.CurrentRealm != null)
                grpRealmProperties.Text = "Realm Properties (" + Editor.CurrentRealm.GetType().Name + ")";
        }

        private void realmsBtnAddRealm_Click(object sender, EventArgs e)
        {
            //We need to make sure we never have a duplicate name.
            int value = 1;
            string newName = "New Realm" + value; //New Realm1
            
            //Once we have a Realm with a unique name, this will become true
            bool validName = false; 

            //Loop until we have a valid Realm name that is not duplicated.
            while (!validName)
            {
                //In the event this is the first Realm.
                //Prevents infinit loop
                //Since we have zero Realms that exists, of course the first one will be valid.
                if (Editor.Game.World.GetRealms().Length == 0)
                    validName = true;

                //Loop through each Realm and check it's name
                foreach (var r in Editor.Game.World.GetRealms())
                {
                    //We found a match, so it's considered a duplicate.
                    if (r.Name == newName)
                    {
                        //Increase our number
                        value++;
                        //Update our name
                        newName = "New Realm" + value;
                    }
                    else
                    {
                        //Otherwise we are good to go
                        validName = true;
                    }
                }
            }

            //Grab a new instance of the default IRealm specified in the engine settings
            IRealm realm = (IRealm)ScriptFactory.GetScript(MudDesigner.Engine.Properties.EngineSettings.Default.RealmScript, null);

            if (realm == null)
            {
                //In the event the scripts didn't compile or are missing
                MessageBox.Show("There are currently no Realm scripts that exist.  Please create a script that inherits from MudDesigner.Engine.Environments.BaseRealm", "Mud Designer Editor : Realms", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Set the name we built
            realm.Name = newName;
            
            //Add it to the World
            Editor.Game.World.AddRealm(realm);

            //Add it and select it on our Realm List Collection
            realmsLstExistingRealms.Items.Add(realm.Name);
            realmsLstExistingRealms.SelectedItem = realm.Name;
        }

        private void realmsBtnDeleteRealm_Click(object sender, EventArgs e)
        {
            if (realmsLstExistingRealms.SelectedIndex == -1)
            {
                MessageBox.Show("You must select a Realm to delete first.", "Mud Designer Editor : Realms", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Editor.Game.World.RemoveRealm(realmsLstExistingRealms.SelectedItem.ToString());
            realmsLstExistingRealms.Items.Remove(realmsLstExistingRealms.SelectedItem);
            realmsProperties.SelectedObject = null;
            Editor.CurrentRealm = null;
        }

        private void realmsLstExistingRealms_SelectedIndexChanged(object sender, EventArgs e)
        {
            //If we clicked on the list collection but have nothing in it
            //it will be -1. Abort so we're not trying to reference a 
            //non-existant object from the collection
            if (realmsLstExistingRealms.SelectedIndex == -1)
                return;

            //Get a reference to the selected object within the World
            var realm = Editor.Game.World.GetRealm(realmsLstExistingRealms.SelectedItem.ToString());

            //If the object didn't exist for some reason, abort.
            if (realm == null)
                return;

            //Select it for editing.
            realmsProperties.SelectedObject = realm;
            Editor.CurrentRealm = realm;

            UpdateUI();
        }

        private void realmsLstExistingRealms_DoubleClick(object sender, EventArgs e)
        {
            //If we are just changing the realm (launched from another editor)
            //we want to run the Realm selection code real-quick and close ourself
            if (ChangingRealm)
            {
                realmsLstExistingRealms_SelectedIndexChanged(sender, e);
                this.Close();
            }
        }

        private void realmsProperties_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            //If the Realms name changed, update the labels on the editor UI
            if (e.ChangedItem.Label == "Name")
            {
                realmsLstExistingRealms.Items[realmsLstExistingRealms.Items.IndexOf(e.OldValue)] = Editor.CurrentRealm.Name;
            }
        }
    }
}
