using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MUDEngine.Objects;

namespace MUDEngine.Controls
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

        [Browsable(false)]
        internal BaseObject GameObject
        {
            get;
            set;
        }

        public VisualContainer(BaseObject EngineObject) : this()
        {
            //InitializeComponent();
            this.Dock = DockStyle.Fill;
            GameObject = EngineObject;
        }

        public VisualContainer() : base()
        {
            InitializeComponent();
        }

        protected MUDEngine.Objects.BaseObject _Object;
    }
}
