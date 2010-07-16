using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using MudEngine.FileSystem;
using MudEngine.GameObjects;
using MudEngine.GameObjects.Environment;

namespace MudDesigner.UIWidgets
{
    public partial class RealmWidget : UserControl, IWidget
    {
        public RealmWidget()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public Control Initialize()
        {
            string[] files = new string[]{};

            if (Directory.Exists(FileManager.GetDataPath(SaveDataTypes.Realms)))
                files = Directory.GetFiles(FileManager.GetDataPath(SaveDataTypes.Realms), "*.realm", SearchOption.AllDirectories);

            //TODO: Add if (files.length==0) statement and set a 'No Realms' label in container

            if (files.Length == 0)
            {
                Button button = new Button();
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(48, 48, 48);
                button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(48, 48, 48);
                button.Font = new Font(new FontFamily(System.Drawing.Text.GenericFontFamilies.Serif), 12f, FontStyle.Bold);
                button.BackColor = System.Drawing.Color.FromArgb(48, 48, 48);
                button.ForeColor = System.Drawing.Color.LightGray;
                button.Size = new System.Drawing.Size(160, 128);
                button.Name = "btnNoRealms";
                button.Text = "No Existing Realms.";
                button.Dock = DockStyle.Fill;
                this.Controls.Clear();
                this.Controls.Add(button);
            }

            foreach (string realmFile in files)
            {
                Realm realm = new Realm();
                realm = (Realm)realm.Load(realmFile);

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
            MessageBox.Show("Widgets are still under development.");
        }

        public override void Refresh()
        {
            this.Controls.Clear();

            FlowLayoutPanel flowContainer = new FlowLayoutPanel();
            flowContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            flowContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            flowContainer.Location = new System.Drawing.Point(0, 0);
            flowContainer.Name = "flowContainer";
            flowContainer.Padding = new System.Windows.Forms.Padding(10);
            flowContainer.Size = new System.Drawing.Size(537, 502);
            flowContainer.TabIndex = 0;

            string[] files = new string[] { };

            if (Directory.Exists(FileManager.GetDataPath(SaveDataTypes.Realms)))
                files = Directory.GetFiles(FileManager.GetDataPath(SaveDataTypes.Realms), "*.realm", SearchOption.AllDirectories);

            //TODO: Add if (files.length==0) statement and set a 'No Realms' label in container

            if (files.Length == 0)
            {
                Button button = new Button();
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(48, 48, 48);
                button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(48, 48, 48);
                button.Font = new Font(new FontFamily(System.Drawing.Text.GenericFontFamilies.Serif), 12f, FontStyle.Bold);
                button.BackColor = System.Drawing.Color.FromArgb(48, 48, 48);
                button.ForeColor = System.Drawing.Color.LightGray;
                button.Size = new System.Drawing.Size(160, 128);
                button.Name = "btnNoRealms";
                button.Text = "No Existing Realms.";
                button.Dock = DockStyle.Fill;
                this.Controls.Clear();
                this.Controls.Add(button);
            }

            foreach (string realmFile in files)
            {
                Realm realm = new Realm();
                realm = (Realm)realm.Load(realmFile);

                Button button = new Button();
                button.FlatStyle = FlatStyle.Flat;
                button.Font = new Font(new FontFamily(System.Drawing.Text.GenericFontFamilies.Serif), 12f, FontStyle.Bold);
                button.BackColor = System.Drawing.Color.FromArgb(48, 48, 48);
                button.ForeColor = System.Drawing.Color.LightGray;
                button.Size = new System.Drawing.Size(160, 128);
                button.Name = "btn" + realm.Name;
                button.Text = realm.Name;
                button.Click += new EventHandler(button_Click);
                flowContainer.Controls.Add(button);
            }
            base.Refresh();
        }
    }
}
