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
        ScriptingEngine engine = new ScriptingEngine();
        ScriptObject currentScript;
        Type[] types;
        object movingObject;

        public frmMain()
        {
            InitializeComponent();

            engine.LoadAssembly(Application.StartupPath + "/MUDEngine.dll");
            types = engine.GetAssembly.GetTypes();
            currentScript = new ScriptObject();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //Load the engine
            engine.LoadAssembly(Application.StartupPath + "/MUDEngine.dll");
            Type[] foundTypes = engine.GetAssembly.GetTypes();
            Button button = new Button();

            List<Type> tempTypes = new List<Type>();
            ListBox lst = new ListBox();
            lst.Sorted = true;

            foreach (Type t in foundTypes)
            {
                if (t.BaseType.Name == "BaseObject")
                {
                    tempTypes.Add(t);
                    lst.Items.Add(t.Name);
                }
            }

            foreach (string t in lst.Items)
            {
                button = new Button();
                button.Width = flowLayoutPanel1.Width - 10;
                button.Text = t;
                button.AllowDrop = true;
                button.MouseDown += new MouseEventHandler(newObject_MouseDown);
                flowLayoutPanel1.Controls.Add(button);
            }

            propertyGrid1.ViewForeColor = Color.Blue;
            propertyGrid1.SelectedObject = engine;
        }

        void newObject_MouseDown(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            movingObject = button;
            DoDragDrop(button, DragDropEffects.Copy);
        }

        private void page1_DragDrop(object sender, DragEventArgs e)
        {

            if (movingObject is Button)
            {
                Button button = (Button)movingObject;

                foreach (Type t in types)
                {
                    if (t.Name == button.Text)
                    {
                        engine.InstanceObject(t, null);
                        currentScript = engine.GetObject(t.Name);
                        propertyGrid1.SelectedObject = currentScript.Instance;
                        if (page1.Text == "Empty")
                        {
                            page1.Text = currentScript.Name;
                        }
                        else
                        {
                            TabPage tab = new TabPage(currentScript.Name);
                            tabControl1.TabPages.Add(tab);
                        }
                    }
                }
            }
        }

        private void page1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
    }
}
