using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudEngine.GameObjects;
using MudEngine.GameObjects.Environment;
using System.Windows.Forms;

namespace MudDesigner.Engine.UITypeEditors
{
    public class UIRealmEditor : System.Drawing.Design.UITypeEditor
    {
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            Realm obj = (Realm)context.Instance;
            List<string> zones = new List<string>();

            //Save a copy of the original
            foreach (string zone in obj.Zones)
                zones.Add(zone);

            UIRealmControl ctl = new UIRealmControl(obj);
            if (ctl.IsDisposed)
                //return the previous zones collection, incase the control error'd out
                //we aren't overriding and loosing content.
                return zones;

            ctl.ShowDialog();

            return obj.Zones;
        }

        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }
    }
}
