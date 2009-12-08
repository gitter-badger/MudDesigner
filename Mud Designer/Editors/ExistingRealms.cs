using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MudDesigner.MudEngine.FileSystem;
using MudDesigner.MudEngine.GameObjects;
using MudDesigner.MudEngine.GameObjects.Environment;

namespace MudDesigner.Editors
{
    public partial class ExistingRealms : Form
    {
        Zone _Zone;

        public ExistingRealms(string Zone)
        {
            InitializeComponent();
            _Zone = new Zone();
            string filename = System.IO.Path.Combine(FileManager.GetDataPath(Program.Realm.Name, Zone), Zone + ".zone");
            _Zone = (Zone)FileManager.Load(filename, _Zone);

            string[] realms = System.IO.Directory.GetFiles(FileManager.GetDataPath(SaveDataTypes.Realms), "*.realm");
            foreach (string file in realms)
            {
                Realm realm = new Realm();
                realm = (Realm)FileManager.Load(file, realm);
                lstRealms.Items.Add(realm.Name);
            }
        }
    }
}
