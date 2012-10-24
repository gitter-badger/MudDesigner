using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Actions;
using MudDesigner.Engine.Objects;

namespace MudDesigner.Engine.Environment
{
    public interface IZone : IEnvironment
    {
        IRealm Realm { get; }

        Dictionary<string, BaseRoom> Rooms { get; }

        //TODO - Add a general collection of monsters that populate the entire Zone.
        //Helps you not having to insert Monsters into every room you make
        //List<IMonster> Monsters {get;}

        void AddRoom(BaseRoom room, bool forceOverwrite);
        IRoom GetRoom(string roomName);
        void RemoveRoom(BaseRoom room);
        void DeleteRooms();
    }
}
