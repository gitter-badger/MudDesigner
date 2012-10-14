using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Networking;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Scripting;

namespace MudDesigner.Editor
{
    public partial class Editor : Form
    {
        public Editor()
        {
            InitializeComponent();
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            //Compile the game scripts
            CompileEngine.AddAssemblyReference("MudDesigner.Engine.dll");
            CompileEngine.Compile(MudDesigner.Engine.Properties.Engine.Default.ScriptsPath);

            //Add the engine assembly to the Script Factory
            ScriptFactory.AddAssembly(Assembly.GetExecutingAssembly());
            //Add the compiled scripts assembly to the Script Factory
            ScriptFactory.AddAssembly(CompileEngine.CompiledAssembly);
            
            //Load the Engine assembly
            Assembly assem = Assembly.LoadFile(Path.Combine(Environment.CurrentDirectory, "MudDesigner.Engine.dll"));
            ScriptFactory.AddAssembly(assem);
            assem = Assembly.LoadFile(Path.Combine(Environment.CurrentDirectory, "MudDesigner.Scripts.dll"));
            ScriptFactory.AddAssembly(assem);

            //Add any additional assemblies that might have been compiled elsewhere (downloadable assemblies)
            if (MudDesigner.Engine.Properties.Engine.Default.ScriptLibrary.Count != 0)
            {
                foreach (string assembly in MudDesigner.Engine.Properties.Engine.Default.ScriptLibrary)
                {
                    //Make sure the assembly actually exists first.
                    if (File.Exists(assembly))
                        ScriptFactory.AddAssembly(assembly);
                }
            }

            Type[] gameObjects = ScriptFactory.FindInheritedTypes("MudDesigner.Engine.Core.BaseGameObject");

            if (gameObjects.Length > 0)
            {
                foreach (Type t in gameObjects)
                {
                    if (t.IsAbstract || t.IsEnum || t.IsInterface || t.IsValueType)
                        continue;
                    objectBrowser.Items.Add(t.Name);
                }
            }
        }

        private void objectBrowser_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
            editorContainer.Panel1Collapsed = true;

        }
    }
}
