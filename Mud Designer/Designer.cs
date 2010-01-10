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
            containerMain.Panel1.Controls.Clear();
            containerMain.Panel1.Controls.Add(new UIWidgets.RealmExplorer().InstallControl(projectPath));
        }

        private void btnSaveObject_Click(object sender, EventArgs e)
        {
            //Get the object Type
            Type t = propertyObject.SelectedObject.GetType();
            //We can use to get a copy of the currently selected object
            //if it is a BaseObject (Aquire it's BaseObject.Filename)
            var obj = new BaseObject();

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
                obj = (Currency)propertyObject.SelectedObject;
                objectPath = Path.Combine(projectPath, "Currencies");
                ValidatePath(objectPath);
                filename = Path.Combine(objectPath, obj.Filename);
                FileManager.Save(filename, obj);
            }
            else if (t == typeof(Realm))
            {
                obj = (Realm)propertyObject.SelectedObject;
                objectPath = Path.Combine(projectPath, "Realms");
                objectPath = Path.Combine(objectPath, obj.Name);
                filename = Path.Combine(objectPath, obj.Filename);
                objectPath = Path.Combine(objectPath, "Zones");
                ValidatePath(objectPath);
                FileManager.Save(filename, obj);
            }
            else if (t == typeof(Zone))
            {
                Zone zone = (Zone)propertyObject.SelectedObject;
                if (string.IsNullOrEmpty(zone.Realm))
                {
                    objectPath = Path.Combine(projectPath, "Zones");
                    objectPath = Path.Combine(objectPath, zone.Name);
                }
                else
                {
                    objectPath = Path.Combine(projectPath, "Realms");
                    objectPath = Path.Combine(objectPath, zone.Realm);
                    objectPath = Path.Combine(objectPath, "Zones");
                    objectPath = Path.Combine(objectPath, zone.Name);
                    filename = Path.Combine(objectPath, zone.Filename);
                }
                ValidatePath(objectPath);
                filename = Path.Combine(objectPath, zone.Filename);
                FileManager.Save(filename, zone);
            }

            RefreshProjectExplorer();
        }

        private void LoadObject(TreeNode selectedNode)
        {
            string projectPath = Path.Combine(Application.StartupPath, "Project");
            string objectFilename = "";

            if (selectedNode.Text == "Project")
            {
                MessageBox.Show("You cannot edit the Project node in the Project Explorer.");
                return;
            }

            //for root objects
            if (selectedNode.Parent.Text == "Project")
            {
                    if (selectedNode.Text == "Game.xml")
                    {
                        objectFilename = selectedNode.FullPath;
                        ProjectInformation project = new ProjectInformation();
                        project = (ProjectInformation)FileManager.Load(objectFilename, project);
                        lblObjectProperties.Text = "Project Properties (" + project.GameTitle + ")";
                        propertyObject.SelectedObject = project;
                    }
            }
            else if (selectedNode.Parent.Text == "Currencies")
            {
                objectFilename = selectedNode.FullPath;
                Currency currency = new Currency();
                currency = (Currency)FileManager.Load(objectFilename, currency);
                lblObjectProperties.Text = "Currency Properties (" + currency.Name + ")";
                propertyObject.SelectedObject = currency;
            }
            else if (selectedNode.Parent.Parent.Text == "Realms")
            {
                objectFilename = selectedNode.FullPath;
                    //incase a directory was selected instead of a file to be edited.
                if (Path.GetExtension(objectFilename) == "")
                    return;

                Realm realm = new Realm();
                realm= (Realm)FileManager.Load(objectFilename, realm);
                lblObjectProperties.Text = "Realm Properties (" + realm.Name + ")";
                propertyObject.SelectedObject = realm;
            }
            else if (selectedNode.Parent.Parent.Text == "Zones")
            {
                //Zone selected already contained within a Realm
                objectFilename = selectedNode.FullPath;

                //incase a directory was selected instead of a file to be edited.
                if (Path.GetExtension(objectFilename) == "")
                    return;
                Zone zone = new Zone(); 
                zone= (Zone)FileManager.Load(objectFilename, zone);
                lblObjectProperties.Text = "Zone Properties (" + zone.Name + ")";
                propertyObject.SelectedObject = zone;
            }
        }

        private void ValidatePath(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string projectPath = Path.Combine(Application.StartupPath, "Project");

            RefreshProjectExplorer();
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
                t.ForeColor = System.Drawing.Color.Blue;
                node.Nodes.Add(t);
            }

            treeExplorer.SelectedNode = node;
        }

        private void btnRefreshObjects_Click(object sender, EventArgs e)
        {
            treeExplorer.Nodes.Clear();
            TreeNode node = new TreeNode("Project");
            treeExplorer.Nodes.Add(node);
            string projectPath = Path.Combine(Application.StartupPath, "Project");
            PopulateTree(projectPath, node);
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

        private void currencyEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            propertyObject.SelectedObject = new Currency();
        }

        private void mnuZoneBuilder_Click(object sender, EventArgs e)
        {
            propertyObject.SelectedObject = new Zone();
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
                return;

            TreeNode node = FindNode(txtSearch.Text, treeExplorer.Nodes[0]);
            if (node == null)
                MessageBox.Show("No results found!", "Mud Designer");
            else
            {
                //TODO select the node
            }
        }

        private TreeNode FindNode(string nodeText, TreeNode startNode)
        {
            foreach (TreeNode node in startNode.Nodes)
            {
                if (node.Text == nodeText)
                {
                    return node;
                }
                if (node.Nodes.Count != 0)
                {
                    TreeNode n = FindNode(nodeText, node);
                    if (n == null) continue;
                    else return n;
                }
            }

            return null;
        }

        private void deleteObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeExplorer.SelectedNode.Text == "Project")
            {
                MessageBox.Show("You cannot delete the root item 'Project'", "Mud Designer");
                return;
            }
            DialogResult result;
            bool IsZone = false;
            //Check if we are deleting a realm or zone, if so inform the user that
            //all zones/rooms within the object will be deleted as well.
            if (treeExplorer.SelectedNode.FullPath.Contains("Realms") || treeExplorer.SelectedNode.FullPath.Contains("Zones"))
            {
                result = MessageBox.Show("Are you sure you want to delete" 
                    + treeExplorer.SelectedNode.Text + "?\nAll Rooms or Zones within this item will be deleted!", "Mud Designer", MessageBoxButtons.YesNo);
                if (treeExplorer.SelectedNode.FullPath.Contains("Zones"))
                    IsZone = true;
            }
            else
                result = MessageBox.Show("Are you sure you want to delete"
                    + treeExplorer.SelectedNode.Text + "?", "Mud Designer", MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
                return;

            if (IsZone)
            {
                Zone z = new Zone();
                string filename = Path.Combine(Application.StartupPath, treeExplorer.SelectedNode.FullPath);
                if (Path.GetExtension(treeExplorer.SelectedNode.FullPath) == "")
                {
                    string[] zone = Directory.GetFiles(filename, "*.zone");
                    if (zone.Length != 0)
                    {
                        filename = zone[0];
                    }
                    else
                    {
                        Directory.Delete(treeExplorer.SelectedNode.FullPath, true);
                        return;
                    }
                }
                    z = (Zone)FileManager.Load(filename, z);
                    if (z.Realm != "")
                    {
                        string projectPath = Path.Combine(Application.StartupPath, "Project");
                        string[] files = Directory.GetFiles(Path.Combine(projectPath, "Realms"), "*.realm", SearchOption.AllDirectories);

                        foreach (string file in files)
                        {
                            Realm r = new Realm();
                            r = (Realm)FileManager.Load(file, r);
                            if (r.Name == z.Realm)
                            {
                                r.Zones.Remove(z.Filename);
                                FileManager.Save(file, r);
                                break;
                            }
                        }
                    }
                }

            //Its a directory to delete if we have no extension assigned to it
            if (Path.GetExtension(treeExplorer.SelectedNode.FullPath) == "")
            {
                Directory.Delete(Path.Combine(Application.StartupPath, treeExplorer.SelectedNode.FullPath), true);
            }
            else
            {
                string filename = Path.GetFileName(treeExplorer.SelectedNode.FullPath);
                string fullPath = treeExplorer.SelectedNode.FullPath;
                string deletePath = fullPath.Substring(0, fullPath.Length - filename.Length);
                File.Delete(Path.Combine(Application.StartupPath, treeExplorer.SelectedNode.FullPath));
                Directory.Delete(deletePath, true);
            }
            //Just incase we have the zone or the realm selected that the zone belonged too.
            //users can re-save the current realm and if it contained the zone we just deleted
            //the zone will be still be saved as part of the realm.
            propertyObject.SelectedObject = null;
            RefreshProjectExplorer();
        }

        public void RefreshProjectExplorer()
        {
            btnRefreshObjects_Click(null, null);
        }

        private void propertyObject_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            SaveSelected();
            RefreshProjectExplorer();
        }

        public void SaveSelected()
        {
            btnSaveObject_Click(null, null);
        }
    }
}
