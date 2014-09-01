using Mud.Engine.Core.Character;
using Mud.Engine.Core.Environment.Travel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Engine.Core.Environment
{
    public interface IDoorway
    {
        ITravelDirection DepartureDirection { get; set; }

        IRoom ArrivalRoom { get; }

        void ConnectRooms(IRoom departureRoom, IRoom arrivalRoom);

        void DisconnectRooms(IRoom departureRoom, IRoom arrivalRoom);

        void DisconnectRoom(IRoom departureRoom, ITravelDirection travelDirection);

        void TravelThrough(ICharacter occupant);
    }
}
