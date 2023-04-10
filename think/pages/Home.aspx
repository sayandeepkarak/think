<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="think.pages.Home" %>

  <!DOCTYPE html
    PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

  <html xmlns="http://www.w3.org/1999/xhtml">

  <head id="Head1" runat="server">
    <title>Home</title>
    <link href="/css/global.css" rel="stylesheet" type="text/css" />
    <link href="/css/home.css" rel="stylesheet" type="text/css" />
  </head>

  <body>
    <header>
      <p class="logo">think</p>
      <button id="openLogin" class="themeBtn">Login</button>
    </header>

    <section id="homeSection">
      <div id="homeLeft" class="homeChilds">
        <p class="homeBigText">We are good just ask</p>
        <p class="homeBigText">our moms</p>
        <p class="homeSmallText">There is no one friend as loyal as book</p>
        <button id="openSignup" class="themeBtn">
          Create a student account
        </button>
      </div>
      <div id="homeRight" class="homeChilds">
        <img src="/assets/home.png" alt="Home" id="homeImage" />

        <form id="login" class="forms">
          <p>Login</p>
          <input type="email" name="loginEmail" id="loginEmail" class="inputs" placeholder="Email" />
          <input type="password" name="loginPassword" id="loginPassword" class="inputs" placeholder="Password"
            autocomplete="false" />
          <button type="submit" class="themeBtn">Login</button>
        </form>

        <form id="signup" class="forms">
          <p>Signup</p>
          <input type="text" name="fullname" id="fullname" class="inputs" placeholder="Name" />
          <input type="email" name="email" id="email" class="inputs" placeholder="Email" />
          <input type="text" name="mobile" id="mobile" class="inputs" placeholder="Mobile" />
          <input type="password" name="password" id="password" class="inputs" placeholder="Password"
            autocomplete="false" />
          <input type="password" name="confirmPass" id="confirmPass" class="inputs" placeholder="Confirm Password"
            autocomplete="false" />
          <button type="submit" class="themeBtn">Register</button>
        </form>
      </div>
    </section>
    <script src="/js/Home.js" type="text/javascript"></script>
  </body>

  </html>