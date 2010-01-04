using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MudDesigner.MudEngine.GameObjects;

namespace MudDesigner.Editors
{
    public partial class ScriptEditor : Form
    {
        public string Script
        {
            get
            {
                return richTextBox1.Text;
            }
        }
        public ScriptEditor(BaseObject baseObject)
        {
            InitializeComponent();

            richTextBox1.Text = baseObject.Script;
        }
    }
}
