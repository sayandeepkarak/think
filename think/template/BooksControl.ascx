<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BooksControl.ascx.cs" Inherits="think.template.BooksControl" %>

<div class="panelsTabsArea">
    <div class="nav nav-tabs panelTabsWrapper border-0" id="nav-tab" role="tablist">
        <button class="nav-link active" id="nav-allbooks-tab" data-bs-toggle="tab" data-bs-target="#nav-allbooks" type="button" role="tab" aria-controls="nav-allbooks" aria-selected="true">All</button>
        <button class="nav-link" id="nav-update-tab" data-bs-toggle="tab" data-bs-target="#nav-newBook" type="button" role="tab" aria-controls="nav-newBook" aria-selected="false">Update</button>
    </div>
</div>

<div class="tab-content" id="nav-tabContent">
    <div class="tab-pane fade show active" id="nav-allbooks" role="tabpanel" aria-labelledby="nav-allbooks-tab">

        <div class="formWrapper">
            <p id="text" runat="server">New Book</p>
            <div id="addBookForm">
                <input type="text" name="bookName" id="bookName" class="inputs" placeholder="Book name" />
                <input type="text" name="author" id="author" class="inputs" placeholder="Author name" />
                <input type="number" name="price" id="price" class="inputs" placeholder="Price" />
                <input type="number" name="quantity" id="quantity" class="inputs" placeholder="Quantity" autocomplete="false" />
                <asp:Button id="addBook" runat="server" CssClass="themeBtn" Text="Add" 
                    onclick="addBook_Click" />
            </div>
        </div>

        <div class="panelDataMiddleBlock">
            <p>All Books</p>
            <select class="inputs" name="searchId" id="searchId">
                <option value="" disabled selected>Select an Id</option>
                <option value="1">1</option>
            </select>
        </div>
        
        <div class="tableWrapper">
            <table class="table table-striped table-hover">
                <thead>
                    <tr>
                        <th>ISBN</th>
                        <th>Bookname</th>
                        <th>Author</th>
                        <th>Price</th>
                        <th>Quantity</th>
                    </tr>
                </thead>
                <tbody id="booksRow" runat="server">
                    <tr>
                        <th>1</th>
                        <td>Java</td>
                        <td>Me</td>
                        <td>150</td>
                        <td>5</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <div class="tab-pane fade" id="nav-newBook" role="tabpanel" aria-labelledby="nav-newBook-tab">
        
    </div>
</div>
