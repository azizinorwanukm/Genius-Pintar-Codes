<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="login.ascx.vb" Inherits="UKM3.login1" %>

<div class="imgcontainer">
    <a href="home.aspx"><img src="imgs/ppintar.png" ></a>
    <br />
    <h1>Ujian STEM</h1>
</div>

<div class="container">
    <label><b>MYKAD/MYKID</b></label>
    <asp:TextBox ID="txtLoginID" runat="server" autocomplete="off" ></asp:TextBox>

    <label><b>Kata Laluan</b></label>
    <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" ></asp:TextBox>
    <asp:Button ID="btnLogin" runat="server" Text="Log Masuk" CssClass="logbtn" />
    <br />
    <a href="home.aspx" > Kembali ke menu utama </a>
    <br />
    <h3><asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></h3>
</div>

<style>
body {font-family: Arial, Helvetica, sans-serif;}

input[type=text], input[type=password] {
    width: 100%;
    padding: 12px 20px;
    margin: 8px 0;
    display: inline-block;
    border: 1px solid #ccc;
    box-sizing: border-box;
}

h3{
    color: red;
}

.logbtn {
    background-color: #4CAF50;
    color: white;
    padding: 14px 20px;
    margin: 8px 0;
    border: none;
    cursor: pointer;
    width: 100%;
}

button:hover {
    opacity: 0.8;
}

.imgcontainer {
    text-align: center;
    margin: 24px 0 12px 0;
}

img.avatar {
    width: 40%;
    border-radius: 50%;
}

.container {
    padding: 16px;
    text-align: center;
    margin: 0 auto;
    width: 380px;
}

span.psw {
    float: right;
    padding-top: 16px;
}
</style>