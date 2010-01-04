using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudDesigner.MudEngine.GameObjects;
using MudDesigner.MudEngine.GameObjects.Environment;
using System.Windows.Forms;

namespace MudDesigner.MudEngine.UITypeEditors
{
    public class UIScriptEditor : System.Drawing.Design.UITypeEditor
    {
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            object obj = context.Instance;
            Type[] types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
            BaseObject baseObj = new BaseObject();
            bool IsOk = false;

            foreach (Type type in types)
            {
                if (type == obj.GetType())
                {
                    baseObj = (BaseObject)obj;
                    IsOk = true;
                }
            }

            if (!IsOk)
            {
                MessageBox.Show("Unable to locate the Type specified\n"
                    + obj.GetType().Name);
                return null;
            }

            MudDesigner.Editors.ScriptEditor frm = new MudDesigner.Editors.ScriptEditor(baseObj);
            frm.Show();
            string script = "";

            while (frm.Created)
            {
                script = frm.Script;
                System.Windows.Forms.Application.DoEvents();
            }

            return script;
        }

        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }
    }
}
