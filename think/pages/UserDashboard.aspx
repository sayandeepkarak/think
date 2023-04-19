<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDashboard.aspx.cs" Inherits="think.pages.UserDashboard" %>
<%@ Register Src="~/template/AllBooks.ascx" TagName="BooksCard" TagPrefix="Book" %>
  <!DOCTYPE html
    PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

  <html xmlns="http://www.w3.org/1999/xhtml">

  <head runat="server">
    <title>think</title>
    <link href="/css/global.css" rel="stylesheet" type="text/css" />
    <link href="/css/userpanel.css" rel="stylesheet" type="text/css" />
    <link href="/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
  </head>

  <body>
    <form id="userPanleForm" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server">
      </asp:ScriptManager>

      <header>
        <p class="logo">think</p>

        <nav id="navbar-example2" class="navbar ">
          <ul class="nav nav-pills">
            <li class="nav-item">
              <a class="nav-link" href="#homeSection">Home</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="#allBooks">Books</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="#myBooks">Bills</a>
            </li>
          </ul>
        </nav>

        <asp:Button id="logout" CssClass="themeBtn" runat="server" Text="Logout" onclick="logout_Click" />
      </header>

      <div data-bs-spy="scroll" data-bs-target="#navbar-example2" data-bs-offset="0" class="scrollspy-example"
        tabindex="0">
        <section id="homeSection" class="homeSection">

          <asp:UpdatePanel ID="textPanel" runat="server" class="homeChilds homeLeft">
            <ContentTemplate>
              <p class="homeBigText">What's up</p>
              <p class="homeBigText" id="userName" runat="server"></p>
              <p class="homeSmallText">We inform you in <span id="userEmail" runat="server"></span></p>
              <asp:Button id="openUpdateForm" runat="server" CssClass="themeBtn" Text="Profile information"
                onclick="openUpdateForm_Click" />
            </ContentTemplate>
          </asp:UpdatePanel>

          <div class="homeChilds homeRight">
            <img src="/assets/student.jpg" alt="Home" id="userPanelImage" class="homeImage" />
            <div id="userInfoform" class="forms">
              <p>Personal details</p>

              <asp:UpdatePanel class="inputWrapper" runat="server">
                <ContentTemplate>
                  <asp:TextBox ID="fullname" runat="server" CssClass="inputs" placeholder="Fullname" />
                  <asp:TextBox ID="email" TextMode="Email" runat="server" CssClass="inputs" placeholder="Email" />
                  <asp:TextBox ID="mobile" runat="server" CssClass="inputs" placeholder="Mobile" />
                  <asp:TextBox ID="password" runat="server" CssClass="inputs" placeholder="Password" />
                </ContentTemplate>
              </asp:UpdatePanel>

              <div class="formGroupBtn">
                <button type="button" class="themeBtn" id="cancleBtn">Cancle</button>
                <asp:UpdatePanel ID="updateBtnPanel" runat="server">
                  <ContentTemplate>
                    <asp:Button CssClass="themeBtn" ID="updateBtn" runat="server" Text="Update"
                      onclick="updateBtn_Click" />
                  </ContentTemplate>
                </asp:UpdatePanel>
              </div>
            </div>
          </div>
        </section>

        <Book:BooksCard id="booksCardControl" runat="server" />

        <section id="myBooks" class="sections">

        </section>
      </div>
    </form>
    <script src="/js/UserPanel.js" type="text/javascript"></script>
    <script src="/js/bootstrap.bundle.min.js" type="text/javascript"></script>
    <script src="/js/bootstrap.min.js" type="text/javascript"></script>
  </body>

  </html>