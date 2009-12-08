using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//MudEngine
using MudDesigner.MudEngine;
using MudDesigner.MudEngine.Attributes;
using MudDesigner.MudEngine.FileSystem;
using MudDesigner.MudEngine.GameObjects;
using MudDesigner.MudEngine.GameObjects.Environment;


namespace MudDesigner.Editors
{
    public partial class ProjectSettings : Form
    {
        public ProjectSettings()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //Aquire the Project settings and show them.
            propertySettings.SelectedObject = Program.Project;
            txtStory.Text = Program.Project.Story;
        }

        private void txtStory_TextChanged(object sender, EventArgs e)
        {
            Program.Project.Story = txtStory.Text;
        }

        private void ProjectSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            string filename = System.IO.Path.Combine(FileManager.GetDataPath(SaveDataTypes.Root), "Project.xml");
            FileManager.Save(filename, Program.Project);
        }
    }
}
