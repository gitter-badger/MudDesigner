using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Actions;

namespace MudDesigner.Engine.Environment
{
    public interface IZone : ILoadable, ISaveable
    {
        IRealm Realm { get; }

        Dictionary<string, Room> Rooms { get; }

        string Name { get; set; }

        //TODO - Add a general collection of monsters that populate the entire Zone.
        //Helps you not having to insert Monsters into every room you make
        //List<IMonster> Monsters {get;}

        void AddRoom(Room room, bool forceOverwrite);
        IRoom GetRoom(string roomName);
        void RemoveRoom(Room room);
    }
}
