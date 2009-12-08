using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MudDesigner.MudEngine.FileSystem;
using MudDesigner.MudEngine.GameObjects;
using MudDesigner.MudEngine.GameObjects.Environment;

namespace MudDesigner.Editors
{
    public partial class ExistingRealms : Form
    {
        List<Realm> realms = new List<Realm>();
        internal string _RealmFilename = "";
        internal string _RealmName = "";

        public ExistingRealms()
        {
            InitializeComponent();

            string realmRoot = FileManager.GetDataPath(SaveDataTypes.Realms);
            string[] realmFiles = Directory.GetFiles(realmRoot, "*.realm", SearchOption.AllDirectories);

            foreach (string file in realmFiles)
            {
                Realm r = new Realm();
                r = (Realm)FileManager.Load(file, r);
                realms.Add(r);
            }

            foreach (Realm realm in realms)
            {
                lstRealms.Items.Add(realm.Name);
            }
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lstRealms_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Realm realm in realms)
            {
                if (realm.Name == lstRealms.SelectedItem.ToString())
                {
                    _RealmFilename = realm.Filename;
                    _RealmName = realm.Name;
                }
            }
        }
    }
}
