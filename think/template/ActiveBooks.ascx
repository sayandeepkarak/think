<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActiveBooks.ascx.cs" Inherits="think.template.ActiveBooks" %>

<div class="panelsTabsArea">
    <div class="nav nav-tabs panelTabsWrapper border-0" id="nav-tab" role="tablist">
        <button class="nav-link active" id="nav-activeBooks-tab" data-bs-toggle="tab" data-bs-target="#nav-activeBooks" type="button" role="tab" aria-controls="nav-activeBooks" aria-selected="true">All</button>
        <button class="nav-link" id="nav-issueReturn-tab" data-bs-toggle="tab" data-bs-target="#nav-issueReturn" type="button" role="tab" aria-controls="nav-issueReturn" aria-selected="false">Issue_Return</button>
    </div>
</div>

<div class="tab-content" id="nav-tabContent">
    <div class="tab-pane fade show active" id="nav-activeBooks" role="tabpanel" aria-labelledby="nav-activeBooks-tab">
        <asp:UpdatePanel ID="allActiveBooksPanel" runat="server">
            <ContentTemplate>
                <div class="panelDataMiddleBlock">
                    <p>Issued books</p>
                    <asp:DropDownList ID="issueBookNames" runat="server" CssClass="inputs" AutoPostBack="True">
                    </asp:DropDownList>
                </div>    
                <div class="tableWrapper">
                    <asp:GridView ID="allActiveBooks" runat="server" CssClass="table table-striped table-hover"></asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div class="tab-pane fade" id="nav-issueReturn" role="tabpanel" aria-labelledby="nav-issueReturn-tab">
        <div class="formWrapper">
            <p>Issue book</p>
            <div id="issueBookForm">
                <asp:UpdatePanel ID="issueBookPanel" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="bookNames" runat="server" CssClass="inputs">
                        </asp:DropDownList>
                        <asp:DropDownList ID="studentMobile" runat="server" CssClass="inputs">
                        </asp:DropDownList>
                        <asp:TextBox ID="issuedate" TextMode="Date" runat="server" Enabled="false" CssClass="inputs" />
                        <asp:TextBox ID="returndate" TextMode="Date" runat="server" CssClass="inputs" />
                        <asp:Button id="issueBook" runat="server" CssClass="themeBtn ml" Text="Issue" onclick="issueBook_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="formWrapper">
            <p id="txt" runat="server">Return book</p>
            <asp:UpdatePanel ID="returnBookPanel" runat='server'>
                <ContentTemplate>
                    <div class="findBookBlock">
                        <asp:DropDownList ID="issueId" runat="server" CssClass="inputs" AutoPostBack="True"></asp:DropDownList>
                        <asp:Button id="fetchIssueBook" runat="server" CssClass="themeBtn ml" Text="Fetch" />
                    </div>
                    <table class="table table-striped table-hover">
                        <thead>
                            <tr>
                                <th>
                                    Book</th>
                                <th>
                                    Student</th>
                                <th>
                                    issuedate</th>
                                <th>
                                    returndate</th>
                                <th>
                                    fine</th>
                                <th>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>C#</td>   
                                <td>Sayandeep Karak</td>   
                                <td>10-01-2023</td>   
                                <td>17-01-2023</td>   
                                <td>14.52</td>   
                                <td>
                                    <button class="themeBtn">Return</button>
                                </td>   
                            </tr>
                        </tbody>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</div>