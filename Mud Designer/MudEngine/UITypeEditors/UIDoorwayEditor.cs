using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.IO;

using MudDesigner.MudEngine.GameObjects.Environment;
using MudDesigner.MudEngine.GameObjects;
using MudDesigner.MudEngine.FileSystem;

namespace MudDesigner.MudEngine.UITypeEditors
{
    public class UIDoorwayEditor : UITypeEditor
    {
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            Door d = (Door)context.Instance;
            Zone z = new Zone(); 
            
            UIDoorwayControl ctl = new UIDoorwayControl(d, z);
            ctl.ShowDialog();

            return d.DoorwayLink;
        }

        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }

    }
}
