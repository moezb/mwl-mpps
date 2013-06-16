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
using AdminWebServer.SharedConfig;
 

namespace AdminWebServer
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (IsPostBack)
            {
                  SharedConfig.SharedConfig config = new SharedConfig.SharedConfig();
                
                  Byte[] submittedPassBytes;
                  Byte[] encodedBytes;
                  MD5 md5;
                  string amereSalt = "0ç__è-(''\"'-°\"Bhj5s9867";
                  md5 = new MD5CryptoServiceProvider();
                  submittedPassBytes = ASCIIEncoding.Default.GetBytes(amereSalt + this._pwTextBox.Text);
                  encodedBytes = md5.ComputeHash(submittedPassBytes);
                  string hash = BitConverter.ToString(encodedBytes);
                  if (hash == config.AdminPwdHash)
                  {
                      _badPasslabel.Text = "";
                      Response.Redirect("adminmain.aspx");
                  }
                  else
                      _badPasslabel.Text = "Wrong Password.";                  
            }


        }

        

       

        

        
    }
}
