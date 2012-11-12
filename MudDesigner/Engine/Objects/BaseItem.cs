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
        public int Weight { get; set; }

        public int Health { get; set; }

        public bool Indestructible { get; set; }

        public bool IsAdminOnly { get; set; }

        public override void CopyState(ref dynamic copyTo)
        {
            if (copyTo is IItem)
            {
                Scripting.ScriptObject newObject = new Scripting.ScriptObject(copyTo);

                newObject.SetProperty("Weight", Weight, null);
                newObject.SetProperty("Health", Health, null);
                newObject.SetProperty("Indestructible", Indestructible, null);
                newObject.SetProperty("IsAdminOnly", IsAdminOnly, null);
            }

            base.CopyState(ref copyTo);
        }
        public virtual void Inspect(IPlayer player)
        {
            throw new NotImplementedException();
        }

        public void Repair(IMob healder, int amount)
        {
        }

        public void Damage(IMob dealer, int amount)
        {
            throw new NotImplementedException();
        }
    }
}
