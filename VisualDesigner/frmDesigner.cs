using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisualDesigner
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //Load the engine
            System.Reflection.Assembly assem = System.Reflection.Assembly.LoadFile(Application.StartupPath + "/MUDEngine.dll");
            Type[] types = assem.GetTypes();
            Button button = new Button();
            this.flowLayoutPanel1.Controls.Add(button);

            foreach (Type t in types)
            {
                if (t.BaseType.Name == "BaseObject")
                {
                    button = new Button();
                    button.Width = flowLayoutPanel1.Width - 10;
                    button.Text = t.Name;
                    button.Click += new EventHandler(newObject_Click);
                    this.flowLayoutPanel1.Controls.Add(button);
                }
            }
            
        }

        void newObject_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            System.Reflection.Assembly assem = System.Reflection.Assembly.LoadFile(Application.StartupPath + "/MUDEngine.dll");
            Type[] types = assem.GetTypes();
            bool found = false;

            foreach (Type t in types)
            {
                if (t.Name == button.Text)
                {
                    //Found it
                    found = true;

                    ManagedScripting.ScriptingEngine engine = new ManagedScripting.ScriptingEngine();
                    engine.LoadAssembly(Application.StartupPath + "/MUDEngine.dll");
                    engine.InstanceObject(t, null);
                    ManagedScripting.ScriptObject obj = engine.GetObject(t.Name);
                    propertyGrid1.SelectedObject = obj.Instance;
                }
            }
        }
    }
}
