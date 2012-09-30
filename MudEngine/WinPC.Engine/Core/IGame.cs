using System;
using System.Collections.Generic;

using MudDesigner.Engine.Networking;
using MudDesigner.Engine.Actions;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Scripting;
namespace MudDesigner.Engine.Core
{
    public interface IGame : ILoadable, ISaveable
    {
        string Name { get; set; }
        string Description { get; set; }
        string Version { get; set; }

        Dictionary<Guid, IGameObject> GameObjects { get; }
        DateTime LastSave { get; }

        IWorld World { get; }

        bool Initialize(IServer startedServer, IWorld world);
        void Start();
        void Stop();
    }
}