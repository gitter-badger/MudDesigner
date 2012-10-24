using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudDesigner.Engine.Objects
{
    public class GameObject : IGameObject
    {
        public Guid Id { get; protected set; }

        public virtual void Save(System.IO.BinaryWriter writer)
        {
            throw new NotImplementedException();
        }

        public virtual void Load(Core.IGame game, System.IO.BinaryReader reader)
        {
            throw new NotImplementedException();
        }


        public string Name { get; set; }

        public string Description { get; set; }
    }
}
