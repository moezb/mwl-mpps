using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace AdminWebServer
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
        void Session_OnStart()
        {
            Server.Transfer("Login.aspx");
        }

        
    }
}