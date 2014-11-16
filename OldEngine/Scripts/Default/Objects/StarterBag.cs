using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.Engine.Mobs;
using MudDesigner.Engine.Objects;

namespace MudDesigner.Scripts.Default.Objects
{
    public class StarterBag : BaseItem, IEquipable
    {
        public StarterBag()
        {
            Indestructible = true;
            Weight = 1;

            Components = new Dictionary<IItem, int>();
        }

        public Dictionary<IItem, int> Components { get; private set; }

        public void Equip(IPlayer player)
        {
            throw new NotImplementedException();
        }

        public void Unequip(IPlayer player)
        {
            throw new NotImplementedException();
        }

        public void AddComponent(IItem equipment)
        {
            //If this item is stackable, then rather than adding it to the 
            //collection, will just increase the stacked number
            if (equipment.IsStackable)
            {
                bool found = false;

                //Loop through each item in the collection
                foreach (IItem item in Components.Keys)
                {
                    //If we find a match
                    if (item == equipment)
                    {
                        //Increase the value of the stack
                        //but don't add it to the collection
                        Components[item]++;
                        found = true;
                        break;
                    }
                }

                //If we did not find an existing item, add it to the
                //collecton
                if (!found)
                    Components.Add(equipment, 1);
            }
                //if it's not stackable, then add this single item to 
                //the collection.
            else
                Components.Add(equipment, 1);
        }

        public void RemoveComponent(IItem equipment)
        {
            foreach (IItem item in Components.Keys)
            {
                if (item == equipment)
                {
                    if (item.IsStackable && Components[item] > 1)
                    {
                        Components[item]--;
                    }
                    else if (item.IsStackable && Components[item] == 1)
                    {
                        Components.Remove(item);
                    }
                    else if (!item.IsStackable)
                    {
                        Components.Remove(item);
                    }
                }
            }
        }
    }
}
