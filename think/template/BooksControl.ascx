<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BooksControl.ascx.cs" Inherits="think.template.BooksControl" %>


<div class="panelsTabsArea">
    <div class="nav nav-tabs panelTabsWrapper border-0" id="nav-tab" role="tablist">
        <button class="nav-link active" id="nav-allbooks-tab" data-bs-toggle="tab" data-bs-target="#nav-allbooks" type="button" role="tab" aria-controls="nav-allbooks" aria-selected="true">All</button>
        <button class="nav-link" id="nav-addUpdate-tab" data-bs-toggle="tab" data-bs-target="#nav-addUpdate" type="button" role="tab" aria-controls="nav-addUpdate" aria-selected="false">Add_Update</button>
    </div>
</div>

<div class="tab-content" id="nav-tabContent">
    <div class="tab-pane fade show active" id="nav-allbooks" role="tabpanel" aria-labelledby="nav-allbooks-tab">
        <asp:UpdatePanel ID="allBookPanel" runat="server">
            <ContentTemplate>
                <div class="panelDataMiddleBlock">
                    <p>Overview</p>
                </div>    
                <div class="overviewCardsArea">
                    <div class="overviewCard">
                        <h1>10</h1>
                        <p>Total</p>
                    </div>
                    <div class="overviewCard">
                        <h1>10</h1>
                        <p>In Stock</p>
                    </div>
                    <div class="overviewCard">
                        <h1>10</h1>
                        <p>Out Of Stock</p>
                    </div>
                </div>
                <div class="panelDataMiddleBlock">
                    <p>All books</p>
                    <asp:DropDownList ID="searchId" runat="server" CssClass="inputs" AutoPostBack="True" onselectedindexchanged="searchId_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>    
                <div class="tableWrapper">
                    <asp:GridView ID="allBooks" runat="server" CssClass="table table-striped table-hover"></asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="tab-pane fade" id="nav-addUpdate" role="tabpanel" aria-labelledby="nav-addUpdate-tab">
        <div class="formWrapper">
            <p>New book</p>
            <div id="addBookForm">
                <asp:UpdatePanel ID="addBookPanel" runat="server">
                    <ContentTemplate>
                        <asp:TextBox id="bookName" class="inputs" runat="server" placeholder="Book name"></asp:TextBox>
                        <asp:TextBox id="author" class="inputs" runat="server" placeholder="Author name"></asp:TextBox>
                        <asp:TextBox TextMode="Number" id="bookPrice" class="inputs" runat="server" placeholder="Price"></asp:TextBox>
                        <asp:TextBox TextMode="Number"  id="bookQuantity" class="inputs" runat="server" placeholder="Quantity" autocomplete="false"></asp:TextBox>
                        <asp:Button id="addBook" runat="server" CssClass="themeBtn ml" Text="Add" onclick="addBook_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        
        <div class="formWrapper">
            <p>Update book</p>
            <asp:UpdatePanel ID="updateBookPanel" runat='server'>
                <ContentTemplate>
                    <div class="findBookBlock">
                        <asp:DropDownList ID="updateId" runat="server" CssClass="inputs" AutoPostBack="True"></asp:DropDownList>
                        <asp:Button id="findBook" runat="server" CssClass="themeBtn ml" Text="Find" onclick="findBook_Click" />
                    </div>
                    <div id="updateBookForm">
                        <asp:TextBox type="text" id="updBookName" class="inputs" runat="server" placeholder="Book name"></asp:TextBox>
                        <asp:TextBox type="text" id="updAuthor" class="inputs" runat="server" placeholder="Author name"></asp:TextBox>
                        <asp:TextBox type="number" id="updBookPrice" class="inputs" runat="server" placeholder="Price"></asp:TextBox>
                        <asp:TextBox type="number" id="updBookQuantity" class="inputs" runat="server" placeholder="Quantity" autocomplete="false"></asp:TextBox>
                        <asp:Button id="updateBook" runat="server" CssClass="themeBtn ml" Text="Update" onclick="updateBook_Click" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
