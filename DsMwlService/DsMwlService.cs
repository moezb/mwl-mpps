using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using Edgeteks.MWLServer;

namespace DsMwlService
{
    public partial class DsMwlService : ServiceBase
    {
        private int _listeningPort; 
        public DsMwlService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (!MWLScp.Started)
            {
                SharedConfig.SharedConfig.Default.Reload();
                _listeningPort = SharedConfig.SharedConfig.Default.MwlPort;
                MWLScp.StartListening(SharedConfig.SharedConfig.Default.MwlAETitle,
                                      _listeningPort,
                                      SharedConfig.SharedConfig.Default.MWLResponseBufferCapacity,
                                      SharedConfig.SharedConfig.Default.MWLMaxResponsesToSend);
            }
            
        }

        protected override void OnStop()
        {
            if (!MWLScp.Started)
                MWLScp.StopListening(_listeningPort);
        }
    }
}
