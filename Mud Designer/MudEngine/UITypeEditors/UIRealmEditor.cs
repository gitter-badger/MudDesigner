using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudDesigner.MudEngine.GameObjects;
using MudDesigner.MudEngine.GameObjects.Environment;
using System.Windows.Forms;

namespace MudDesigner.MudEngine.UITypeEditors
{
    public class UIRealmEditor : System.Drawing.Design.UITypeEditor
    {
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            Realm obj = (Realm)context.Instance;
            List<string> zones = new List<string>();
            UIRealmControl ctl = new UIRealmControl(obj);
            if (ctl.IsDisposed)
                //return the previous zones collection, incase the control error'd out
                //we aren't overriding and loosing content.
                return obj.Zones;

            ctl.ShowDialog();

            return obj.Zones;
        }

        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }
    }
}
