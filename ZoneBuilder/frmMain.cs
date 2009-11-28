using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MUDEngine;
using MUDEngine.Objects.Environment;

namespace ZoneBuilder
{
    public partial class frmMain : Form
    {
        Zone _CurrentZone;
        Room _CurrentRoom;

        public frmMain()
        {
            InitializeComponent();
            _CurrentRoom = new Room();
            _CurrentZone = new Zone();
        }
    }
}
