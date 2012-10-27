using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MudDesigner.Editor;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Scripting;

namespace MudDesigner.Editor.Environment
{
    public partial class frmRealms : Form
    {
        public frmRealms()
        {
            InitializeComponent();
        }

        private void realmsBtnDeleteRealm_Click(object sender, EventArgs e)
        {
            if (realmsLstExistingRealms.SelectedIndex == -1)
            {
                MessageBox.Show("You must select a Realm to delete first.", "Mud Designer Editor : Realms", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            EngineEditor.Game.World.RemoveRealm(realmsLstExistingRealms.SelectedItem.ToString());
            realmsLstExistingRealms.Items.Remove(realmsLstExistingRealms.SelectedItem);
            realmsProperties.SelectedObject = null;
            EngineEditor.CurrentRealm = null;
        }

        private void realmsBtnAddRealm_Click(object sender, EventArgs e)
        {
            //We need to make sure we never have a duplicate name.
            int value = 1;
            string newName = "New Realm" + value;
            bool validName = false;

            while (!validName)
            {
                //In the event this is the first Realm.
                //Prevents infinit loop
                if (EngineEditor.Game.World.Realms.Count == 0)
                    validName = true;

                foreach (var r in EngineEditor.Game.World.Realms)
                {
                    if (r.Value.Name == newName)
                    {
                        value++;
                        newName = "New Realm" + value;
                    }
                    else
                    {
                        validName = true;
                    }
                }
            }

            IRealm realm = (IRealm)ScriptFactory.GetScript(MudDesigner.Engine.Properties.EngineSettings.Default.RealmType, null);
            realm.Name = newName;
            EngineEditor.Game.World.AddRealm(realm);

            realmsLstExistingRealms.Items.Add(realm.Name);
            realmsLstExistingRealms.SelectedItem = realm.Name;
        }

        private void realmsLstExistingRealms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (realmsLstExistingRealms.SelectedIndex == -1)
                return;

            var realm = EngineEditor.Game.World.GetRealm(realmsLstExistingRealms.SelectedItem.ToString());

            if (realm == null)
                return;

            realmsProperties.SelectedObject = realm;
            EngineEditor.CurrentRealm = realm;
        }

        private void frmRealms_Load(object sender, EventArgs e)
        {
            foreach (IRealm realm in EngineEditor.Game.World.Realms.Values)
            {
                realmsLstExistingRealms.Items.Add(realm.Name);
            }
        }
    }
}
