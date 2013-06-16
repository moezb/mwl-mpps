#region License

// Copyright (c) 2009, ClearCanvas Inc.
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met:
//
//    * Redistributions of source code must retain the above copyright notice, 
//      this list of conditions and the following disclaimer.
//    * Redistributions in binary form must reproduce the above copyright notice, 
//      this list of conditions and the following disclaimer in the documentation 
//      and/or other materials provided with the distribution.
//    * Neither the name of ClearCanvas Inc. nor the names of its contributors 
//      may be used to endorse or promote products derived from this software without 
//      specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, 
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR 
// PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR 
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, 
// OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE 
// GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN 
// ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
// OF SUCH DAMAGE.

#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ClearCanvas.Dicom.Codec;
using ClearCanvas.Dicom.Network.Scu;
using ClearCanvas.Dicom.Utilities.Xml;
using Edgeteks.MWLServer.SharedConfig;

namespace Edgeteks.MWLServer
{
    public partial class TestBasicSCPForm : Form
    {      
		
        private VerificationScu _verificationScu = new VerificationScu();
        

        public TestBasicSCPForm()
        {
			InitializeComponent();          
            Logger.RegisterLogHandler(this.OutputTextBox);           
			
        }

        #region Button Click Handlers       
       

        private void _buttonOutputClearLog_Click(object sender, EventArgs e)
        {
            OutputTextBox.Text = "";
        }

        private void _buttonMWScpStartStop_Click(object sender, EventArgs e)
        {

            if (MWLScp.Started)
            {
                _buttonMWScpStartStop.Text = "Start";
                MWLScp.StopListening(int.Parse(_textBoxMWLScpPort.Text));
            }
            else
            {
                _buttonMWScpStartStop.Text = "Stop";
                MWLScp.StartListening(_textBoxMWLScpAeTitle.Text,
                                       int.Parse(_textBoxMWLScpPort.Text),
                                       SharedConfig.SharedConfig.Default.MWLResponseBufferCapacity,
                                       SharedConfig.SharedConfig.Default.MWLMaxResponsesToSend);

            }
        }
        #endregion

        private void TestBasicSCPForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MWLScp.Started)
                MWLScp.StopListening(int.Parse(_textBoxMWLScpPort.Text));
        }

        private void button1_Click(object sender, EventArgs e)
        {
           worklistdbForm _dataForm = new worklistdbForm();
           _dataForm.ShowDialog();
        }

        private void _buttonMPPSScpStartStop_Click(object sender, EventArgs e)
        {
             if (MPPSScp.Started)
            {
                _buttonMPPSScpStartStop.Text = "Start";
                MPPSScp.StopListening(int.Parse(_textBoxMPPSScpPort.Text));
            }
            else
            {
                _buttonMPPSScpStartStop.Text = "Stop";
                MPPSScp.StartListening(_textBoxMPPSScpAeTitle.Text,
                    int.Parse(_textBoxMWLScpPort.Text)); 

            }
        }

        private void _showUpdatedWorkListItems_Click(object sender, EventArgs e)
        {
            worklistdbForm _dataForm = new worklistdbForm();
            _dataForm.ShowDialog();
        }

        private void _showSeriesData_Click(object sender, EventArgs e)
        {
            SeriesDataDialog _dataForm = new SeriesDataDialog();
            _dataForm.ShowDialog();
        }

       
        
    }
}