using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.Core;
namespace MudDesigner.Engine.Objects
{
    public abstract class BaseItem : GameObject,  IItem
    {
        public Guid ObjectBound { get; set; }

        public int Weight { get; set; }

        public int Health { get; set; }

        public bool Indestructible { get; set; }

        public BaseItem()
            : base()
        {
            InspectEvent += new InspectHandler(Inspect);
        }

        public virtual void Inspect(IPlayer player)
        {
            InspectEvent(player);
        }

        public delegate void InspectHandler(IPlayer player);
        public event InspectHandler InspectEvent;
        public virtual void OnInspect(IPlayer player)
        {
            throw new NotImplementedException();
        }

        public void OnDamage(int amount)
        {
            throw new NotImplementedException();
        }

        public void OnRepair(int amount)
        {
            throw new NotImplementedException();
        }

        public override void Save(System.IO.BinaryWriter writer)
        {
            throw new NotImplementedException();
        }

        public override void Load(Core.IGame game, System.IO.BinaryReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
