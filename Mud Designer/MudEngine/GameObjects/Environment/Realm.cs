using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

using MudDesigner.MudEngine.FileSystem;
using MudDesigner.MudEngine.GameObjects;
using MudDesigner.MudEngine.UITypeEditors;

namespace MudDesigner.MudEngine.GameObjects.Environment
{
    public class Realm : BaseObject
    {

        [Category("Environment Information")]
        [EditorAttribute(typeof(UIRealmEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public List<string> Zones { get; set; }

        public Realm()
        {
            Zones = new List<string>();
        }

        public Zone GetZone(string ZoneName)
        {
            //correct the zonename if needed
            if (!ZoneName.EndsWith(".zone"))
                ZoneName += ".zone";

            //build our path
            string realmPath = Path.Combine(FileManager.GetDataPath(SaveDataTypes.Realms), this.Name);

            //get a collection of all the zones within the realm
            string[] files = Directory.GetFiles(realmPath, "*.zone");
            Zone zone = new Zone();

            //look four our zone file
            foreach (string file in files)
            {
                if (file == ZoneName)
                {
                    string zonePath = Path.Combine(realmPath, Path.GetDirectoryName(file));
                }
            }

            return null;
        }
    }
}
