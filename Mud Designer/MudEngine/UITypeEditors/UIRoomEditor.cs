using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;

using MudDesigner.MudEngine.GameObjects.Environment;

namespace MudDesigner.MudEngine.UITypeEditors
{
    public class UIRoomEditor : System.Drawing.Design.UITypeEditor
    {
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            Zone obj = (Zone)context.Instance;
            
            List<string> Rooms = new List<string>();

            UIRoomControl ctl = new UIRoomControl();

            if (ctl.IsDisposed)
                //return the previous zones collection, incase the control error'd out
                //we aren't overriding and loosing content.
                return obj.Rooms;

            ctl.ShowDialog();

            while (ctl.Created)
            {
                Application.DoEvents();
            }

            if (Program.CurrentEditor is Designer)
            {
                Designer form = (Designer)Program.CurrentEditor;
                form.RefreshProjectExplorer();
            }

            return new List<Room>();
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
