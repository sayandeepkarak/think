<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="think.pages.AdminDashboard"
    %>
    <%@ Register Src="~/template/BooksControl.ascx" TagName="BooksControl" TagPrefix="Admin" %>

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
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>    
                        <header>
                            <p class="logo">think</p>
                            <button id="logout" class="themeBtn">Logout</button>
                        </header>
                        <div class="panelBody">

                            <div class="panelSidebar">
                                <div class="nav flex-column nav-pills w-100" id="v-pills-tab" role="tablist"
                                    aria-orientation="vertical">
                                    <button class="nav-link side-tabs active" id="v-pills-books-tab" data-bs-toggle="pill"
                                        data-bs-target="#v-pills-books" type="button" role="tab" aria-controls="v-pills-books"
                                        aria-selected="true">Books</button>
                                    <button class="nav-link side-tabs" id="v-pills-users-tab" data-bs-toggle="pill"
                                        data-bs-target="#v-pills-users" type="button" role="tab" aria-controls="v-pills-users"
                                        aria-selected="false">Users</button>
                                    <button class="nav-link side-tabs" id="v-pills-activebooks-tab" data-bs-toggle="pill"
                                        data-bs-target="#v-pills-activebooks" type="button" role="tab"
                                        aria-controls="v-pills-activebooks" aria-selected="false">
                                        Active Books</button>
                                </div>
                            </div>

                            <div class="tab-content w-100 p-40" id="v-pills-tabContent">
                                <div class="tab-pane fade overflow-scroll  show active p-4 h-100" id="v-pills-books"
                                    role="tabpanel" aria-labelledby="v-pills-books-tab">
                                    <Admin:BooksControl runat="server" />
                                </div>
                                <div class="tab-pane fade p-4 h-100" id="v-pills-users" role="tabpanel"
                                    aria-labelledby="v-pills-users-tab">
                                    Users</div>
                                <div class="tab-pane fade p-4 h-100" id="v-pills-activebooks" role="tabpanel"
                                    aria-labelledby="v-pills-activebooks-tab">Active
                                    Books</div>
                            </div>
                        </div>    
                    </ContentTemplate>
                </asp:UpdatePanel>
                
            </form>
            <script src="/js/bootstrap.bundle.min.js" type="text/javascript"></script>
        </body>

        </html>