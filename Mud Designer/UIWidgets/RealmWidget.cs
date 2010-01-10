using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using MudDesigner.MudEngine.FileSystem;
using MudDesigner.MudEngine.GameObjects;
using MudDesigner.MudEngine.GameObjects.Environment;
namespace MudDesigner.UIWidgets
{
    public partial class RealmExplorer : UserControl
    {
        public RealmExplorer()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public Control InstallControl(string projectPath)
        {
            string[] files = Directory.GetFiles(Path.Combine(projectPath, "Realms"), "*.realm", SearchOption.AllDirectories);

            //TODO: Add if (files.length==0) statement and set a 'No Realms' label in container

            foreach (string realmFile in files)
            {
                Realm realm = new Realm();
                realm = (Realm)FileManager.Load(realmFile, realm);

                Button button = new Button();
                button.FlatStyle = FlatStyle.Flat;
                button.Font = new Font(new FontFamily(System.Drawing.Text.GenericFontFamilies.Serif), 12f, FontStyle.Bold);
                button.BackColor = System.Drawing.Color.FromArgb(48, 48, 48);
                button.ForeColor = System.Drawing.Color.LightGray;
                button.Size = new System.Drawing.Size(160,128);
                button.Name = "btn" + realm.Name;
                button.Text = realm.Name;
                button.Click += new EventHandler(button_Click);
                flowContainer.Controls.Add(button);
            }

            return this;
        }

        void button_Click(object sender, EventArgs e)
        {
            
        }
    }
}
