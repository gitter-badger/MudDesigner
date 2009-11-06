using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MUDEngine
{
    public class Engine
    {
        /// <summary>
        /// Used to ensure that the paths needed to run the game exists.
        /// </summary>
        /// <param name="validatedPath"></param>
        public static void ValidateProjectPath(string validatedPath)
        {
            string dataPath = System.IO.Path.Combine(validatedPath, "Data");

            if (!System.IO.Directory.Exists(dataPath))
                System.IO.Directory.CreateDirectory(dataPath);

            //begin checking directories
            string[] paths = { "Rooms", "Zones", "Realms" };

            foreach (var path in paths)
            {
                string createPath = System.IO.Path.Combine(dataPath, path);
                if (!System.IO.Directory.Exists(createPath))
                    System.IO.Directory.CreateDirectory(createPath);
            }
        }
    }
}
