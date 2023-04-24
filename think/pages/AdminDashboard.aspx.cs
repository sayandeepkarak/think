using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Diagnostics;

namespace think.pages
{
    public partial class AdminDashboard : System.Web.UI.Page
    {

        private void clearCookie() {
            Response.Cookies["userId"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["userType"].Expires = DateTime.Now.AddDays(-1);
            Response.Redirect("/");
        }

        protected void logout_Click(object sender, EventArgs e)
        {
            clearCookie();
            Response.Redirect("/");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["userId"] != null && Request.Cookies["userType"] != null && Request.Cookies["userType"].Value == "admin")
            {
                string userId = Request.Cookies["userId"].Value;
                InternalSqlCrud crud = new InternalSqlCrud();
                SqlDataReader data = crud.executeReader("SELECT * FROM users WHERE id=" + userId + " AND userType='admin'");
                if (data.HasRows)
                {
                    FineCalculator.calculateFine(true, userId);
                    data = crud.executeReader("SELECT id,fullname,email,mobile FROM users WHERE userType='user'");
                    if (data.HasRows)
                    {
                        string cards = "";
                        while (data.Read())
                        {
                            cards += String.Format(@"
                                         <div class='userCards'>
                                             <div class='userCardTop'>
                                                <p class='userCardLogo'>#think-{0}</p>
                                             </div>
                                             <div class='userCardMiddle'>
                                                 <p class='cardName'>{1}</p>
                                                 <p class='cardEmail'>{2}</p>
                                             </div>
                                             <div class='userCardBottom'>
                                                <p class='cardBottomText'>Mobile - <span> {3}</span></p>
                                             </div>
                                         </div>
                                      ", data["id"].ToString(), data["fullname"].ToString(), data["email"].ToString(), data["mobile"]);
                        }
                        userCardsArea.InnerHtml = cards;
                    }
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