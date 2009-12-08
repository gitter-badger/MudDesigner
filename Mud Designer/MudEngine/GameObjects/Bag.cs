using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudDesigner.MudEngine.GameObjects.Items;

namespace MudDesigner.MudEngine.GameObjects
{
    public class Bag : BaseObject
    {
        /// <summary>
        /// Gets or Sets the size of the bag.
        /// </summary>
        public int Size
        {
            get;
            set;
        }

        private List<Items.BaseItem> Items { get; set; }

        public void Add(BaseItem item)
        {
            if (Items.Count < Size)
                Items.Add(item);
        }

        public int GetSlotsRemaining()
        {
            return Size - Items.Count;
        }
    }
}
