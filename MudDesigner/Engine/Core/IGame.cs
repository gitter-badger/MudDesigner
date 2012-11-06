using System;
using System.Collections.Generic;

using MudDesigner.Engine.Networking;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Scripting;
namespace MudDesigner.Engine.Core
{
    public interface IGame
    {
        string Name { get; set; }
        string Description { get; set; }
        string Version { get; set; }
        string Website { get; set; }

        bool HideRoomNames { get; set; }
        bool Autosave { get; set; }
        int SaveFrequency { get; set; }

        Dictionary<Guid, IGameObject> GameObjects { get; }
        DateTime LastSave { get; }

        IWorld World { get; }
        IServer Server { get; set; }
        bool Initialize(IServer startedServer);
        void Start();
        void Stop();

        void RestoreWorld();
        void SaveWorld();
    }
}