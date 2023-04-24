using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace think.pages
{
    public partial class UserDashboard : System.Web.UI.Page
    {

        private string gridQuery = "SELECT b.bookname Book,a.issuedate Issuedate,a.returndate Returndate,a.fine Fine,a.id IssueId FROM activebooks a, books b WHERE a.isbn=b.isbn";


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

        private void fillBooks(string query)
        {
            InternalSqlCrud crud = new InternalSqlCrud();
            SqlDataReader data = crud.executeReader(query);
            SqlDataReader stockDetails;
            string cards = "";
            if (data.HasRows)
            {
                string[] images = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l" };
                int count = 0;
                while (data.Read())
                {
                    string bookIsbn = data["isbn"].ToString();
                    stockDetails = crud.executeReader("SELECT COUNT(*) AS Avail FROM books WHERE isbn=" + bookIsbn + " AND quantity=(SELECT COUNT(*) AS quantity FROM activebooks WHERE isbn=" + bookIsbn + ")");
                    if (stockDetails.HasRows)
                    {
                        stockDetails.Read();
                        string availText = stockDetails[0].ToString() == "0" ? "In stock" : "Out of stock";
                        string availClass = stockDetails[0].ToString() == "0" ? "text-success" : "";
                        cards += String.Format(@"
                                <div class='booksCard'>
                                    <div class='cardImageWrapper'>
                                        <img src='/assets/{5}.jpg' class='booksCardImg' alt='x' loading='lazy'>
                                        <p class='bookName booksText'>{0}</p>
                                        <p class='bookAuthor booksText'>by<br />{1}</p>
                                    </div>
                                    <div class='cardDetailsArea'>
                                        <p class='bookPrice'>Price : ₹{2}</p>
                                        <p class='bookStock {3}'>{4}</p>
                                    </div>
                                </div>", data["bookname"].ToString(), data["author"].ToString(), data["price"].ToString(), availClass, availText, images[count]);
                        count++;
                    }
                }
            }
            else {
                cards = "<p>No books found</p>";
            }
            booksCardArea.InnerHtml = cards;
        }

        protected void loadMyBooks() {
            InternalSqlCrud crud = new InternalSqlCrud();
            SqlDataReader data = crud.executeReader(this.gridQuery);
            if (data.HasRows) {
                myBooksTable.Rows.Clear();
                TableHeaderRow headerRow = new TableHeaderRow();
                foreach (string e in new string[] { "Book","IssueDate","ReturnDate","Fine" }) {
                    TableHeaderCell cell = new TableHeaderCell();
                    cell.Text = e;
                    headerRow.Cells.Add(cell);
                }
                myBooksTable.Rows.Add(headerRow);
                while (data.Read()) {
                    TableRow row = new TableRow();
                    for (int i = 0; i <= 2; i++)
                    {
                        TableCell cell = new TableCell();
                        cell.Text = data[i].ToString();
                        row.Cells.Add(cell);
                    }
                    TableCell btnCell = new TableCell();
                    string fine = data["Fine"].ToString();
                    if (int.Parse(fine) > 0)
                    {
                        Button payFineBtn = new Button();
                        payFineBtn.CssClass = "themeBtn";
                        payFineBtn.Text = "Pay " + fine;
                        payFineBtn.Click += payFine;
                        payFineBtn.Attributes.Add("issue-id", data["IssueId"].ToString());
                        btnCell.Controls.Add(payFineBtn);
                    }
                    else {
                        btnCell.Text= fine;
                    }
                    row.Cells.Add(btnCell);
                    myBooksTable.Rows.Add(row);

                }
            }
        }
        protected void payFine(object sender, EventArgs e) {
            Button payFineBtn = (Button)sender;
            payFineBtn.Enabled = false;
            string issueId = payFineBtn.Attributes["issue-id"];
            InternalSqlCrud crud = new InternalSqlCrud();
            crud.executeCommand("UPDATE activebooks SET fine=0,returndate=CONVERT(varchar(MAX), GETDATE(), 23) WHERE id=" + issueId);
            loadMyBooks();
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
                    FineCalculator.calculateFine(false, userId);
                    this.gridQuery += " AND a.studentid=" + userId;
                    data.Read();
                    userName.InnerText = data["fullname"].ToString();
                    userEmail.InnerText = data["email"].ToString(); 
                    fillBooks("SELECT * FROM books");
                    loadMyBooks();
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


        protected void bookSearchBtn_Click(object sender, EventArgs e)
        {
            fillBooks("SELECT * FROM books WHERE bookname LIKE '%" + bookNameSearch.Text + "%'");
        }

        protected void clearBtn_Click(object sender, EventArgs e)
        {
            fillBooks("SELECT * FROM books");
            bookNameSearch.Text = "";
        }

    }
}