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

        Dictionary<Guid, IGameObject> GameObjects { get; }
        DateTime LastSave { get; }

        IWorld World { get; }

        bool Initialize(IServer startedServer);
        void Start();
        void Stop();
    }
}