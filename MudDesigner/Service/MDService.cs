using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service
{
    public partial class MDService : ServiceBase
    {
        private Thread oThread;
        private MudDesignerService _service;
        public MDService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _service = new MudDesignerService();
            oThread = new Thread(() => _service.Start(args));
            oThread.Start();

            while (!oThread.IsAlive) ;
            Thread.Sleep(1);

            while (_service.IsEnabled)
            {
                Thread.Sleep(1000);
            }
            
            
            
        }

        protected override void OnStop()
        {
           _service.StopServer();
          oThread.Abort();  
        }
    }
}
