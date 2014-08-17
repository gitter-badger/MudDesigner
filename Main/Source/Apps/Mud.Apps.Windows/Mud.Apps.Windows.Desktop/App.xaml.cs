using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using Mud.Engine.Core.Engine;
using Mud.Engine.Default.Desktop.Engine;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Mud.Apps.WinDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected async override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }
}
