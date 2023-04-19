using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace think.pages
{
    public partial class UserDashboard : System.Web.UI.Page
    {

        private void clearCookie()
        {
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
            if (Request.Cookies["userId"] != null && Request.Cookies["userType"] != null && Request.Cookies["userType"].Value == "user")
            {
                string userId = Request.Cookies["userId"].Value;
                InternalSqlCrud crud = new InternalSqlCrud();
                SqlDataReader data = crud.executeReader("SELECT * FROM users WHERE id=" + userId + " AND userType='user'");
                
                if (data.HasRows)
                {
                    data.Read();
                    userName.InnerText = data["fullname"].ToString();
                    userEmail.InnerText = data["email"].ToString();
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

        protected void updateBtn_Click(object sender, EventArgs e)
        {
            Validation validator = new Validation();
            Regex mobileReg = new Regex("^[6-9]([0-9]){9}$");

            validator.validateField(fullname.Text.Length > 0, fullname);
            validator.validateField(email.Text.Length > 0, email);
            validator.validateField(mobileReg.Match(mobile.Text).Success, mobile);
            validator.validateField(password.Text.Length > 8, password);

            if(validator.isOk()){
                string userId = Request.Cookies["userId"].Value;
                InternalSqlCrud crud = new InternalSqlCrud();
                string query = String.Format("UPDATE users SET fullname='{0}',email='{1}',mobile='{2}',password='{3}' WHERE id={4}", fullname.Text, email.Text, mobile.Text, password.Text, userId);
                bool result = crud.executeCommand(query);
                if (result)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "offForm", "toggleForm(false)", true);
                    userName.InnerText = fullname.Text;
                    userEmail.InnerText = email.Text;
                }
                else {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Someting went wrong')", true);    
                }
            }

        }

        protected void openUpdateForm_Click(object sender, EventArgs e)
        {
            InternalSqlCrud crud = new InternalSqlCrud();
            string userId = Request.Cookies["userId"].Value; 
            SqlDataReader data = crud.executeReader("SELECT * FROM users WHERE id=" + userId + " AND userType='user'");
            if (data.HasRows)
            {
                data.Read();
                fullname.Text = data["fullname"].ToString();
                email.Text = data["email"].ToString();
                mobile.Text = data["mobile"].ToString();
                password.Text = data["password"].ToString(); 
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "offForm", "toggleForm(true)", true);
                    
            }
            else {
                clearCookie();
            }
        }

    }
}