//MudEngine
using MudDesigner.MudEngine.FileSystem;
using MudDesigner.MudEngine.GameObjects;
using MudDesigner.MudEngine.GameObjects.Environment;

namespace MudDesigner.MudEngine.GameObjects.Environment
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