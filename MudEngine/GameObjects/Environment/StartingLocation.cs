using System;

//MUD Engine
using MudEngine.FileSystem;
using MudEngine.GameObjects;
using MudEngine.GameObjects.Environment;

namespace MudEngine.GameObjects.Environment
{
    public struct StartingLocation
    {
        public String Room;
        public String Zone;
        public String Realm;

        public override String ToString()
        {
            if (String.IsNullOrEmpty(Room))
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