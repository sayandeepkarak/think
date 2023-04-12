using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace think.pages
{
    public partial class Home : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Request.Cookies["userId"] != null && Request.Cookies["userType"] != null)
                {
                    string uType = Request.Cookies["userType"].Value;
                    if (uType == "admin")
                    {
                        Response.Redirect("/AdminPanel");
                    }
                    if (uType == "user")
                    {
                        Response.Redirect("/UserPanel");
                    }
                }
            }     
        }
    }
}