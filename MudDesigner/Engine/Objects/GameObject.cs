using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

using MudDesigner.Engine.Core;

namespace MudDesigner.Engine.Objects
{
    public class GameObject : IGameObject
    {
        public Guid ID { get; protected set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; }

        public bool Permanent { get; set; }

        public bool Destroyed { get; protected set; }

        public GameObject() : this(Guid.NewGuid()) { }

        public GameObject(Guid id)
        {
            ID = id;
            Enabled = true;
        }

        public virtual void Save(BinaryWriter writer)
        {
            if (Destroyed)
                return;
        }

        public virtual void Load(IGame game, BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public void Destroy()
        {
            Destroyed = true;
            Enabled = false;
        }
    }
}
