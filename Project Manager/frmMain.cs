using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project_Manager
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            propertyGrid1.SelectedObject = Program.project;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //Get all of the realms currently created.
            string[] realms = System.IO.Directory.GetFiles(Application.StartupPath + @"\Data\Realms");

            //Add each realm found into the combo box of available realms.
            foreach (string realm in realms)
            {
                //Instance a new realm
                MUDEngine.Environment.Realm newRealm = new MUDEngine.Environment.Realm();
                //De-serialize the current realm.
                newRealm = (MUDEngine.Environment.Realm)MUDEngine.XmlSerialization.Load(realm, newRealm);
                //Add it to the available realms combo box.
                comRealms.Items.Add(newRealm.Name);
            }

            //If the project already has a starting realm, then select it.
            if (Program.project.InitialLocation.Realm != null)
            {
                comRealms.SelectedIndex = comRealms.Items.IndexOf(Program.project.InitialLocation.Realm.Name);
            }
                //If there is no starting realm, but a realm does exist, select the first one in the list.
            else if (comRealms.Items.Count != 0)
            {
                comRealms.SelectedIndex = 0;
            }
        }
    }
}
