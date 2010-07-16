using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;
using System.IO;

using MudEngine.GameObjects.Environment;
using MudEngine.FileSystem;

namespace MudDesigner.Engine.UITypeEditors
{
    public class UIRoomEditor : System.Drawing.Design.UITypeEditor
    {
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            Zone obj = (Zone)context.Instance;
            UIRoomControl ctl;
            ctl = new UIRoomControl(obj);

            string zonePath = "";
            if (obj.Realm == "No Realm Associated.")
            {
                zonePath = FileManager.GetDataPath(SaveDataTypes.Zones);
                zonePath = Path.Combine(zonePath, obj.Name);
            }
            else
                zonePath = FileManager.GetDataPath(obj.Realm, obj.Name);

            string filename = Path.Combine(zonePath, obj.Filename);

            if (!File.Exists(filename))
            {
                MessageBox.Show("You must save the Zone prior to managing it's Rooms", "Mud Designer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return obj.Rooms;
            }

            ctl.ShowDialog();

            return ctl.Rooms;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
