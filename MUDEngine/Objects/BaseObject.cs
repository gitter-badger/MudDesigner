using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Collections.Generic;

namespace MUDEngine.Objects
{
    public class BaseObject
    {
        [Category("Object Setup")]
        public string Name
        {
            get;
            set;
        }

        [Category("Object Setup")]
        public string Description
        {
            get;
            set;
        }

        [Browsable(false)]
        public ManagedScripting.CodeBuilding.ClassGenerator Script
        {
            get;
            set;
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }
    }
}
