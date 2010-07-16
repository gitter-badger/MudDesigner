//MUD Engine
using MudEngine.FileSystem;
using MudEngine.GameObjects;
using MudEngine.GameObjects.Environment;

namespace MudEngine.GameObjects.Environment
{
    public struct StartingLocation
    {
        public string Room;
        public string Zone;
        public string Realm;

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Room))
                return "No initial location defined.";
            else
            {
                if (Realm == "No Realm Associated.")
                {
                    return Zone + "->" + Room;
                }
                else
                {
                    return Realm + "->" + Zone + "->" + Room;
                }
            }

        }
    }
}