using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MUDCompiler
{
    public partial class frmCompiler : Form
    {
        public frmCompiler()
        {
            InitializeComponent();
        }

        private void openDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rScripting.CompileEngine engine = new rScripting.CompileEngine();

            FolderBrowserDialog browse = new FolderBrowserDialog();
            browse.ShowDialog();

            if (String.IsNullOrEmpty(browse.SelectedPath))
            {
                txtConsole.Text = "Compilation canceled.";
                return;
            }

            bool isOK = engine.Compile(browse.SelectedPath);

            if (isOK)
                txtConsole.Text = "Compiled without error.";
            else
                txtConsole.Text = engine.Errors;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rScripting.CompileEngine engine = new rScripting.CompileEngine();

            OpenFileDialog browse = new OpenFileDialog();
            browse.ShowDialog();

            System.IO.FileInfo file = new System.IO.FileInfo(browse.FileName);

            if (String.IsNullOrEmpty(browse.FileName))
            {
                txtConsole.Text = "Compilation canceled.";
                return;
            }

            bool isOK = engine.Compile(file);
            if (isOK)
                txtConsole.Text = "Compiled without error.";
            else
                txtConsole.Text = engine.Errors;
        }
    }
}
