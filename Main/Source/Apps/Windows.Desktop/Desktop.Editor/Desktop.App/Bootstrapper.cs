using Microsoft.Practices.Prism.UnityExtensions;
using Mud.Apps.WinDesktop.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using System.Windows;
using Microsoft.Practices.Prism.Regions;
using Mud.Apps.Windows.Infrastructure.RegionAdapters;
using Microsoft.Practices.Prism.Modularity;
using System.IO;
using Microsoft.Practices.Prism.PubSubEvents;
using Mud.Engine.Core.Engine;
using Mud.Engine.Core.Environment;
using Mud.Repositories.Shared;
using Mud.Repositories.Engine.DefaultDesktop;

namespace Mud.Apps.WinDesktop
{
    public class Bootstrapper : UnityBootstrapper
    {
        /// <summary>
        /// Creates the shell or main window of the application.
        /// </summary>
        /// <returns>
        /// The shell of the application.
        /// </returns>
        /// <remarks>
        /// If the returned instance is a <see cref="T:System.Windows.DependencyObject" />, the
        /// <see cref="T:Microsoft.Practices.Prism.Bootstrapper" /> will attach the default <see cref="T:Microsoft.Practices.Prism.Regions.IRegionManager" /> of
        /// the application in its <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionManagerProperty" /> attached property
        /// in order to be able to add regions by using the <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionNameProperty" />
        /// attached property from XAML.
        /// </remarks>
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        /// <summary>
        /// Initializes the shell.
        /// </summary>
        protected override void InitializeShell()
        {
            base.InitializeShell();

            App.Current.MainWindow = (Window)Shell;
            App.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            this.Container.RegisterType<IEventAggregator>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IWorld, DefaultWorld>();
            this.Container.RegisterType<IWorldRepository, WorldRepository>(new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// Configures the default region adapter mappings to use in the application, in order
        /// to adapt UI controls defined in XAML to use a region and register it automatically.
        /// May be overwritten in a derived class to add specific mappings required by the application.
        /// </summary>
        /// <returns>
        /// The <see cref="T:Microsoft.Practices.Prism.Regions.RegionAdapterMappings" /> instance containing all the mappings.
        /// </returns>
        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();
            mappings.RegisterMapping(typeof(StackPanelRegionAdapter), Container.Resolve<StackPanelRegionAdapter>());

            return mappings;
        }

        protected override Microsoft.Practices.Prism.Modularity.IModuleCatalog CreateModuleCatalog()
        {
            if (!Directory.Exists(@".\Plugins"))
            {
                Directory.CreateDirectory(@".\Plugins");
            }

            return new DirectoryModuleCatalog { ModulePath = @".\Plugins" };
        }
    }
}
