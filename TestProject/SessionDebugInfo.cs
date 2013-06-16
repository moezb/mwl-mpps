using System;
using System.Collections.Generic;
using System.Text;
using ClearCanvas.Dicom.Network;
using ClearCanvas.Dicom;
using ClearCanvas.Common;
//using ClearCanvas.Enterprise.Core;
using System.IO;
namespace Edgeteks.MWLServer
{
    [Serializable]
    public class SessionDebugInfo
    {
        public SessionDebugInfo()
        {
            _responses = new  List<string>();            
        }
        public void ResetInfo()
        {
            _responses.Clear();
            _searchCriteriaDumpString = "";
            
        }

        public void DumpSession()
        {
            string filePath = Path.Combine(
                 Platform.LogDirectory,
                 string.Format("{0:MMddyyyyhhmmss}session.bin", DateTime.Now)
                 );
            Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write);
          
            
            System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Close();
            ResetInfo();

        }
        
        public void SetAssociationDumpString(AssociationParameters ass)
        {
            _associationParams = ass.ToString();
        }

        public string _associationParams;
        public string _request;
        public List<string> _responses;
        public string _searchCriteriaDumpString;
        

    }
}
