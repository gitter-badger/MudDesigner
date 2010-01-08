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

            if (Program.CurrentEditor is Designer)
            {
                UIScriptControl control = new UIScriptControl(baseObj);
                control.Dock = DockStyle.Fill;
                Designer frm = (Designer)Program.CurrentEditor;
                //frm.ControlContainer.Panel1.Controls.Clear();
                //frm.ControlContainer.Panel1.Controls.Add(control);
                string script = "";

                while (control.Created)
                {
                    //if (!frm.ControlContainer.Panel1.Controls.Contains(control))
                        //break;
                    Application.DoEvents();
                    script = control.Script;
                }


                control = null;
                frm = null;

                return script;
            }

            return null;
        }

        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }
    }
}
