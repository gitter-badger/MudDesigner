using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.IO;

using MudDesigner.MudEngine.GameObjects.Environment;
using MudDesigner.MudEngine.FileSystem;

namespace MudDesigner.MudEngine.UITypeEditors
{
    public class UIDoorwayEditor : UITypeEditor
    {
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            Room room = new Room();
            room = (Room)context.Instance;

            UIDoorwayControl ctl = new UIDoorwayControl(room);
            ctl.ShowDialog();

            //Load the Zone that this room belongs to.
            string zonePath = "";

            if (room.Realm == "No Realm Associated.")
            {
                zonePath = FileManager.GetDataPath(SaveDataTypes.Zones);
            }
            else
            {
                zonePath = FileManager.GetDataPath(room.Realm, room.Zone);
            }

            zonePath = Path.Combine(zonePath, room.Zone);
            string zoneFile = Path.Combine(zonePath, room.Zone + ".zone");
            Zone z = new Zone();
            z = (Zone)z.Load(zoneFile);

            //Get an instance of the current Room Editor so we can refresh it.
            //it requires the zone it belongs to, to be passed into the constructor
            UIRoomControl roomEd =(UIRoomControl)Program.CurrentEditor;
            
            roomEd.RefreshRoomList();
            
            return room.Doorways;
        }

        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }
    }
}
