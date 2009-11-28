using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MUDEngine.Objects.Environment
{
    public class Realm : BaseObject
    {
        [System.ComponentModel.Browsable(false)]
        public List<Zone> Zones { get; set; }

        public Realm()
        {
            Zones = new List<Zone>();
        }
    }
}
