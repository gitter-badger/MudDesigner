using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MudDesigner.UIControls
{
    public partial class RealmExplorer : UserControl
    {
        public bool IsSplashVisible
        {
            set
            {
                if (value == true)
                    btnSplash.Visible = true;
                else
                    btnSplash.Visible = false;
            }
            get
            {
                return btnSplash.Visible;
            }
        }

        public RealmExplorer()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }
    }
}
