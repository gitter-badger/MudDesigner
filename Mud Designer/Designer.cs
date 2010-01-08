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

            //Get out saved project file
            string projectPath = Path.Combine(Application.StartupPath, "Project");
            string projectFilename = Path.Combine(projectPath, "Game.xml");

            //Check if the project directory exists
            if (!Directory.Exists(projectPath))
                Directory.CreateDirectory(projectPath);

            //Check if the project file exists. If so load it
            if (File.Exists(projectFilename))
                _Project = (ProjectInformation)FileManager.Load(projectPath, _Project);
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
        }

        private void btnSaveObject_Click(object sender, EventArgs e)
        {
            Type t = propertyObject.SelectedObject.GetType();
            BaseObject obj = new BaseObject();
            string projectPath = Path.Combine(Application.StartupPath, "Project");
            string objectPath = "";
            string filename = "";

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
        }

        private void ValidatePath(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}
