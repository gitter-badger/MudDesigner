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
        [Description("A collection of Zones that are contained within this Realm. Players can traverse the world be traveling through Rooms that are contained within Zones. Note that it is not required to place a Zone into a Realm.")]
        [EditorAttribute(typeof(UIRealmEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public List<string> Zones { get; set; }

        public Realm()
        {
            Zones = new List<string>();
        }

        /// <summary>
        /// Returns the requested Zone if the Zone exists within this Realm.
        /// </summary>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public Zone GetZone(string filename)
        {
            var filterQuery =
                from zone in Zones
                where zone == filename
                select zone;

            Zone z = new Zone();
            foreach (var zone in filterQuery)
                return (Zone)z.Load(zone); ;

            return null;
        }
    }
}
