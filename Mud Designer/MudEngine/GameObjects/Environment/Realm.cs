using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudDesigner.MudEngine.Objects.Environment
{
    public class Realm : BaseObject
    {
        [System.ComponentModel.Browsable(false)]
        public List<Zone> Zones { get; set; }

        public Realm()
        {
            Zones = new List<Zone>();
        }

        public Zone GetZone(string ZoneName)
        {
            foreach(Zone zone in Zones)
            {
                if (zone.Name == ZoneName)
                    return zone;
            }

            return null;
        }
    }
}
