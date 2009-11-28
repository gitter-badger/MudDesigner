using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RoomDesigner
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] arguments)
        {
            MUDEngine.Engine.ValidateDataPaths();
            MUDEngine.FileSystem.FileSystem.FileType = MUDEngine.FileSystem.FileSystem.OutputFormats.XML;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            //Check if we have a valid room supplied in the arguments.
            //The Zone editor can launch the Room Designer, if the user
            //chooses to edit a room from within the Zone Editor. Currently
            //Zone editor is the only planned editor that can allow users to
            //edit existing rooms. Simplifies it, as I don't need to design
            //a Realm/Zone/Room explorer in the Room or Realm editors.
            if (arguments.Length == 0)
            {
                Application.Run(new frmMain());
            }
            else
            {
                foreach (string argument in arguments)
                {
                    //check if it's a room specified.
                    if (argument.ToLower().StartsWith("room="))
                    {
                        int startIndex = "room=".Length;
                        string room = argument.Substring(startIndex);
                        string file = Path.Combine(Application.StartupPath, "Data\\Rooms\\") + room;

                        if (File.Exists(file))
                        {
                            Application.Run(new frmMain(argument));
                        }
                        else
                        {
                            MessageBox.Show("Unable to locate the specified file."
                                + "Please ensure that it exists or the correct argument format was used.\n"
                                + "Room: " + file,
                                "Room Designer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Application.Run(new frmMain());
                        }
                    }
                }
            }
        }
    }
}
