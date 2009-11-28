using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MUDEngine.Objects.Environment;
using MUDEngine;

namespace RealmExplorer
{
    public partial class frmMain : Form
    {
        Zone _Zone;
        Realm _Realm;

        public frmMain()
        {
            InitializeComponent();
            _Zone = new Zone();
            _Realm = new Realm();
        }

        private void btnNewRealm_Click(object sender, EventArgs e)
        {
            _Zone = new Zone();
            _Realm = new Realm();
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }
    }
}
