using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Objects;
using Newtonsoft.Json;

namespace MudDesigner.Engine.Environment
{
    public interface IZone : IEnvironment
    {
        IRealm Realm { get; }
        [JsonProperty(TypeNameHandling = TypeNameHandling.All)]
        Dictionary<Guid, IRoom> Rooms { get; }

        //TODO - Add a general collection of monsters that populate the entire Zone.
        //Helps you not having to insert Monsters into every room you make
        //List<IMonster> Monsters {get;}

        void AddRoom(IRoom room, bool forceOverwrite);
        void AddRooms(IRoom[] rooms, bool forceOverwrite);
        IRoom GetRoom(string roomName);
        void RemoveRoom(IRoom room);
        void RemoveRoom(Guid id);
        void DeleteRooms();
    }
}
