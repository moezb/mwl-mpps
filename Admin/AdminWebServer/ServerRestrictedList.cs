using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SharedConfig;
using System.Xml.Linq;
using System.Collections.Specialized;

namespace mwladminwebserver
{
    public class ServerRestrictedList
    {
        public ServerRestrictedList()
        {

        }
        public XElement List
        {
            get 
            {
                XElement root = new XElement("AETRestrictionList");
                StringCollection list = Sharedconfig.SharedConfig.Default.RestrictedAET;
                foreach (string item in list)
                {
                    string[] AETItem = item.Split(',');
                    // skip eronnous values
                    if (AETItem == null) continue;
                    if (AETItem.Count() != 3) continue;

                    XElement AETXItem = new XElement("AET");
                    if (AETItem[0].Trim().Length == 0) continue;
                    AETXItem.SetAttributeValue("AETitle", AETItem[0]);
                    
                    AETXItem.SetAttributeValue("AETAdress", AETItem[1]);
                    AETXItem.SetAttributeValue("AETPort", AETItem[2]);
                    
                    root.Add(AETXItem);
                }
                return root;
            }
            set
            {
            }
        }
    }
}
