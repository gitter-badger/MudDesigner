using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MudDesigner
{
    public partial class frmInputBox : Form
    {
        public bool IsCancel { get; set; }

        public String Description { get { return this.lblDescription.Text; } set { this.lblDescription.Text = value; } }

        public String Input { get { return this.txtInput.Text; } private set { this.txtInput.Text = value; } }

        public frmInputBox(String desc)
        {
            InitializeComponent();

            this.Description = desc;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            IsCancel = true;
            Input = String.Empty;
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOK_Click(sender, null);
            else if (e.KeyCode == Keys.Escape)
                btnCancel_Click(sender, null);
        }
    }
}
