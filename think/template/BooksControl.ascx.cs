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
    public partial class BooksControl : System.Web.UI.UserControl
    {
        private void fillGrid(string query) {
            InternalSqlCrud crud = new InternalSqlCrud();
            crud.fillGrid(query,allBooks);
        }

        private void fillId() {
            InternalSqlCrud crud = new InternalSqlCrud();
            SqlDataReader data = crud.executeReader("SELECT isbn FROM books");
            searchId.Items.Clear();
            updateId.Items.Clear();
            searchId.Items.Add("Select an Isbn");
            updateId.Items.Add("Select an Isbn");
            if (data.HasRows) {
                while (data.Read())
                {
                    ListItem item = new ListItem(data[0].ToString(), data[0].ToString());
                    searchId.Items.Add(item);
                    updateId.Items.Add(item);
                }
            }
        }

        protected void addBook_Click(object sender, EventArgs e)
        {
            Validation validator = new Validation();

            validator.validateField(bookName.Text.Length > 0, bookName);
            validator.validateField(author.Text.Length > 0, author);
            validator.validateField(bookPrice.Text.Length > 0, bookPrice);
            validator.validateField(bookQuantity.Text.Length > 0, bookQuantity);

            if (validator.isOk()) {
                addBook.Text = "Adding...";
                addBook.Enabled = false;
                InternalSqlCrud crud = new InternalSqlCrud();
                string query = "INSERT INTO books(bookname,author,price,quantity) ";
                query += "VALUES('" + bookName.Text + "','" + author.Text + "','" + bookPrice.Text + "','"+bookQuantity.Text+"')";
                bool res = crud.executeCommand(query);
                string resText = res ? "Successfully added" : "Failed to add";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + resText + "')", true);
                if (res)
                {
                    bookName.Text = "";
                    author.Text = "";
                    bookPrice.Text = "";
                    bookQuantity.Text = "";
                    fillGrid("SELECT * FROM books");
                    fillId();
                }
                addBook.Text = "Add";
                addBook.Enabled = true;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                fillGrid("SELECT * FROM books");
                fillId();
            }
        }

        protected void searchId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (searchId.SelectedIndex != 0)
            {
                fillGrid("SELECT * FROM books WHERE isbn=" + searchId.Text);
            }
            else {
                fillGrid("SELECT * FROM books");
            }
        }

        protected void findBook_Click(object sender, EventArgs e)
        {
            if (updateId.SelectedIndex != 0) {
                InternalSqlCrud crud = new InternalSqlCrud();
                string query = "SELECT * FROM books WHERE isbn=" + updateId.Text;
                SqlDataReader data = crud.executeReader(query);
                if (data.HasRows) {
                    data.Read();
                    updBookName.Text = data["bookname"].ToString();
                    updAuthor.Text = data["author"].ToString();
                    updBookPrice.Text = data["price"].ToString();
                    updBookQuantity.Text = data["quantity"].ToString();
                }
            }
        }

        protected void updateBook_Click(object sender, EventArgs e)
        {
            Validation validator = new Validation();

            validator.validateField(updateId.SelectedIndex != 0,updateId);
            validator.validateField(updBookName.Text.Length > 0, updBookName);
            validator.validateField(updAuthor.Text.Length > 0, updAuthor);
            validator.validateField(updBookPrice.Text.Length > 0, updBookPrice);
            validator.validateField(updBookQuantity.Text.Length > 0,updBookQuantity);

            if(validator.isOk()){
                updateBook.Enabled = false;
                updateBook.Text = "Updating ...";
                InternalSqlCrud crud = new InternalSqlCrud();
                string newData = String.Format("bookname='{0}',author='{1}',price='{2}',quantity='{3}'", updBookName.Text, updAuthor.Text, updBookPrice.Text, updBookQuantity.Text);
                string query = "UPDATE books SET " + newData + " WHERE isbn=" + updateId.Text;
                bool res = crud.executeCommand(query);
                if (res)
                {
                    fillGrid("SELECT * FROM books");
                    updateId.ClearSelection();
                    updBookName.Text = "";
                    updAuthor.Text = "";
                    updBookPrice.Text = "";
                    updBookQuantity.Text = "";
                }
                updateBook.Enabled = true;
                updateBook.Text = "Update";
            }
        }
    }
}