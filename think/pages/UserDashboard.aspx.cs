using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace think.pages
{
    public partial class UserDashboard : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Request.Cookies["userId"] != null && Request.Cookies["userType"] != null && Request.Cookies["userType"].Value == "user")
                {
                    string userId = Request.Cookies["userId"].Value;
                    InternalSqlCrud crud = new InternalSqlCrud();
                    SqlDataReader data = crud.executeReader("SELECT * FROM users WHERE id=" + userId + " AND userType='user'");
                    if (data.HasRows)
                    {
                        data.Read();
                        textt.InnerText = data["email"].ToString();
                    }
                    else
                    {
                        Response.Redirect("/");
                    }
                }
                else
                {
                    Response.Redirect("/");
                }
            }
        }
    }
}