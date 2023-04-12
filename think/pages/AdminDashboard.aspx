<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="think.pages.AdminDashboard"
    %>
    <%@ Register Src="~/template/BooksControl.ascx" TagName="BooksControl" TagPrefix="Admin" %>
        <%@ Register Src="~/template/ActiveBooks.ascx" TagName="ActiveBooks" TagPrefix="Admin" %>

            <!DOCTYPE html
                PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

            <html xmlns="http://www.w3.org/1999/xhtml">

            <head runat="server">
                <title>AdminPanel</title>
                <link href="/css/global.css" rel="stylesheet" type="text/css" />
                <link href="/css/panel.css" rel="stylesheet" type="text/css" />
                <link href="/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
                <link href="/css/adminPanel.css" rel="stylesheet" type="text/css" />
            </head>

            <body>
                <form id="mainForm" runat="server">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <header>
                        <p class="logo">think</p>
                        <asp:Button id="logout" CssClass="themeBtn" runat="server" Text="Logout"
                            onclick="logout_Click" />
                    </header>
                    <div class="panelBody">
                        <div class="panelSidebar">
                            <div class="nav flex-column nav-pills w-100" id="v-pills-tab" role="tablist"
                                aria-orientation="vertical">
                                <button class="nav-link side-tabs active" id="v-pills-books-tab" data-bs-toggle="pill"
                                    data-bs-target="#v-pills-books" type="button" role="tab"
                                    aria-controls="v-pills-books" aria-selected="true">Books</button>
                                <button class="nav-link side-tabs" id="v-pills-issuereturn-tab" data-bs-toggle="pill"
                                    data-bs-target="#v-pills-issuereturn" type="button" role="tab"
                                    aria-controls="v-pills-issuereturn" aria-selected="false">
                                    Issue_Return</button>
                                <button class="nav-link side-tabs" id="v-pills-users-tab" data-bs-toggle="pill"
                                    data-bs-target="#v-pills-users" type="button" role="tab"
                                    aria-controls="v-pills-users" aria-selected="false">Users</button>
                            </div>
                        </div>

                        <div class="tab-content w-100 p-40" id="v-pills-tabContent">
                            <div class="tab-pane fade overflow-auto  show active h-100 tabContent" id="v-pills-books"
                                role="tabpanel" aria-labelledby="v-pills-books-tab">
                                <Admin:BooksControl ID="booksControl" runat="server" />
                            </div>
                            <div class="tab-pane fade h-100 tabContent" id="v-pills-issuereturn" role="tabpanel"
                                aria-labelledby="v-pills-issuereturn-tab">
                                <Admin:ActiveBooks ID="activeBooksControl" runat="server" />
                            </div>
                            <div class="tab-pane fade p-4 h-100" id="v-pills-users" role="tabpanel"
                                aria-labelledby="v-pills-users-tab">
                                Users</div>
                        </div>
                    </div>
                </form>
                <script src="/js/bootstrap.bundle.min.js" type="text/javascript"></script>
            </body>

            </html>