using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MudDesigner.MudEngine.GameObjects.Environment;
using MudDesigner.MudEngine.GameObjects;
namespace MudDesigner
{
    public partial class Designer : Form
    {
        Room r = new Room();

        public Designer()
        {
            InitializeComponent();
            propertyGrid1.SelectedObject = r;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ControlContainer.Panel1.Controls.Clear();
        }
    }
}
