﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Environment;

namespace MudDesigner.Scripts.Environment
{
    public class Zone : MudDesigner.Engine.Environment.Zone
    {
        public Zone(string name, IRealm realm)
            : base(name, realm)
        {
        }
    }
}
