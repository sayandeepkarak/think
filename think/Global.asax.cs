using System;
using System.Web.Routing;

namespace think
{
    public class Global : System.Web.HttpApplication
    {

        void setAllRoute(RouteCollection routes) 
        {
            routes.MapPageRoute("Home", "", "~/pages/Home.aspx");
            routes.MapPageRoute("AdminPanel", "AdminPanel/", "~/pages/AdminDashboard.aspx");
            routes.MapPageRoute("UserPanel", "UserPanel/", "~/pages/UserDashboard.aspx");
        }

        

        void Application_Start(object sender, EventArgs e)
        {
            setAllRoute(RouteTable.Routes);
        }

        void Application_End(object sender, EventArgs e)
        {
        }

        void Application_Error(object sender, EventArgs e)
        {
        }

        void Session_Start(object sender, EventArgs e)
        {
        }

        void Session_End(object sender, EventArgs e)
        {
        }

    }
}
