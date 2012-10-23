using System.Collections.Generic;

using MudDesigner.Engine.Core;
using System;
using System.Collections;
using MudDesigner;
using MudDesigner.Engine.Mobs;
namespace MudDesigner.Engine.Environment
{
    public struct Senses
    {
        public string See { get; set; }
        public string Hear { get; set; }
        public string Smell { get; set; }
        public string Feel { get; set; }
        public string Taste { get; set; }
    }

    public interface IRoom
    {
        IZone Zone { get; set; }
        bool Safe { get; set; }
        string Description { get; set; }
        Senses Sense { get; set; }
        bool Enabled { get; }
        string Name { get; set; }

        Dictionary<string, IPlayer> Occupants { get; set; }
        Dictionary<AvailableTravelDirections, IDoor> Doorways { get; }

        void AddDoorway(AvailableTravelDirections direction, IRoom arrival, bool autoAddReverseDireciton, bool forceOverwrite);
        void RemoveDoorway(AvailableTravelDirections direction, bool autoRemoveReverseDirection);
        IDoor GetDoorway(AvailableTravelDirections direction);
        IDoor[] GetDoorways();
        void Destroy();
    }
}