using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MUDEngine.Objects.Environment;
using MUDEngine;

namespace RealmExplorer
{
    public partial class frmMain : Form
    {
        Zone _Zone;
        Realm _Realm;
        List<Zone> _AvailableZones;

        public frmMain()
        {
            InitializeComponent();
            _Zone = new Zone();
            _Realm = new Realm();
            _AvailableZones = new List<Zone>();

            string[] zones = System.IO.Directory.GetFiles(Engine.GetDataPath(Engine.SaveDataTypes.Zones), "*.zone");
            bool available = true;

            foreach (string zone in zones)
            {
                string[] realms = System.IO.Directory.GetFiles(Engine.GetDataPath(Engine.SaveDataTypes.Realms), "*.realm");

                foreach (string realm in realms)
                {
                    Realm r = new Realm();
                    r = (Realm)ManagedScripting.XmlSerialization.Load(realm, r);

                    foreach (Zone z in r.Zones)
                    {
                        if (z.Name == System.IO.Path.GetFileNameWithoutExtension(zone))
                        {
                            available = false;
                            break;
                        }
                    }
                    if (!available)
                        break;
                }

                if (!available)
                    break;
                else
                {
                    lstAvailableZones.Items.Add(System.IO.Path.GetFileNameWithoutExtension(zone));
                }
            }

            propertyRealm.SelectedObject = _Realm;

            string[] existingRealms = System.IO.Directory.GetFiles(Engine.GetDataPath(Engine.SaveDataTypes.Realms));
            foreach (string realm in existingRealms)
                lstRealms.Items.Add(System.IO.Path.GetFileNameWithoutExtension(realm));
        }

        private void btnNewRealm_Click(object sender, EventArgs e)
        {
            _Zone = new Zone();
            _Realm = new Realm();

            propertyRealm.SelectedObject = _Realm;
            lstZonesInRealm.Items.Clear();
        }

        private void btnSaveRealm_Click(object sender, EventArgs e)
        {
            string path = Engine.GetDataPath(Engine.SaveDataTypes.Realms);
            string filename = System.IO.Path.Combine(path, _Realm.Name + ".realm");
            MUDEngine.FileSystem.FileSystem.Save(filename, _Realm);

            if (!lstRealms.Items.Contains(_Realm.Name))
            lstRealms.Items.Add(_Realm.Name);
        }

        private void lstRealms_SelectedIndexChanged(object sender, EventArgs e)
        {
            string path = Engine.GetDataPath(Engine.SaveDataTypes.Realms);
            string filename = System.IO.Path.Combine(path, lstRealms.SelectedItem.ToString() + ".realm");
            _Realm = (Realm)ManagedScripting.XmlSerialization.Load(filename, _Realm);

            propertyRealm.SelectedObject = _Realm;
        }
    }
}
