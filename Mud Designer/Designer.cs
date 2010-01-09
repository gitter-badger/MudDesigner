//.NET
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

//Mud Designer
using MudDesigner.MudEngine.FileSystem;
using MudDesigner.MudEngine.GameManagement;
using MudDesigner.MudEngine.GameObjects;
using MudDesigner.MudEngine.GameObjects.Environment;
using MudDesigner.MudEngine.GameObjects.Items;

namespace MudDesigner
{
    public partial class Designer : Form
    {
        ProjectInformation _Project;
        BaseObject _GameObject;

        public Designer()
        {
            InitializeComponent();

            //instance a baseObject so that we can store inherited classes
            //for use during our runtime
            _GameObject = new BaseObject();
            _Project = new ProjectInformation();

            //Get out saved project file
            string projectPath = Path.Combine(Application.StartupPath, "Project");
            string projectFilename = Path.Combine(projectPath, "Game.xml");

            //Check if the project directory exists
            ValidatePath(projectPath);

            //Check if the project file exists. If so load it
            if (File.Exists(projectFilename))
                _Project = (ProjectInformation)FileManager.Load(projectFilename, _Project);
            else
                _Project = new ProjectInformation();

            //Setup our Designer Titlebar text
            StringBuilder title = new StringBuilder();
            title.Append("Mud Designer: ");
            title.Append(_Project.GameTitle);
            title.Append(" ");
            title.Append(_Project.Version);
            this.Text = title.ToString();

            //Assign our Project Information to the propertygrid
            propertyObject.SelectedObject = _Project;

            //build a collection of Realms for viewing
            InstallWidgetRealms(projectPath);
        }

        private void InstallWidgetRealms(string projectPath)
        {
            ValidatePath(Path.Combine(projectPath, "Realms"));
            string[] files = Directory.GetFiles(Path.Combine(projectPath, "Realms"), "*.xml");

            if (files.Length == 0)
                return;

            UninstallWidget();
            foreach (string realmFile in files)
            {
                Realm realm = new Realm();
                realm = (Realm)FileManager.Load(realmFile, realm);

                Button button = new Button();
                button.FlatStyle = FlatStyle.Flat;
                button.BackColor = System.Drawing.Color.FromArgb(48,48,48   );
                button.Size = new System.Drawing.Size(130,100);
                button.Name = "btn" + realm.Name;
                button.Text = realm.Name;
                flowContainer.Controls.Add(button);
            }
        }

        /// <summary>
        /// Uninstalls the currently installed widget
        /// </summary>
        public void UninstallWidget()
        {
            flowContainer.Controls.Clear();
        }

        private void btnSaveObject_Click(object sender, EventArgs e)
        {
            //Get the object Type
            Type t = propertyObject.SelectedObject.GetType();
            //We can use to get a copy of the currently selected object
            //if it is a BaseObject (Aquire it's BaseObject.Filename)
            BaseObject obj = new BaseObject();

            //Filepaths
            string projectPath = Path.Combine(Application.StartupPath, "Project");
            string objectPath = "";
            string filename = "";

            //Start checking to see what object we are saving
            if (t == typeof(ProjectInformation))
            {
                filename = Path.Combine(projectPath, "Game.xml");
                FileManager.Save(filename, _Project);
            }
            else if (t == typeof(Currency))
            {
                obj = (BaseObject)propertyObject.SelectedObject;
                objectPath = Path.Combine(projectPath, "Currencies");
                ValidatePath(objectPath);
                filename = Path.Combine(objectPath, obj.Filename);
                FileManager.Save(filename, obj);
            }
            else if (t == typeof(Realm))
            {
                obj = (BaseObject)propertyObject.SelectedObject;
                objectPath = Path.Combine(projectPath, "Realms");
                ValidatePath(objectPath);
                filename = Path.Combine(objectPath, obj.Filename);
                FileManager.Save(filename, obj);
            }

            btnRefreshObjects_Click(null, null);
        }

        private void LoadObject(TreeNode selectedNode)
        {
            string projectPath = Path.Combine(Application.StartupPath, "Project");
            string objectPath = "";
            string objectFilename = "";

            if (selectedNode.Text == "Game Objects")
            {
                MessageBox.Show("You cannot edit the Game Object node in the Project Explorer.");
                return;
            }

            switch (selectedNode.Parent.Text)
            {
                case "Game Objects":
                    if (selectedNode.Text == "Game.xml")
                    {
                        objectFilename = Path.Combine(projectPath, selectedNode.Text);
                        propertyObject.SelectedObject = (ProjectInformation)FileManager.Load(objectFilename, new ProjectInformation());
                    }
                    break;

                case "Currencies":
                    objectPath = Path.Combine(projectPath, selectedNode.Parent.Text);
                    objectFilename = Path.Combine(objectPath, selectedNode.Text);
                    propertyObject.SelectedObject = (Currency)FileManager.Load(objectFilename, new Currency());
                    break;

                case "Realms":
                    objectPath = Path.Combine(projectPath, selectedNode.Parent.Text);
                    objectFilename = Path.Combine(objectPath, selectedNode.Text);
                    propertyObject.SelectedObject = (Realm)FileManager.Load(objectFilename, new Realm());
                    break;
            }
        }

        private void ValidatePath(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string projectPath = Path.Combine(Application.StartupPath, "Project");

            btnRefreshObjects_Click(null, null);
        }

        private void PopulateTree(string dir, TreeNode node)
        {
            // get the information of the directory
            DirectoryInfo directory = new DirectoryInfo(dir);

            // loop through each subdirectory
            foreach (DirectoryInfo d in directory.GetDirectories())
            {
                // create a new node
                TreeNode t = new TreeNode(d.Name);
                // populate the new node recursively
                PopulateTree(d.FullName, t);
                node.Nodes.Add(t); // add the node to the "master" node
            }
            // lastly, loop through each file in the directory, and add these as nodes
            foreach (FileInfo f in directory.GetFiles())
            {
                // create a new node
                TreeNode t = new TreeNode(f.Name);
                // add it to the "master"
                node.Nodes.Add(t);
            }

            treeExplorer.SelectedNode = node;
        }

        private void btnRefreshObjects_Click(object sender, EventArgs e)
        {
            treeExplorer.Nodes.Clear();
            TreeNode node = new TreeNode("Game Objects");
            treeExplorer.Nodes.Add(node);
            string projectPath = Path.Combine(Application.StartupPath, "Project");
            PopulateTree(projectPath, node);
        }

        private void currencyEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Currency obj = new Currency();
            propertyObject.SelectedObject = obj;
        }

        private void mnuEditObject_Click(object sender, EventArgs e)
        {
            LoadObject(treeExplorer.SelectedNode);
        }

        private void mnuProjectInformation_Click(object sender, EventArgs e)
        {
            propertyObject.SelectedObject = _Project;
        }

        private void mnuRealmEditor_Click(object sender, EventArgs e)
        {
            propertyObject.SelectedObject = new Realm();
        }
    }
}
