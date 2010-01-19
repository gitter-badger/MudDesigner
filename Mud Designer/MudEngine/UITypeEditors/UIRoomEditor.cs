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
            UIRoomControl ctl;
            
            ctl = new UIRoomControl(obj);

            ctl.ShowDialog();

            return ctl.Rooms;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}
