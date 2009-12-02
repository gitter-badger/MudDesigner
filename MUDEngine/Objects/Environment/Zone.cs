using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MUDEngine.Objects.Environment
{
    public class Zone : BaseObject
    {
        [System.ComponentModel.Browsable(false)]
        public string Realm
        {
            get;
            set;
        }
    }
}
