using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

namespace think.template
{
    public partial class ActiveBooks : System.Web.UI.UserControl
    {
        private string gridQuery = "SELECT a.id Id,b.bookname Book,u.fullname Student,a.issuedate Issuedate,a.returndate Returndate,a.fine Fine FROM activebooks a, books b, users u WHERE a.studentid=u.id AND a.isbn=b.isbn";


        private void clearReturnArea()
        {
            specificBooks.Items.Clear();
            specificBooks.Items.Add("Select a book");
            specificBooks.Enabled = false;
            returnBtn.Visible = false;
            returnId.Text = "";
            returnBook.Text = "";
            returnStudent.Text = "";
            returnIssueDate.Text = "";
            retDate.Text = "";
            returnFine.Text = "";
            fetchIssueBook.Enabled = false;
        }

        private void fillList(DropDownList list,SqlDataReader data,int nameIndex,int dataIndex) {
            if (data.HasRows)
            {
                while (data.Read())
                {
                    string strName = data[nameIndex].ToString();
                    string strData = data[dataIndex].ToString();
                    ListItem item = new ListItem(strName, strData);
                    list.Items.Add(item);
                }
            } 
        }

        private void fillHistory(string query = "SELECT id AS Id,operation Action,isbn BookId,studentid MemberId,hit_time Time FROM history ORDER BY Id DESC")
        {
            InternalSqlCrud crud = new InternalSqlCrud();
            crud.fillGrid(query, allHistory);
        }

        private void fillGrid(string query) {
            InternalSqlCrud crud = new InternalSqlCrud();
            crud.fillGrid(query, allActiveBooks);
        }

        private void fillInputs() {
            issueBookNames.Items.Clear();
            bookNames.Items.Clear();
            studentMobile.Items.Clear();
            returnMobiles.Items.Clear();
            renewMobiles.Items.Clear();
            issueBookNames.Items.Add("Select a book");
            specificBooks.Items.Add("Select a book");
            renewMobiles.Items.Add("Select a mobile");
            renewBookNames.Items.Add("Select a book");
            bookNames.Items.Add("Select a book");
            studentMobile.Items.Add("Select a mobile");
            returnMobiles.Items.Add("Select a mobile");
            

            InternalSqlCrud crud = new InternalSqlCrud();
            SqlDataReader data = crud.executeReader("SELECT b.isbn,b.bookname FROM books b WHERE quantity<>(SELECT COUNT(*) AS quantity FROM activebooks a WHERE a.isbn=b.isbn)");
            fillList(bookNames, data, 1, 0);

            data = crud.executeReader("SELECT distinct a.isbn,b.bookname FROM books b,activebooks a WHERE b.isbn=a.isbn");
            fillList(issueBookNames, data, 1, 0);

            data = crud.executeReader("SELECT id,mobile FROM users WHERE userType='user'");
            fillList(studentMobile, data, 1, 0);

            string today = DateTime.Now.ToString("yyyy-MM-dd");
            issuedate.Attributes["min"] = today;
            issuedate.Attributes["max"] = today;
            issuedate.Text = today;
            returndate.Attributes["min"] = today; 
            returndate.Attributes["max"] = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");
            extendedDate.Attributes["min"] = today;
            extendedDate.Attributes["max"] = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");

            data = crud.executeReader("SELECT distinct u.id,u.mobile FROM users u,activebooks a WHERE u.id=a.studentid");
            fillList(renewMobiles, data, 1, 0);

            data = crud.executeReader("SELECT distinct u.id,u.mobile FROM users u,activebooks a WHERE u.id=a.studentid");
            fillList(returnMobiles, data, 1, 0);

            specificBooks.Items.Clear();
            specificBooks.Items.Add("Select a book");
            specificBooks.Enabled = false;

        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                fillInputs();
                fillGrid(this.gridQuery); 
                fillHistory();
            }
        }

        protected void issueBook_Click(object sender, EventArgs e)
        {
            Validation validator = new Validation();

            validator.validateField(bookNames.SelectedIndex != 0, bookNames);
            validator.validateField(studentMobile.SelectedIndex != 0, studentMobile);
            validator.validateField(!String.IsNullOrEmpty(returndate.Text), returndate);

            if (validator.isOk()) {
                InternalSqlCrud crud = new InternalSqlCrud();

                string query = "INSERT INTO activebooks(isbn,studentid,issuedate,returndate,fine) ";
                query += String.Format("VALUES ('{0}','{1}','{2}','{3}','{4}')", bookNames.SelectedValue, studentMobile.SelectedValue, issuedate.Text, returndate.Text, "0");
                bool res = crud.executeCommand(query);
                string msg = res ? "Book issued successfully" : "Failed to issue book";
                if (res)
                {
                    bookNames.ClearSelection();
                    studentMobile.ClearSelection();
                    returndate.Text = "";
                    fillGrid(this.gridQuery);
                    fillInputs();
                    fillHistory();
                }
                Validation.alertInJs(issueBookPanel, res, msg);
            }
        }

        protected void issueBookNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (issueBookNames.SelectedIndex != 0)
            {
                fillGrid(this.gridQuery + " AND a.isbn=" + issueBookNames.SelectedValue);
            }
            else {
                fillGrid(this.gridQuery);
            }
        }

        protected void fetchIssueBook_Click(object sender, EventArgs e)
        {
            if (specificBooks.Enabled && specificBooks.SelectedIndex != 0)
            {
                InternalSqlCrud crud = new InternalSqlCrud();
                SqlDataReader data = crud.executeReader(this.gridQuery + " AND a.id='" + specificBooks.SelectedValue + "'");
                if (data.HasRows)
                {
                    data.Read();
                    returnId.Text = data["Id"].ToString();
                    returnBook.Text = data["Book"].ToString();
                    returnStudent.Text = data["Student"].ToString();
                    returnIssueDate.Text = data["Issuedate"].ToString();
                    retDate.Text = data["Returndate"].ToString();
                    returnFine.Text = data["Fine"].ToString();
                    if (int.Parse(data["Fine"].ToString()) == 0)
                    {
                        returnBtn.Visible = true;
                        returnBtn.Attributes.Add("data-issue-id", specificBooks.SelectedValue);
                    }
                    else {
                        returnControl.Text = "<p style='color:var(--error-border);font-size:1rem;'>Payment required</p>";
                    }
                }
            }
            else
            {
            }
        }

        protected void fillIssueBookNames(DropDownList list,string userId) {
            list.Enabled = true;
            InternalSqlCrud crud = new InternalSqlCrud();
            string query = "SELECT a.id,b.bookname FROM activebooks a,books b,users u WHERE a.studentid=u.id AND a.isbn=b.isbn AND u.id=" + userId;
            SqlDataReader data = crud.executeReader(query);
            list.Items.Add("Select a book");
            fillList(list, data, 1, 0);
        }

        protected void returnMobiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            specificBooks.Items.Clear();
            if (returnMobiles.SelectedIndex != 0)
            {
                fillIssueBookNames(specificBooks, returnMobiles.SelectedValue);
            }
            else
            {
                clearReturnArea();
            }
        }

        protected void renewMobiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            renewBookNames.Items.Clear();
            if (renewMobiles.SelectedIndex != 0)
            {
                fillIssueBookNames(renewBookNames, renewMobiles.SelectedValue);
            }
            else {
                resetRenewArea();
            }
        }

        protected void resetRenewArea() {
            renewMobiles.ClearSelection();
            renewBookNames.Items.Clear();
            renewBookNames.Items.Add("Select a book");
            renewBookNames.Enabled = false;
            clearRenewDate();
        }

        protected void returnBtn_Click(object sender, EventArgs e) 
        {
            string id = returnBtn.Attributes["data-issue-id"];
            InternalSqlCrud crud = new InternalSqlCrud();
            bool res = crud.executeCommand("DELETE FROM activebooks WHERE id=" + id);
            string msg = res ? "Book returned successfully" : "Failed to return book";
            if (res)
            {
                clearReturnArea();
                fillInputs();
                fillGrid(this.gridQuery);
                fillHistory();
            }
            Validation.alertInJs(returnBookPanel, res, msg);
        }

        protected void renewBookNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (renewBookNames.SelectedIndex != 0)
            {
                InternalSqlCrud crud = new InternalSqlCrud();
                SqlDataReader data = crud.executeReader("SELECT returndate FROM activebooks WHERE id=" + renewBookNames.SelectedValue);
                if (data.HasRows)
                {
                    data.Read();
                    extendedDate.Enabled = true;
                    extendedDate.Text = data[0].ToString();
                    renewBook.Enabled = true;
                }
            }
            else {
                clearRenewDate();
            }
        }

        protected void clearRenewDate()
        {
            extendedDate.Text = "";
            extendedDate.Enabled = false;
            renewBook.Enabled = false;
        }

        protected void renewBook_Click(object sender, EventArgs e)
        {
            InternalSqlCrud crud = new InternalSqlCrud();
            bool res = crud.executeCommand("UPDATE activebooks SET returndate='" + extendedDate.Text + "' WHERE id=" + renewBookNames.SelectedValue);
            string msg = res ? "Book renewed successfully" : "Failed to renew book";
            if (res)
            {
                resetRenewArea();
                fillGrid(this.gridQuery);
                fillHistory();
            }
            Validation.alertInJs(renewIssuePanel, res, msg);
        }

        protected void specificBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            fetchIssueBook.Enabled = specificBooks.SelectedIndex > 0;
        }

        protected void historyFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (historyFilter.SelectedIndex != 0)
            {
                fillHistory("SELECT id AS Id,operation Action,isbn BookId,studentid MemberId,hit_time Time FROM history WHERE operation='" + historyFilter.SelectedValue + "' ORDER BY Id DESC");
            }
            else {
                fillHistory();
            }
        }

    }
}