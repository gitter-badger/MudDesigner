using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudDesigner.Engine.Environment;
using Newtonsoft.Json;

namespace MudDesigner.Scripts.Environment
{
    [JsonObject(IsReference = true)]
    public class Realm : MudDesigner.Engine.Environment.BaseRealm
    {
        public Realm(string name) : base(name)
        {
        }
    }
}
