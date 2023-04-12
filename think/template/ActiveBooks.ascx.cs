using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace think.template
{
    public partial class ActiveBooks : System.Web.UI.UserControl
    {
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
            issueBookNames.Items.Add("Select a book");
            bookNames.Items.Add("Select a book");
            studentMobile.Items.Add("Select a mobile");

            InternalSqlCrud crud = new InternalSqlCrud();
            SqlDataReader data = crud.executeReader("SELECT isbn,bookname FROM books");
            fillList(bookNames, data, 1, 0);
            data = crud.executeReader("SELECT id,mobile FROM users");
            fillList(studentMobile, data, 1, 0);
            issuedate.Text = DateTime.Now.AddDays(5).ToString("yyyy-MM-dd");
            data = crud.executeReader("SELECT a.id,b.bookname, FROM books b,activebooks a WHERE b.isbn=a.isbn");
            fillList(issueBookNames, data, 1, 0);
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                fillInputs();
                fillGrid("SELECT a.id Id,b.bookname Book,u.fullname Student,a.issuedate Issuedate,a.returndate Returndate,a.fine Fine FROM activebooks a, books b, users u WHERE a.studentid=u.id AND a.isbn=b.isbn");
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
                    fillGrid("SELECT a.id Id,b.bookname Book,u.fullname Student,a.issuedate Issuedate,a.returndate Returndate,a.fine Fine FROM activebooks a, books b, users u WHERE a.studentid=u.id AND a.isbn=b.isbn");
                }
            }
        }

    }
}