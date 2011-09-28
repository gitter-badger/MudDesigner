//Microsoft .NET Framework
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//MUD Engine
using MudEngine.GameObjects.Items;

namespace MudEngine.GameObjects
{
    public class Bag : BaseObject
    {
        /// <summary>
        /// Gets or Sets the size of the bag.
        /// </summary>
        public Int32 Size
        {
            get;
            set;
        }

        private List<Items.BaseItem> Items { get; set; }

        public Bag(GameManagement.Game game) : base(game)
        {
        }

        public void Add(BaseItem item)
        {
            if (Items.Count < Size)
                Items.Add(item);
        }

        public Int32 GetSlotsRemaining()
        {
            return Size - Items.Count;
        }
    }
}
