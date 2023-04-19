<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AllBooks.ascx.cs" Inherits="think.template.AllBooks" %>
<section id="allBooks" class="sections">
      <div class="sectionTop">
        <p class="sectionTopText">All Books</p>
        <asp:UpdatePanel id="searchBookPanel" runat="server">
          <ContentTemplate>
            <asp:TextBox TextMode="Search" ID="searchBookName" placeholder="Book's name" 
                  runat="server" CssClass="inputs" AutoPostBack="True" runat="server" 
                  ontextchanged="searchBookName_TextChanged"></asp:TextBox>
          </ContentTemplate>
        </asp:UpdatePanel>
      </div>
      
      <asp:UpdatePanel id="allBooksPanel" class="cardScroller" runat="server">
        <ContentTemplate>
          <div id="booksCardArea" class="bookCardsArea" runat="server"></div>
        </ContentTemplate>
      </asp:UpdatePanel>
</section>