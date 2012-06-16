using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinPC.Engine.Abstract.Actions;

namespace WinPC.Engine.Abstract.Core
{
    public interface IWorld : ILoadable, ISaveable, IUpdatable
    {
        List<IRealm> Realms { get; }
        string Name { get; set; }

        void Create(string name);
        void Create(string name, List<IRealm> realms);
    }
}
