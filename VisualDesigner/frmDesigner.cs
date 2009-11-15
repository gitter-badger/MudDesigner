using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ManagedScripting;
using MUDEngine;
using MUDEngine.Objects;
using MUDEngine.Objects.Environment;
using MUDEngine.FileSystem;

namespace VisualDesigner
{
    public partial class frmMain : Form
    {
        //Script engine used to load and instance objects
        ScriptingEngine engine = new ScriptingEngine();
        //The current object being edited
        ScriptObject currentScript;
        //Collection of types the engine is holding from compiled scripts and the engine Objects namespace
        Type[] types;
        //The object being dragged by the mouse onto the visual designer
        object movingObject;

        public frmMain()
        {
            InitializeComponent();

            //Load the engine's assembly
            engine.LoadAssembly(Application.StartupPath + "/MUDEngine.dll");

            //Get all of the Types it contains.
            types = engine.GetAssembly.GetTypes();
            //instance the current script object
            currentScript = new ScriptObject();
        }

        /// <summary>
        /// Closes the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ScriptObject obj in engine.GetObjects())
            {
                XmlSerialization.Save(obj.Name + ".xml", obj);
            }
            Application.Exit();
        }

        /// <summary>
        /// Sets up the initial editor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            //The button that will be placed within the Object Browser for object creation
            Button button = new Button();

            //Collections used to sort out the non-useable objects and sort them
            ListBox lst = new ListBox();
            lst.Sorted = true;

            //Scan the types array and only save the Types inheriting from BaseObject
            foreach (Type t in types)
            {
                if (t.BaseType.Name == "BaseObject")
                {
                    lst.Items.Add(t.Name);
                }
            }

            //Loop through our now sorted Object collection and create new buttons
            //within the Object Browser for each Object, and tie them into the same
            //MouseDown event handler.
            foreach (string t in lst.Items)
            {
                button = new Button();
                button.Width = flowLayoutPanel1.Width - 10;
                button.Text = t;
                button.AllowDrop = true;
                button.Dock = DockStyle.Top;
                button.MouseDown += new MouseEventHandler(newObject_MouseDown);
                button.FlatStyle = FlatStyle.Flat;
                
                flowLayoutPanel1.Controls.Add(button);
            }
        }

        /// <summary>
        /// When the mouse is pressed down, begin the drag and drop
        /// Store the button within the movingObject so we can access it
        /// due to DoDragDrop not coping the button like it should when I
        /// try to access it via the DragDrop method of the tabpage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void newObject_MouseDown(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            movingObject = button;
            DoDragDrop(button, DragDropEffects.Copy);
        }

        /// <summary>
        /// Create a new instance of the object dropped onto this tab and
        /// create a new page once the object is created.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page1_DragDrop(object sender, DragEventArgs e)
        {
            //Check if the object is a button being dropped on here
            if (movingObject is Button)
            {
                //create a new button based off the one being dropped on here
                Button button = (Button)movingObject;

                //loop through all the types we have at the moment
                //and find the one that matches the one the button represents
                foreach (Type t in types)
                {
                    if (t.Name == button.Text)
                    {
                        //Create an instance of the object within the engine
                        engine.InstanceObject(t, null);
                        //Get a copy of it
                        currentScript = engine.GetObject(t.Name);
                        //Place it in the propertygrid so we can edit it
                        propertyGrid1.SelectedObject = currentScript.Instance;

                        //If the first page is still empty, use it
                        if (page1.Text == "Empty")
                        {
                            page1.Text = currentScript.Name;
                        }
                            //Otherwise create a new page
                        else
                        {
                            TabPage tab = new TabPage(currentScript.Name);
                            //All new tabs will use the same event handler method
                            tab.DragDrop +=new DragEventHandler(page1_DragDrop);
                            tab.DragEnter += new DragEventHandler(page1_DragEnter);
                            tab.AllowDrop = true;
                            tab.BackColor = Color.FromArgb(64,64,64);
                            tabControl1.TabPages.Add(tab);
                            //select the tab.
                            tabControl1.SelectedTab = tab;
                        }
                    }
                }
            }
        }

        private void page1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentScript = engine.GetObject(tabControl1.SelectedTab.Text);
            propertyGrid1.SelectedObject = currentScript.Instance;
        }
    }
}
