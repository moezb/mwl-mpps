using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace AdminWebServer
{
    public partial class MppsSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                SharedConfig.SharedConfig config = new SharedConfig.SharedConfig();
                config.PropertyValues["MppsPort"].PropertyValue = _MppsAETTextBox.Text;
                config.PropertyValues["MppsPort"].PropertyValue = Int32.Parse(_MppsListeningPort.Text);
                config.PropertyValues["LastUpdatedDate"].PropertyValue = DateTime.Now;
                config.PropertyValues["LastUpdatedFrom"].PropertyValue = "Here";
                config.Save();
                Label3.Visible = true;
                
            }
            else
                Label3.Visible = false;

        }

        

        

       
    }
}
