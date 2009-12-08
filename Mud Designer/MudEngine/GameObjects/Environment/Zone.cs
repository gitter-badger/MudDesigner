using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MudDesigner.MudEngine.GameObjects.Environment
{
    public class Zone : BaseObject
    {

        [Category("Environment Information")]
        [DefaultValue(0)]
        public int StatDrainAmount
        {
            get;
            set;
        }

        [Category("Environment Information")]
        [DefaultValue(false)]
        public bool StatDrain
        {
            get;
            set;
        }

        [ReadOnly(true)]
        [Category("Environment Information")]
        public string Realm
        {
            get;
            set;
        }
        internal List<Room> Rooms { get; set; }



        public Zone()
        {
            Rooms = new List<Room>();
        }
    }
}
