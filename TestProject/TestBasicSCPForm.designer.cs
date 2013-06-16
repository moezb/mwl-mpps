#region License

// Copyright (c) 2006-2007, ClearCanvas Inc.
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

namespace Edgeteks.MWLServer
{
    partial class TestBasicSCPForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SamplesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.SamplesTabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this._ShowWorkListItems = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this._textBoxMWLScpAeTitle = new System.Windows.Forms.TextBox();
            this._buttonMWScpStartStop = new System.Windows.Forms.Button();
            this._textBoxMWLScpPort = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this._textBoxMPPSScpAeTitle = new System.Windows.Forms.TextBox();
            this._buttonMPPSScpStartStop = new System.Windows.Forms.Button();
            this._textBoxMPPSScpPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._buttonOutputClearLog = new System.Windows.Forms.Button();
            this.OutputTextBox = new System.Windows.Forms.TextBox();
            this._showSeriesData = new System.Windows.Forms.Button();
            this._showUpdatedWorkListItems = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.SamplesSplitContainer.Panel1.SuspendLayout();
            this.SamplesSplitContainer.Panel2.SuspendLayout();
            this.SamplesSplitContainer.SuspendLayout();
            this.SamplesTabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // SamplesSplitContainer
            // 
            this.SamplesSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SamplesSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.SamplesSplitContainer.Name = "SamplesSplitContainer";
            this.SamplesSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SamplesSplitContainer.Panel1
            // 
            this.SamplesSplitContainer.Panel1.Controls.Add(this.SamplesTabs);
            this.SamplesSplitContainer.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // SamplesSplitContainer.Panel2
            // 
            this.SamplesSplitContainer.Panel2.Controls.Add(this.statusStrip1);
            this.SamplesSplitContainer.Panel2.Controls.Add(this._buttonOutputClearLog);
            this.SamplesSplitContainer.Panel2.Controls.Add(this.OutputTextBox);
            this.SamplesSplitContainer.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SamplesSplitContainer.Size = new System.Drawing.Size(779, 507);
            this.SamplesSplitContainer.SplitterDistance = 130;
            this.SamplesSplitContainer.TabIndex = 0;
            // 
            // SamplesTabs
            // 
            this.SamplesTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SamplesTabs.Controls.Add(this.tabPage1);
            this.SamplesTabs.Controls.Add(this.tabPage2);
            this.SamplesTabs.Location = new System.Drawing.Point(3, 0);
            this.SamplesTabs.Name = "SamplesTabs";
            this.SamplesTabs.SelectedIndex = 0;
            this.SamplesTabs.Size = new System.Drawing.Size(773, 127);
            this.SamplesTabs.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this._ShowWorkListItems);
            this.tabPage1.Controls.Add(this.label19);
            this.tabPage1.Controls.Add(this._textBoxMWLScpAeTitle);
            this.tabPage1.Controls.Add(this._buttonMWScpStartStop);
            this.tabPage1.Controls.Add(this._textBoxMWLScpPort);
            this.tabPage1.Controls.Add(this.label20);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(765, 101);
            this.tabPage1.TabIndex = 4;
            this.tabPage1.Text = "MWL SCP Server";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // _ShowWorkListItems
            // 
            this._ShowWorkListItems.Location = new System.Drawing.Point(652, 3);
            this._ShowWorkListItems.Name = "_ShowWorkListItems";
            this._ShowWorkListItems.Size = new System.Drawing.Size(108, 22);
            this._ShowWorkListItems.TabIndex = 13;
            this._ShowWorkListItems.Text = "show worklist items";
            this._ShowWorkListItems.UseVisualStyleBackColor = true;
            this._ShowWorkListItems.Click += new System.EventHandler(this.button1_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(184, 18);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(67, 13);
            this.label19.TabIndex = 12;
            this.label19.Text = "This AE Title";
            // 
            // _textBoxMWLScpAeTitle
            // 
            this._textBoxMWLScpAeTitle.Location = new System.Drawing.Point(184, 36);
            this._textBoxMWLScpAeTitle.Name = "_textBoxMWLScpAeTitle";
            this._textBoxMWLScpAeTitle.Size = new System.Drawing.Size(124, 20);
            this._textBoxMWLScpAeTitle.TabIndex = 11;
            this._textBoxMWLScpAeTitle.Text = "DSMWL";
            // 
            // _buttonMWScpStartStop
            // 
            this._buttonMWScpStartStop.Location = new System.Drawing.Point(9, 33);
            this._buttonMWScpStartStop.Name = "_buttonMWScpStartStop";
            this._buttonMWScpStartStop.Size = new System.Drawing.Size(75, 23);
            this._buttonMWScpStartStop.TabIndex = 10;
            this._buttonMWScpStartStop.Text = "Start";
            this._buttonMWScpStartStop.UseVisualStyleBackColor = true;
            this._buttonMWScpStartStop.Click += new System.EventHandler(this._buttonMWScpStartStop_Click);
            // 
            // _textBoxMWLScpPort
            // 
            this._textBoxMWLScpPort.Location = new System.Drawing.Point(357, 36);
            this._textBoxMWLScpPort.Name = "_textBoxMWLScpPort";
            this._textBoxMWLScpPort.Size = new System.Drawing.Size(100, 20);
            this._textBoxMWLScpPort.TabIndex = 9;
            this._textBoxMWLScpPort.Text = "2112";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(354, 19);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(26, 13);
            this.label20.TabIndex = 8;
            this.label20.Text = "Port";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this._showUpdatedWorkListItems);
            this.tabPage2.Controls.Add(this._showSeriesData);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this._textBoxMPPSScpAeTitle);
            this.tabPage2.Controls.Add(this._buttonMPPSScpStartStop);
            this.tabPage2.Controls.Add(this._textBoxMPPSScpPort);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(765, 101);
            this.tabPage2.TabIndex = 5;
            this.tabPage2.Text = "MPPS SCP Server";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(184, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "This AE Title";
            // 
            // _textBoxMPPSScpAeTitle
            // 
            this._textBoxMPPSScpAeTitle.Location = new System.Drawing.Point(184, 40);
            this._textBoxMPPSScpAeTitle.Name = "_textBoxMPPSScpAeTitle";
            this._textBoxMPPSScpAeTitle.Size = new System.Drawing.Size(124, 20);
            this._textBoxMPPSScpAeTitle.TabIndex = 16;
            this._textBoxMPPSScpAeTitle.Text = "MPPSSCP";
            // 
            // _buttonMPPSScpStartStop
            // 
            this._buttonMPPSScpStartStop.Location = new System.Drawing.Point(9, 37);
            this._buttonMPPSScpStartStop.Name = "_buttonMPPSScpStartStop";
            this._buttonMPPSScpStartStop.Size = new System.Drawing.Size(75, 23);
            this._buttonMPPSScpStartStop.TabIndex = 15;
            this._buttonMPPSScpStartStop.Text = "Start";
            this._buttonMPPSScpStartStop.UseVisualStyleBackColor = true;
            this._buttonMPPSScpStartStop.Click += new System.EventHandler(this._buttonMPPSScpStartStop_Click);
            // 
            // _textBoxMPPSScpPort
            // 
            this._textBoxMPPSScpPort.Location = new System.Drawing.Point(357, 40);
            this._textBoxMPPSScpPort.Name = "_textBoxMPPSScpPort";
            this._textBoxMPPSScpPort.Size = new System.Drawing.Size(100, 20);
            this._textBoxMPPSScpPort.TabIndex = 14;
            this._textBoxMPPSScpPort.Text = "2113";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(354, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Port";
            // 
            // _buttonOutputClearLog
            // 
            this._buttonOutputClearLog.Location = new System.Drawing.Point(16, 3);
            this._buttonOutputClearLog.Name = "_buttonOutputClearLog";
            this._buttonOutputClearLog.Size = new System.Drawing.Size(75, 23);
            this._buttonOutputClearLog.TabIndex = 1;
            this._buttonOutputClearLog.Text = "Clear Log";
            this._buttonOutputClearLog.UseVisualStyleBackColor = true;
            this._buttonOutputClearLog.Click += new System.EventHandler(this._buttonOutputClearLog_Click);
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputTextBox.Location = new System.Drawing.Point(3, 3);
            this.OutputTextBox.MaxLength = 65536;
            this.OutputTextBox.Multiline = true;
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.OutputTextBox.Size = new System.Drawing.Size(773, 345);
            this.OutputTextBox.TabIndex = 0;
            this.OutputTextBox.WordWrap = false;
            // 
            // _showSeriesData
            // 
            this._showSeriesData.Location = new System.Drawing.Point(649, 6);
            this._showSeriesData.Name = "_showSeriesData";
            this._showSeriesData.Size = new System.Drawing.Size(108, 23);
            this._showSeriesData.TabIndex = 18;
            this._showSeriesData.Text = "Show Series data";
            this._showSeriesData.UseVisualStyleBackColor = true;
            this._showSeriesData.Click += new System.EventHandler(this._showSeriesData_Click);
            // 
            // _showUpdatedWorkListItems
            // 
            this._showUpdatedWorkListItems.Location = new System.Drawing.Point(648, 35);
            this._showUpdatedWorkListItems.Name = "_showUpdatedWorkListItems";
            this._showUpdatedWorkListItems.Size = new System.Drawing.Size(108, 22);
            this._showUpdatedWorkListItems.TabIndex = 20;
            this._showUpdatedWorkListItems.Text = "show worklist items";
            this._showUpdatedWorkListItems.UseVisualStyleBackColor = true;
            this._showUpdatedWorkListItems.Click += new System.EventHandler(this._showUpdatedWorkListItems_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 351);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(779, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // TestBasicSCPForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 507);
            this.Controls.Add(this.SamplesSplitContainer);
            this.Name = "TestBasicSCPForm";
            this.Text = "Test Form";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TestBasicSCPForm_FormClosed);
            this.SamplesSplitContainer.Panel1.ResumeLayout(false);
            this.SamplesSplitContainer.Panel2.ResumeLayout(false);
            this.SamplesSplitContainer.Panel2.PerformLayout();
            this.SamplesSplitContainer.ResumeLayout(false);
            this.SamplesTabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer SamplesSplitContainer;
        private System.Windows.Forms.TabControl SamplesTabs;
        private System.Windows.Forms.TextBox OutputTextBox;
        private System.Windows.Forms.Button _buttonOutputClearLog;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox _textBoxMWLScpAeTitle;
        private System.Windows.Forms.Button _buttonMWScpStartStop;
        private System.Windows.Forms.TextBox _textBoxMWLScpPort;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button _ShowWorkListItems;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _textBoxMPPSScpAeTitle;
        private System.Windows.Forms.Button _buttonMPPSScpStartStop;
        private System.Windows.Forms.TextBox _textBoxMPPSScpPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button _showUpdatedWorkListItems;
        private System.Windows.Forms.Button _showSeriesData;
        private System.Windows.Forms.StatusStrip statusStrip1;
    }
}

