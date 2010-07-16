using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.IO;

using MudEngine.GameObjects.Environment;
using MudEngine.FileSystem;

namespace MudDesigner.Engine.UITypeEditors
{
    public class UIDoorwayEditor : UITypeEditor
    {
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            Room room = new Room();
            room = (Room)context.Instance;

            UIDoorwayControl ctl = new UIDoorwayControl(room);
            ctl.ShowDialog();

            //Get an instance of the current Room Editor so we can refresh it.
            //it requires the zone it belongs to, to be passed into the constructor
            UIRoomControl roomEd =(UIRoomControl)Program.CurrentEditor;
            
            roomEd.RefreshRoomList();
            roomEd.SaveSelected();

            return room.Doorways;
        }

        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }
    }
}
