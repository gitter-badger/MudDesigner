using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisualComponents
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class VisualContainer : UserControl
    {
        [Browsable(true)]
        public  string Title
        {
            get { return btnTitle.Text; }
            set { btnTitle.Text = value; }
        }

        public VisualContainer()
        {
            InitializeComponent();
        }
    }
}
