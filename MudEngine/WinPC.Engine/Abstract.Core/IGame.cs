using WinPC.Engine.Abstract.Networking;

namespace WinPC.Engine.Abstract.Core
{
    public interface IGame
    {
        string Name { get; set; }
        string Description { get; set; }
        string Version { get; set; }

        void Initialize(IServer startedServer);
        void Start();
        void Stop();
        void Update();
    }
}