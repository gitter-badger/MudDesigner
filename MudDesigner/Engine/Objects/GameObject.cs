using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudDesigner.Engine.Objects
{
    public class GameObject : IGameObject
    {
        public Guid Id
        {
            get { throw new NotImplementedException(); }
        }

        public void Save(System.IO.BinaryWriter writer)
        {
            throw new NotImplementedException();
        }

        public void Load(Core.IGame game, System.IO.BinaryReader reader)
        {
            throw new NotImplementedException();
        }


        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Description
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
