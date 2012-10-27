using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Environment;

namespace MudDesigner.Editor
{
    public static class EngineEditor
    {
        public static IGame Game { get; set; }

        public static IRealm CurrentRealm { get; set; }
        public static IZone CurrentZone { get; set; }
        public static IRoom CurrentRoom { get; set; }
    }
}
