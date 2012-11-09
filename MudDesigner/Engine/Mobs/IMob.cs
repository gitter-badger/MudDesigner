using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Core;

namespace MudDesigner.Engine.Mobs
{
    public enum MessageBroadcastLevels
    {
        Zone,
        Room,
    }

    public interface IMob : IGameObject
    {
        //Character Creation content
        IGender Gender { get; set; }
        IRace Race { get; set; }
        IClass Class { get; set; }

        IRoom Location { get; }
        bool CanTalk { get; set; }
        int MaxInventorySize { get; set; }

        void SendMessage(string message, bool newLine = true);
        void Talk(string message, MessageBroadcastLevels broadcastLevel = MessageBroadcastLevels.Room);
        void Talk(string message, IMob target);
        void Talk(string message, IMob[] group);
        //TODO: Think how this should be implemented
        //void Talk(string message, IFaction faction);

        void Move(IRoom room);

        void AddItem(IItem item);
        void AddStat(IStat stat);
        void AddAppearanceDescription(IAppearanceAttribute attribute);

        IItem[] GetItems();
        IStat[] GetStats();
        IAppearanceAttribute[] GetAppearanceDescriptions();

        void RemoveItem(IItem item);
        void RemoveItem(string item);
        void RemoveStat(IStat stat);
        void RemoveAppearanceAttribute(IAppearanceAttribute attribute);

        void ClearItems();

        void Attack(IMob target);
        void Attack(IMob[] targets); //Array for AOE attacks
        void Damage(IGameObject dealer, int amount);
        void Heal(IGameObject dealer, int amount);
        void RestoreMana(IGameObject dealer, int amount);
        void ConsumeMana(int amount);
    }
}
