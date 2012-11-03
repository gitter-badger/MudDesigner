using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Core;

namespace MudDesigner.Engine.Mobs
{
    public interface IMob : IGameObject
    {
        string CharacterName { get; set; }
        IGender Gender { get; set; }

        IRoom Location { get; }
        bool CanTalk { get; set; }

        Dictionary<Guid, IInventory> Inventory { get; }

        void Create(string charName, IRoom location);

        void SendMessage(string message, bool newLine = true);
        void Move(IRoom room);

        void AddInventoryItem(IInventory inventoryItem);
        void UseInventoryItem(IInventory inventoryItem);

        void OnCreate(string charName, IRoom location);
        void OnLeave(IRoom arrivalRoom, bool cancel = false);
        void OnEnter(IRoom departingRoom);
        void OnAttack(IMob[] target); //array of IMob supports AOE attacks
        void OnDealDamage(IMob[] target);
        void OnRecieveDamage(IGameObject target); //Non-IMob objects can deal damage such as traps, poisons etc.
        void OnDeath(IGameObject target);
    }
}
