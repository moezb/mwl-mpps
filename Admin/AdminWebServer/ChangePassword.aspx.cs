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
using System.Security.Cryptography;
using System.Text;

namespace AdminWebServer
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            _badPass.Visible = false;
            if (IsPostBack)
            {
                SharedConfig.SharedConfig config = new SharedConfig.SharedConfig();
               
                Byte[] submittedPassBytes;
                Byte[] encodedBytes;
                MD5 md5;
                string amereSalt = "0ç__è-(''\"'-°\"Bhj5s9867";
                md5 = new MD5CryptoServiceProvider();
                submittedPassBytes = ASCIIEncoding.Default.GetBytes(amereSalt + this._oldpassTextBox.Text);
                encodedBytes = md5.ComputeHash(submittedPassBytes);
                string hash = BitConverter.ToString(encodedBytes);
                if (hash == config.AdminPwdHash)
                {
                    submittedPassBytes = ASCIIEncoding.Default.GetBytes(amereSalt + this._newPassBisTextBox.Text);
                    encodedBytes = md5.ComputeHash(submittedPassBytes);
                    hash = BitConverter.ToString(encodedBytes);
                    
                    config.PropertyValues["AdminPwdHash"].PropertyValue = hash;
                    config.PropertyValues["LastUpdatedDate"].PropertyValue = DateTime.Now;
                    config.PropertyValues["LastUpdatedFrom"].PropertyValue = "Here";
                    config.Save(); 
                    _badPass.Visible = false;
                    Session.Abandon();
                    
                }
                else
                    _badPass.Visible = true;
            }

        }
    }
}
