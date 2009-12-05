using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MudDesigner.MudEngine.FileSystem;
using MudDesigner.MudEngine.Objects;
using MudDesigner.MudEngine.Objects.Environment;

namespace MudDesigner.Editors
{
    public partial class ExistingRealms : Form
    {
        Zone _Zone;

        public ExistingRealms(string Zone)
        {
            InitializeComponent();
            _Zone = new Zone();
            string filename = System.IO.Path.Combine(FileManager.GetDataPath(SaveDataTypes.Zones), Zone + ".zone");
            _Zone = (Zone)FileManager.Load(filename, _Zone);

            string[] realms = System.IO.Directory.GetFiles(FileManager.GetDataPath(SaveDataTypes.Realms), "*.realm");
            foreach (string file in realms)
            {
                Realm realm = new Realm();
                realm = (Realm)FileManager.Load(file, realm);
                lstRealms.Items.Add(realm.Name);
            }
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (lstRealms.SelectedItem.ToString() == Program.Realm.Name)
            {
                MessageBox.Show("The zone already belongs to this realm! Transfer canceled.", "Realm Explorer");
                return;
            }

            RealmExplorer form = (RealmExplorer)Program.CurrentEditor;
            string zonePath = FileManager.GetDataPath(SaveDataTypes.Zones);
            string zoneFile = System.IO.Path.Combine(zonePath, form.lstZones.SelectedItem.ToString() + ".zone");
            Program.Zone = (Zone)FileManager.Load(zoneFile, Program.Zone);

            //Make our changes to the old realm and zone
            foreach (Zone zone in Program.Realm.Zones)
            {
                if (zone.Name == Program.Zone.Name)
                {
                    Program.Realm.Zones.Remove(zone);
                    break;
                }
            }
            form.lstZones.Items.Remove(form.lstZones.SelectedItem);
            Program.Zone.Realm = lstRealms.SelectedItem.ToString();

            //Save old realm and the zone
            FileManager.Save(zoneFile, Program.Zone);
            string realmPath = FileManager.GetDataPath(SaveDataTypes.Realms);
            string realmFile = System.IO.Path.Combine(realmPath, Program.Realm.Filename);
            FileManager.Save(realmFile, Program.Realm);

            //edit the new realm
            realmFile = System.IO.Path.Combine(realmPath, lstRealms.SelectedItem.ToString() + ".realm");
            Realm realm = new Realm();
            realm = (Realm)FileManager.Load(realmFile, realm);

            realm.Zones.Add(Program.Zone);
            FileManager.Save(realmFile, realm);
            Program.Zone = new Zone();
            this.Close();
        }
    }
}
