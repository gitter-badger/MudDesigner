﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Core;

namespace MudDesigner.Engine.Environment
{
    
    public class EngineWorld : IWorld, IGameObject
    {
        public Dictionary<string, IRealm> Realms { get; protected set; }

        public string Name { get; set; }

        public EngineWorld()
        {
            Realms = new Dictionary<string, IRealm>();
            Name = "World";
        }

        public IRealm GetRealm(string realmName)
        {
            IRealm realm;
            Realms.TryGetValue(realmName, out realm);

            return realm;
        }

        public void AddRealm(IRealm realm)
        {
            Realm r = (Realm)realm;

            if (!Realms.Values.Contains<IRealm>(realm))
                Realms.Add(r.Name, r);
        }

        public void Create(string name)
        {
            throw new NotImplementedException();
        }

        public void Create(string name, List<IRealm> realms)
            {
                throw new NotImplementedException();
            }


        public void Save(System.IO.BinaryWriter writer)
        {
            throw new NotImplementedException();
        }

        public void Load(IGame game, System.IO.BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public new GameObjectType Type
        {
            get { return new GameObjectType(); }
        }

        public Guid Id
        {
            get { throw new NotImplementedException(); }
        }
    }
}