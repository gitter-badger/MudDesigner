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
using MudDesigner.UIWidgets;

namespace MudDesigner
{
    public partial class Designer : Form
    {
        //The available object types that we can scan through
        //while loading objects
        enum ObjectType
        {
            Nothing,
            Project,
            Currency,
            Realm,
            ZoneRoot,
            ZoneWithinRealm,
            RoomRoot,
            RoomWithinZone,
            Zone,
            Room,
        }
        
        //Currently loaded project
        ProjectInformation _Project;
        //Used for  temporarily holding an object during load.
        BaseObject _GameObject;
        //Check for if the loaded object is saved yet or not.
        bool IsSaved;

        /// <summary>
        /// Initializes the Designers Temporary objects, Project Information and varifies project paths.
        /// </summary>
        public Designer()
        {
            InitializeComponent();

            //instance a baseObject so that we can store inherited classes
            //for use during our runtime
            _GameObject = new BaseObject();
            _Project = new ProjectInformation();
            IsSaved = true;

            //Get out saved project file
            string projectPath = FileManager.GetDataPath(SaveDataTypes.Root);

            if (File.Exists(Path.Combine(projectPath, _Project.Filename)))
                _Project = (ProjectInformation)_Project.Load(projectPath);

            //ensure the path exists
            ValidatePath(projectPath);

            //Display the Project directory structure in the Project Explorer
            RefreshProjectExplorer();
        }

        /// <summary>
        /// Checks the provided treenode to see what Type of object it is.
        /// The result is returned for use. This method is only useful for
        /// Files that have been selected in the designer.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private ObjectType GetNodeType(TreeNode node)
        {
            try
            {
                //The provided nodes parent node is aquired so we can
                //find if we can get it's current type.
                TreeNode parentNode = node.Parent;

                //switch is only for ProjectInfo, Currency, un-owned Zones & Rooms
                switch (parentNode.Text)
                {
                    case "Project":
                        if (node.Text == _Project.Filename)
                            return ObjectType.Project;
                        break;
                    case "Currencies":
                        return ObjectType.Currency;
                }

                //Basic Root items where not found, do a deeper search now.
                //Find Realms, owned Zones & owned Rooms
                TreeNode grandparentNode = new TreeNode();
                if (parentNode.Parent != null)
                    grandparentNode = parentNode.Parent;

                switch (grandparentNode.Text)
                {
                    case "Realms":
                        return ObjectType.Realm;
                        //Root Zones
                    case "Zones":
                        if (grandparentNode.Parent.Text == "Project")
                            return ObjectType.ZoneRoot;
                        else
                            return ObjectType.ZoneWithinRealm;
                }
            }
                //Something happened, handle it with a message box.
            catch (Exception ex)
            {
                MessageBox.Show("Internal Error: " + ex.Message);
            }
            return ObjectType.Nothing;
        }

        /// <summary>
        /// Searchs to find what Type the selected TreeNode is, and loads
        /// that objects into the propertygrid.
        /// </summary>
        /// <param name="selectedNode"></param>
        private void LoadObject(TreeNode selectedNode)
        {
            ///Method Fields we will need to use
            string projectPath = FileManager.GetDataPath(SaveDataTypes.Root);
            string objectFilename = "";
            string objectPath = "";
            string objectName = "";
            string parentName = "";

            ObjectType objType = GetNodeType(selectedNode);

            //for root objects
            switch (objType)
            {
                    //A non-editable object was found
                case ObjectType.Nothing:
                    return;
                    //Project Information file
                case ObjectType.Project:
                    _Project = (ProjectInformation)_Project.Load(FileManager.GetDataPath(SaveDataTypes.Root));
                    propertyObject.SelectedObject = _Project;
                    break;
                    //Currency File
                case ObjectType.Currency:
                    Currency currency = new Currency();
                    objectFilename = Path.Combine(FileManager.GetDataPath(SaveDataTypes.Currencies), selectedNode.Text);
                    propertyObject.SelectedObject = (Currency)currency.Load(objectFilename);
                    break;
                    //Realm File selected
                case ObjectType.Realm:
                    objectName = Path.GetFileNameWithoutExtension(selectedNode.Parent.Text);
                    objectPath = Path.Combine(FileManager.GetDataPath(SaveDataTypes.Realms), objectName);
                    objectFilename = Path.Combine(objectPath, selectedNode.Text);
                    _GameObject = new Realm();
                    propertyObject.SelectedObject = _GameObject.Load(objectFilename);
                    break;
                    //Zone File located under Project/Zones
                case ObjectType.ZoneRoot:
                    objectName = Path.GetFileNameWithoutExtension(selectedNode.Parent.Text);
                    objectPath = Path.Combine(FileManager.GetDataPath(SaveDataTypes.Zones), objectName);
                    objectFilename = Path.Combine(objectPath, selectedNode.Text);
                    _GameObject = new Zone();
                    propertyObject.SelectedObject = _GameObject.Load(objectFilename);
                    break;
                    //Zone File located under Project/Realms/Zones
                case ObjectType.ZoneWithinRealm:
                    TreeNode grandparent = selectedNode.Parent.Parent;
                    objectName = Path.GetFileNameWithoutExtension(selectedNode.Parent.Text);
                    parentName = grandparent.Parent.Text;
                    objectFilename = Path.Combine(FileManager.GetDataPath(parentName, objectName), selectedNode.Text);
                    _GameObject = new Zone();
                    propertyObject.SelectedObject = _GameObject.Load(objectFilename);
                    break;
            }
        }

        /// <summary>
        /// Checks the supplied path and creates it if the path does not exist.
        /// </summary>
        /// <param name="path"></param>
        public static void ValidatePath(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        /// <summary>
        /// Checks to see if the curent object in the Properties Pane has been saved or not.
        /// </summary>
        /// <returns></returns>
        private bool CheckSavedState()
        {
            //No need to continue if the save flag is already set
            if (IsSaved)
                return true;

            //Inform the user
            DialogResult result = MessageBox.Show(lblObjectProperties.Text + " has not been saved! Do you wish to save it?", "Mud Designer", MessageBoxButtons.YesNoCancel);

            //Don't save it. Return true so that it can be overwrote
            if (result == DialogResult.No)
                return true;
                //User hit cancel, it's not saved so return false.
            else if (result == DialogResult.Cancel)
                return false;
                //User hit Yes, so save the object
            else
                SaveSelected();

            return true;
        }

        /// <summary>
        /// Searches the Project Explorer for a TreeNode matching the supplied string
        /// </summary>
        /// <param name="nodeText"></param>
        /// <param name="startNode"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Populates the Project Explorer with a directory hierarchy
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="node"></param>
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

                if (node.Text == "Rooms")
                    t.ForeColor = System.Drawing.Color.Red;
                node.Nodes.Add(t);
            }

            treeExplorer.SelectedNode = node;
        }

        public void RefreshProjectExplorer()
        {
            treeExplorer.Nodes.Clear();
            TreeNode node = new TreeNode("Project");
            treeExplorer.Nodes.Add(node);
            PopulateTree(FileManager.GetDataPath(SaveDataTypes.Root), node);
        }

        public void SaveSelected()
        {
            //We can use to get a copy of the currently selected object
            //if it is a BaseObject (Aquire it's BaseObject.Filename)
            var obj = (BaseObject)propertyObject.SelectedObject;

            //Filepaths
            string objectPath = "";
            string filename = "";

            switch (obj.GetType().Name)
            {
                case "ProjectInformation":
                    filename = Path.Combine(FileManager.GetDataPath(SaveDataTypes.Root), "Game.xml");
                    _Project.Save(filename);
                    break;
                case "Currency":
                    filename = Path.Combine(FileManager.GetDataPath(SaveDataTypes.Currencies), obj.Filename);
                    obj.Save(filename);
                    break;
                case "Realm":
                    filename = Path.Combine(FileManager.GetDataPath(SaveDataTypes.Realms), obj.Filename);
                    obj.Save(filename);
                    break;
                case "Zone":
                    Zone z = new Zone();
                    z = (Zone)obj;
                    if (String.IsNullOrEmpty(z.Realm))
                    {
                        objectPath = Path.Combine(FileManager.GetDataPath(SaveDataTypes.Zones), z.Name);
                        filename = Path.Combine(objectPath, z.Filename);
                    }
                    else
                    {
                        objectPath = FileManager.GetDataPath(z.Realm, z.Name);
                        filename = Path.Combine(objectPath, z.Filename);
                    }
                    obj.Save(filename);
                    break;
            }

            RefreshProjectExplorer();
        }

        /// <summary>
        /// Refreshes the Project Explorer incase the directory structure
        /// has changed, but was not reflected in the Explorer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefreshObjects_Click(object sender, EventArgs e)
        {
            RefreshProjectExplorer();
        }

        /// <summary>
        /// Right-Click context menu for the Project Explorer.
        /// Loads the currently selected object into the Property Pane
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuEditObject_Click(object sender, EventArgs e)
        {
            if (CheckSavedState())
                LoadObject(treeExplorer.SelectedNode);
        }

        /// <summary>
        /// Loads the Project Information for editing into the Properties Pane
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuProjectInformation_Click(object sender, EventArgs e)
        {
            if (CheckSavedState())
                propertyObject.SelectedObject = _Project;
        }

        /// <summary>
        /// Creates a new Realm for editing within the Properties Pane
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuNewRealm_Click(object sender, EventArgs e)
        {
            if (CheckSavedState())
                propertyObject.SelectedObject = new Realm();
        }

        /// <summary>
        /// Creates a new Currency for editing within the Properties Pane
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuNewCurrency_Click(object sender, EventArgs e)
        {
            if (CheckSavedState())
                propertyObject.SelectedObject = new Currency();
        }

        /// <summary>
        /// Creates a new Zone for editing within the Properties Pane
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuNewZone_Click(object sender, EventArgs e)
        {
            if (CheckSavedState())
                propertyObject.SelectedObject = new Zone();
        }

        /// <summary>
        /// Searchs the Project Explorer for a file or directory matching the supplied name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void mnuDeleteSelectedObject_Click(object sender, EventArgs e)
        {
            //Check if we are trying to delete the root node
            if (treeExplorer.SelectedNode.Text == "Project")
            {
                MessageBox.Show("You cannot delete the root item 'Project'", "Mud Designer");
                return;
            }

            DialogResult result;
            ObjectType objectType = new ObjectType();
            
            //Check if we are deleting a realm or zone, if so inform the user that
            //all zones/rooms within the object will be deleted as well.
            string fullPath = treeExplorer.SelectedNode.FullPath;
            TreeNode selectedNode = treeExplorer.SelectedNode;

            if (fullPath.Contains("Realms") || fullPath.Contains("Zones"))
            {
                //ask if we want to delete this
                result = MessageBox.Show("Are you sure you want to delete" 
                    + treeExplorer.SelectedNode.Text + "?\nAll Rooms or Zones within this item will be deleted!", "Mud Designer", MessageBoxButtons.YesNo);
            }
            else
                //ask if we want to delete this
                result = MessageBox.Show("Are you sure you want to delete"
                    + treeExplorer.SelectedNode.Text + "?", "Mud Designer", MessageBoxButtons.YesNo);

            //User hit no, cancel
            if (result == DialogResult.No)
                return;

            //Find out what we are deleting
            if (Path.GetExtension(fullPath) == "")
            {
                if (selectedNode.Text == "Zones")
                    objectType = ObjectType.Zone;
                else if (selectedNode.Text == "Rooms")
                    objectType = ObjectType.Room;
            }
            else if (Path.GetExtension(fullPath) == ".Room")
            {
                objectType = ObjectType.Room;
            }
            else if (Path.GetExtension(fullPath) == "Zone")
            {
                objectType = ObjectType.Zone;
            }

            if (objectType == ObjectType.Zone)
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
                }//end if(object is zone)
            else if (objectType == ObjectType.Room)
            {
                //TODO Delete rooms from owning zone
            }

            //Its a directory to delete if we have no extension assigned to it
            if (Path.GetExtension(treeExplorer.SelectedNode.FullPath) == "")
            {
                Directory.Delete(Path.Combine(Application.StartupPath, treeExplorer.SelectedNode.FullPath), true);
            }
            else
            {
                string filename = Path.GetFileName(treeExplorer.SelectedNode.FullPath);
                fullPath = treeExplorer.SelectedNode.FullPath;
                string deletePath = fullPath.Substring(0, fullPath.Length - filename.Length);
                File.Delete(Path.Combine(Application.StartupPath, treeExplorer.SelectedNode.FullPath));
                Directory.Delete(Path.Combine(Application.StartupPath, deletePath), true);
            }
            //Just incase we have the zone or the realm selected that the zone belonged too.
            //users can re-save the current realm and if it contained the zone we just deleted
            //the zone will be still be saved as part of the realm.
            propertyObject.SelectedObject = null;
            RefreshProjectExplorer();
        }

        private void propertyObject_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            IsSaved = false;
            if (propertyObject.SelectedObject is BaseObject)
            {
                BaseObject obj = (BaseObject)propertyObject.SelectedObject;

                //Don't auto-save if we haven't assigned a valid name
                if (obj.Name == "New " + obj.GetType().Name)
                    return;
            }
            SaveSelected();
            IsSaved = true;
            RefreshProjectExplorer();
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Mud Designer.\n\nDownload at http://MudDesigner.Codeplex.com\n"
                + "Join the community at http://MudDesigner.DailyForum.net", "Mud Designer");
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void treeExplorer_DoubleClick(object sender, EventArgs e)
        {
            if (CheckSavedState())
                LoadObject(treeExplorer.SelectedNode);
        }

        private void mnuNewRoom_Click(object sender, EventArgs e)
        {
            if (propertyObject.SelectedObject is Zone)
            {
                MudDesigner.MudEngine.UITypeEditors.UIRoomControl control = new MudDesigner.MudEngine.UITypeEditors.UIRoomControl((Zone)propertyObject.SelectedObject);

                control.ShowDialog();
            }
            else
            {
                MessageBox.Show("You must have a zone loaded prior to attempting to create a Room!\n\n"
    + "You can use a loaded Zones 'Rooms' property via the Object Properties pane to create and manage Rooms as well.", "Mud Designer");
                return;
            }
        }
    }
}
