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

        private void fillGrid(string query) {
            InternalSqlCrud crud = new InternalSqlCrud();
            crud.fillGrid(query, allActiveBooks);
        }

        private void fillInputs() {
            issueBookNames.Items.Clear();
            bookNames.Items.Clear();
            studentMobile.Items.Clear();
            returnMobiles.Items.Clear();
            issueBookNames.Items.Add("Select a book");
            specificBooks.Items.Add("Select a book");
            bookNames.Items.Add("Select a book");
            studentMobile.Items.Add("Select a mobile");
            returnMobiles.Items.Add("Select a mobile");

            InternalSqlCrud crud = new InternalSqlCrud();
            SqlDataReader data = crud.executeReader("SELECT b.isbn,b.bookname FROM books b WHERE quantity<>(SELECT COUNT(*) AS quantity FROM activebooks a WHERE a.isbn=b.isbn)");
            fillList(bookNames, data, 1, 0);

            data = crud.executeReader("SELECT id,mobile FROM users");
            fillList(studentMobile, data, 1, 0);
            issuedate.Text = DateTime.Now.AddDays(5).ToString("yyyy-MM-dd");

            data = crud.executeReader("SELECT distinct a.isbn,b.bookname FROM books b,activebooks a WHERE b.isbn=a.isbn");
            fillList(issueBookNames, data, 1, 0);

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

                if (res) {
                    bookNames.ClearSelection();
                    studentMobile.ClearSelection();
                    returndate.Text = "";
                    fillGrid(this.gridQuery);
                    fillInputs();
                }
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

        protected void returnMobiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            specificBooks.Items.Clear();
            if (returnMobiles.SelectedIndex != 0)
            {
                specificBooks.Enabled = true;
                InternalSqlCrud crud = new InternalSqlCrud();
                string query = "SELECT a.id,b.bookname FROM activebooks a,books b,users u WHERE a.studentid=u.id AND a.isbn=b.isbn AND u.id=" + returnMobiles.SelectedValue;
                SqlDataReader data = crud.executeReader(query);
                specificBooks.Items.Add("Select a book");
                fillList(specificBooks, data, 1, 0);
            }
            else
            {
                clearReturnArea();
            }
        }

        protected void returnBtn_Click(object sender, EventArgs e) 
        {
            string id = returnBtn.Attributes["data-issue-id"];
            InternalSqlCrud crud = new InternalSqlCrud();
            bool res = crud.executeCommand("DELETE FROM activebooks WHERE id=" + id);
            clearReturnArea();
            fillInputs();
            fillGrid(this.gridQuery);
        }
    }
}