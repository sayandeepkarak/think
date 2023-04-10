using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace think.pages
{
    public partial class AdminDashboard : System.Web.UI.Page
    {

        private void clearCookie() {
            Response.Cookies["userId"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["userType"].Expires = DateTime.Now.AddDays(-1);
            Response.Redirect("/");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Request.Cookies["userId"] != null && Request.Cookies["userType"] !=null && Request.Cookies["userType"].Value == "admin")
                {
                    string userId = Request.Cookies["userId"].Value;
                    InternalSqlCrud crud = new InternalSqlCrud();
                    SqlDataReader data = crud.executeReader("SELECT * FROM users WHERE id=" + userId+" AND userType='admin'");
                    if (data.HasRows)
                    {
                        data.Read();
                    }
                    else
                    {
                        clearCookie();
                    }
                }
                else
                {
                    clearCookie();
                }
            }
        }
    }
}