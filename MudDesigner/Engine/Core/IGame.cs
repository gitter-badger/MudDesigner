using System;
using System.Collections.Generic;

using MudDesigner.Engine.Networking;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Scripting;
namespace MudDesigner.Engine.Core
{
    public interface IGame : IGameObject
    {
        string Version { get; set; }
        string Website { get; set; }

        bool HideRoomNames { get; set; }
        bool Autosave { get; set; }
        int SaveFrequency { get; set; }

        DateTime LastSave { get; }

        IWorld World { get; set; }
        IServer Server { get; set; }
        bool Initialize(IServer startedServer);

        void RestoreWorld();
        void SaveWorld();
    }
}