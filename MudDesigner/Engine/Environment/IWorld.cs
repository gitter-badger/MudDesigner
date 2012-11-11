﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Objects;
using Newtonsoft.Json;

using MudDesigner.Engine.Mobs;
namespace MudDesigner.Engine.Environment
{
    public interface IWorld : IGameObject
    {
        bool IsSafe { get; set; }
        List<IRealm> Realms { get; set; }

        void AddRealm(IRealm realm, bool overwrite = false);
        IRealm GetRealm(string realmname);
        IRealm[] GetRealms();
        void RemoveRealm(IRealm realm);
        void RemoveRealm(string realmName);

        void BroadcastMessage(string message, List<IPlayer> playersToOmit = null);

        IWorld ShallowCopy();
        IWorld DeepCopy();
    }
}
