﻿using System;
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
            SqlDataReader data = crud.executeReader(query);
            SqlDataReader stockDetails;
            if (data.HasRows) {
                string booksData = "";
                int outOfStock = 0;
                while (data.Read()) {
                    string bookIsbn = data["isbn"].ToString();
                    stockDetails = crud.executeReader("SELECT COUNT(*) AS Avail FROM books WHERE isbn=" + bookIsbn + " AND quantity=(SELECT COUNT(*) AS quantity FROM activebooks WHERE isbn=" + bookIsbn + ")");
                    if (stockDetails.HasRows)
                    {
                        stockDetails.Read();
                        bool isAvail = stockDetails[0].ToString() == "0";
                        string availText = isAvail ? "In Stock" : "Out Of Stock";
                        string availClass = isAvail ? "text-success" : "text-danger";
                        if (!isAvail) {
                            outOfStock++;
                        }
                        booksData += String.Format(@"
                        <tr><td>{0}</td>
                            <td>{1}</td>
                            <td>{2}</td>
                            <td>{3}</td>
                            <td>{4}</td>
                            <td><p class='{5}'>{6}</p></td>
                        </tr>", bookIsbn, data["bookname"].ToString(), data["author"].ToString(), data["price"].ToString(), data["quantity"].ToString(), availClass, availText);
                    }
                }
                allBooksBody.InnerHtml = booksData;
                outstock.InnerText = outOfStock.ToString();
            }
        }

        private void calculateTotalBooks() {
            InternalSqlCrud crud = new InternalSqlCrud();
            SqlDataReader data = crud.executeReader("SELECT SUM(CAST(quantity AS INT)) AS Totalquantity, COUNT(*) As Totalbooks FROM books b");
            if (data.HasRows)
            {
                data.Read();
                totalbooks.InnerText = data["Totalbooks"].ToString();
                quantityBooks.InnerText = data["Totalquantity"].ToString();
            }
        }

        public void calculateStocks() {
            InternalSqlCrud crud = new InternalSqlCrud();
            SqlDataReader data = crud.executeReader("SELECT COUNT(*) AS Totalissue FROM activebooks");
            if (data.HasRows) {
                data.Read();
                totalIssue.InnerText = data["Totalissue"].ToString();
            }
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
                string msg = res ? "New book added" : "Failed to add new book";
                if (res)
                {
                    bookName.Text = "";
                    author.Text = "";
                    bookPrice.Text = "";
                    bookQuantity.Text = ""; 
                    calculateTotalBooks();
                    fillGrid("SELECT * FROM books");
                    fillId();
                }
                Validation.alertInJs(addBookPanel, res, msg);
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
                calculateTotalBooks();
                calculateStocks();
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

        protected void clearUpdateBook() {
            updateId.ClearSelection();
            updBookName.Text = "";
            updAuthor.Text = "";
            updBookPrice.Text = "";
            updBookQuantity.Text = "";
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
                InternalSqlCrud crud = new InternalSqlCrud();
                string query = "SELECT COUNT(*) AS issued FROM activebooks WHERE isbn='" + updateId.Text + "'";
                SqlDataReader data = crud.executeReader(query);
                bool isValidUpdate = true;

                if (data.HasRows) {
                    data.Read();
                    if (int.Parse(updBookQuantity.Text) < Convert.ToInt32(data[0]))
                    {
                        isValidUpdate = false;
                    }
                }
                
                if (isValidUpdate)
                {
                    updateBook.Enabled = false;
                    updateBook.Text = "Updating ...";
                    string newData = String.Format("bookname='{0}',author='{1}',price='{2}',quantity='{3}'", updBookName.Text, updAuthor.Text, updBookPrice.Text, updBookQuantity.Text);
                    query = "UPDATE books SET " + newData + " WHERE isbn=" + updateId.Text;
                    bool res = crud.executeCommand(query);
                    string msg = res ? "Book updated successfully" : "Failed to update book";
                    if (res)
                    {
                        calculateTotalBooks();
                        fillGrid("SELECT * FROM books");
                        clearUpdateBook();
                    }
                    Validation.alertInJs(updateBookPanel, res, msg);
                    updateBook.Enabled = true;
                    updateBook.Text = "Update";
                }
                else {
                    clearUpdateBook();
                    Validation.alertInJs(updateBookPanel, false, "All books are issued");
                }
            }
        }
    }
}