using MudDesigner.Engine.Abstract.Networking;
using MudDesigner.Engine.Abstract.Actions;

namespace MudDesigner.Engine.Abstract.Core
{
    public interface IGame : ILoadable, ISaveable, IUpdatable
    {
        string Name { get; set; }
        string Description { get; set; }
        string Version { get; set; }

        IWorld World { get; }

        bool Initialize(IServer startedServer);
        void Start();
        void Stop();
    }
}