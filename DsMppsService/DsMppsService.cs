using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using Edgeteks.MWLServer;

namespace DsMppsService
{
    public partial class DsMppsService : ServiceBase
    {
        private int _listeningPort;
        public DsMppsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (!MPPSScp.Started)
                SharedConfig.SharedConfig.Default.Reload();
                _listeningPort = SharedConfig.SharedConfig.Default.MppsPort;
                MPPSScp.StartListening(SharedConfig.SharedConfig.Default.MppsAETitle,
                                      _listeningPort);
            
             
        }

        protected override void OnStop()
        {
            if (MPPSScp.Started)
                MPPSScp.StopListening(_listeningPort);
        }
    }
}
