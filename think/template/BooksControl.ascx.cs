using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace think.template
{
    public partial class BooksControl : System.Web.UI.UserControl
    {
        protected void addBook_Click(object sender, EventArgs e)
        {
            text.Style.Add("color","red");
            booksRow.InnerHtml = "<tr><th>1</th><td>Java</td><td>Me</td><td>150</td><td>5</td></tr><tr><th>1</th><td>Java</td><td>Me</td><td>150</td><td>5</td></tr>";
        }
    }
}