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
        public bool IsClosable
        {
            get { return btnClose.Visible; }
            set { btnClose.Visible = value; }
        }

        [Browsable(true)]
        public  string Title
        {
            get { return lblTitle.Text; }
            set { lblTitle.Text = value; }
        }

        public void AddControl(Control control)
        {
            this.flowLayoutPanel1.Controls.Add(control);
        }

        public VisualContainer()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Container.Remove(this);
        }

        private void Container_Load(object sender, EventArgs e)
        {
        }
    }
}
