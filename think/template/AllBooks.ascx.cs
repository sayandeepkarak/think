using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace think.template
{
    public partial class AllBooks : System.Web.UI.UserControl
    {
        private void fillBooks(string query)
        {
            InternalSqlCrud crud = new InternalSqlCrud();
            SqlDataReader data = crud.executeReader(query);
            SqlDataReader stockDetails;
            if (data.HasRows)
            {
                string cards = "";
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
                booksCardArea.InnerHtml = cards;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            fillBooks("SELECT * FROM books");
        }

        protected void searchBookName_TextChanged(object sender, EventArgs e)
        {
            fillBooks("SELECT * FROM books WHERE bookname LIKE '%" + searchBookName.Text + "%'");
        }
    }
}