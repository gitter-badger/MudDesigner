using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Apps.Windows.Modules.ProjectProperties
{
    [Module(ModuleName = "Project Properties", OnDemand = true)]
    public class ProjectPropertiesModule : IModule
    {
        private IUnityContainer container;

        public ProjectPropertiesModule(IUnityContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            // container.RegisterType<PropertiesControl>();
        }
    }
}
