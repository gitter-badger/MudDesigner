using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using MudDesigner.MudEngine.GameObjects;
using MudDesigner.MudEngine.GameObjects.Environment;

namespace MudDesigner.MudEngine.UITypeEditors
{
    public partial class UIDoorwayControl : Form
    {
        Room _Room;
        Zone _Zone;
        public Door Door { get; set; }

        public UIDoorwayControl(Door door, Zone zone)
        {
            InitializeComponent();
            _Room = new Room();
            _Zone = new Zone();
            Door = new Door();

            _Zone = zone;
        }
    }
}
