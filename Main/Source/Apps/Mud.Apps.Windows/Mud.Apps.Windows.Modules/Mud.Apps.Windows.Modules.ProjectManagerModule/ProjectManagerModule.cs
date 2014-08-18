using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Mud.Apps.Windows.Modules.ProjectManagerModule.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mud.Apps.Windows.Modules.ProjectManagerModule
{
    public class ProjectManagerModule : IModule
    {
        private IUnityContainer container;

        public ProjectManagerModule(IUnityContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            container.RegisterType<ProjectList>();
        }
    }
}
