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
using System.Text;
using System.Windows.Forms;
using System.Threading;

using ClearCanvas.Common;

namespace Edgeteks.MWLServer
{
    public struct LogInfo
    {
        public DateTime Time;
        public LogLevel Level;
        public string Message;
        public int ThreadId;
    }

    public static class Logger
    {
        private static TextBox _tb;

        public static void Log(LogInfo info)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendFormat("({0}) {1} {2} ({3}) {4}", info.ThreadId,
                         info.Time.ToShortDateString(), info.Time.ToLongTimeString(),
                         info.Level, info.Message);

            AppendToTextBox(sb.ToString());
        }

        private static void AppendToTextBox(string info)
        {
            if (_tb == null)
                return;

            if (!_tb.InvokeRequired)
            {
                _tb.AppendText(info);
            }
            else
            {
                _tb.BeginInvoke(new Action<string>(AppendToTextBox), new object[] { info });
            }
        }

        private static void Log(LogLevel level, Exception e, string message, params object[] formatArgs)
        {
            if (String.IsNullOrEmpty(message))
                return;

            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(message, formatArgs);
            if (e != null)
            {
                builder.AppendLine();
                builder.Append(e);
            }

            LogInfo info = new LogInfo();
            info.Level = level;
            info.Message = builder.ToString();
            info.ThreadId = Thread.CurrentThread.ManagedThreadId;
            info.Time = DateTime.Now;

            Log(info);
        }

        public static void RegisterLogHandler(TextBox tb)
        {
            _tb = tb;
        }

        public static void LogError(string message, params object[] formatArgs)
        {
            Log(LogLevel.Error, null, message, formatArgs);
        }

        public static void LogErrorException(Exception e, string message, params object[] formatArgs)
        {
            Log(LogLevel.Error, e, message, formatArgs);
        }

        public static void LogInfo(string message, params object[] formatArgs)
        {
            Log(LogLevel.Info, null, message, formatArgs);
        }

        public static void LogWarn(string message, params object[] formatArgs)
        {
            Log(LogLevel.Warn, null, message, formatArgs);
        }

        public static void LogDebug(string message, params object[] formatArgs)
        {
            Log(LogLevel.Debug, null, message, formatArgs);
        }
    }
}
