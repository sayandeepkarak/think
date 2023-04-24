<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActiveBooks.ascx.cs" Inherits="think.template.ActiveBooks" %>

<div class="panelsTabsArea">
    <div class="nav nav-tabs panelTabsWrapper border-0" id="nav-tab" role="tablist">
        <button class="nav-link active" id="nav-activeBooks-tab" data-bs-toggle="tab" data-bs-target="#nav-activeBooks" type="button" role="tab" aria-controls="nav-activeBooks" aria-selected="true">All</button>
        <button class="nav-link" id="nav-issueReturn-tab" data-bs-toggle="tab" data-bs-target="#nav-issueReturn" type="button" role="tab" aria-controls="nav-issueReturn" aria-selected="false">Issue_Return</button>
        <button class="nav-link" id="nav-history-tab" data-bs-toggle="tab" data-bs-target="#nav-history" type="button" role="tab" aria-controls="nav-history" aria-selected="false">History</button>
    </div>
</div>

<div class="tab-content" id="nav-tabContent">
    <div class="tab-pane fade show active" id="nav-activeBooks" role="tabpanel" aria-labelledby="nav-activeBooks-tab">
        <asp:UpdatePanel ID="allActiveBooksPanel" class="tableControlWrap" runat="server">
            <ContentTemplate>
                <div class="panelDataMiddleBlock">
                    <p>Issued books</p>
                    <asp:DropDownList ID="issueBookNames" runat="server" CssClass="inputs" 
                        AutoPostBack="True" 
                        onselectedindexchanged="issueBookNames_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>    
                <div class="tableWrapper">
                    <asp:GridView ID="allActiveBooks" runat="server" CssClass="table table-striped table-hover"></asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div class="tab-pane fade" id="nav-issueReturn" role="tabpanel" aria-labelledby="nav-issueReturn-tab">
        <div class="group-forms">
            <div class="formWrapper">
                <p>Issue book</p>
                <div id="issueBookForm">
                    <asp:UpdatePanel ID="issueBookPanel" class="column-form" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="bookNames" runat="server" CssClass="inputs">
                            </asp:DropDownList>
                            <asp:DropDownList ID="studentMobile" runat="server" CssClass="inputs">
                            </asp:DropDownList>
                            <asp:TextBox ID="issuedate" TextMode="Date" runat="server" CssClass="inputs" />
                            <asp:TextBox ID="returndate" TextMode="Date" runat="server" CssClass="inputs" />
                            <asp:Button id="issueBook" runat="server" CssClass="themeBtn ml" Text="Issue" onclick="issueBook_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="formWrapper">
                <p>Renew issue</p>
                <div id="renewIssueForm">
                    <asp:UpdatePanel ID="renewIssuePanel" class="column-form" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="renewMobiles" runat="server" AutoPostBack="True" 
                                CssClass="inputs" onselectedindexchanged="renewMobiles_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="renewBookNames" runat="server" AutoPostBack="True" 
                                CssClass="inputs" 
                                onselectedindexchanged="renewBookNames_SelectedIndexChanged" 
                                Enabled="False" >
                            </asp:DropDownList>
                            <asp:TextBox ID="extendedDate" TextMode="Date" runat="server" Enabled="false" CssClass="inputs" />
                            <asp:Button id="renewBook" runat="server" CssClass="themeBtn ml" Text="Renew" 
                                Enabled="False" onclick="renewBook_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <div runat="server" class="formWrapper w-100">
            <p id="txt" runat="server">Return book</p>
            <asp:UpdatePanel ID="returnBookPanel" runat='server'>
                <ContentTemplate>
                    <div class="findBookBlock">
                        <asp:DropDownList ID="returnMobiles" runat="server" CssClass="inputs" 
                            AutoPostBack="True" onselectedindexchanged="returnMobiles_SelectedIndexChanged"></asp:DropDownList>
                        <asp:DropDownList ID="specificBooks" runat="server" CssClass="inputs" 
                            AutoPostBack="True" Enabled="false" 
                            onselectedindexchanged="specificBooks_SelectedIndexChanged"></asp:DropDownList>
                        <asp:Button id="fetchIssueBook" runat="server" CssClass="themeBtn ml" 
                            Text="Fetch" onclick="fetchIssueBook_Click" Enabled="False"/>
                    </div>
                    <asp:Table id="returnBookTable" runat="server" CssClass="table table-striped table-hover">
                        <asp:TableHeaderRow runat="server">
                            <asp:TableHeaderCell Text="Id" />
                            <asp:TableHeaderCell Text="Book" />
                            <asp:TableHeaderCell Text="Student" />
                            <asp:TableHeaderCell Text="Issuedate" />
                            <asp:TableHeaderCell Text="Returndate" />
                            <asp:TableHeaderCell Text="Fine" />
                            <asp:TableHeaderCell Text="Control" />
                        </asp:TableHeaderRow>
                        <asp:TableRow>
                            <asp:TableCell ID="returnId" />
                            <asp:TableCell ID="returnBook" />
                            <asp:TableCell ID="returnStudent" />
                            <asp:TableCell ID="returnIssueDate" />
                            <asp:TableCell ID="retDate" />
                            <asp:TableCell ID="returnFine" />
                            <asp:TableCell ID="returnControl">
                                <asp:Button id="returnBtn" CssClass="themeBtn" runat="server" Text='Return' Visible="false" onclick="returnBtn_Click" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="tab-pane fade" id="nav-history" role="tabpanel" aria-labelledby="nav-history-tab">
        <asp:UpdatePanel ID="historyPanel" class="tableControlWrap" runat="server">
            <ContentTemplate>
                <div class="panelDataMiddleBlock">
                    <p>History</p>
                    <asp:DropDownList ID="historyFilter" runat="server" CssClass="inputs" 
                        AutoPostBack="True" onselectedindexchanged="historyFilter_SelectedIndexChanged">
                        <asp:ListItem Selected="True">All</asp:ListItem>
                        <asp:ListItem Value="ISSUE">Issue</asp:ListItem>
                        <asp:ListItem Value="RENEW">Renew</asp:ListItem>
                        <asp:ListItem Value="RETURN">Return</asp:ListItem>
                        <asp:ListItem Value="PAYMENT">Transaction</asp:ListItem>
                    </asp:DropDownList>
                </div>    
                <div class="tableWrapper">
                    <asp:GridView ID="allHistory" runat="server" CssClass="table table-striped table-hover"></asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>