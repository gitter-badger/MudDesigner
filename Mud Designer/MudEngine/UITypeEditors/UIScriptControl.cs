using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MudDesigner.MudEngine.GameObjects;

namespace MudDesigner.MudEngine.UITypeEditors
{
    public partial class UIScriptControl : UserControl
    {
        internal string Script
        {
            get
            {
                return richTextBox1.Text;
            }
        }

        public UIScriptControl(BaseObject obj)
        {
            InitializeComponent();
            richTextBox1.Text = obj.Script;
        }
    }
}
