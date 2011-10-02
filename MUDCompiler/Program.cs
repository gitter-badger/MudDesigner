using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace MUDCompiler
{
    class Program
    {
        [STAThread()]
        static void Main(String[] args)
        {
            Application.Run(new frmCompiler());
        }
    }
}
