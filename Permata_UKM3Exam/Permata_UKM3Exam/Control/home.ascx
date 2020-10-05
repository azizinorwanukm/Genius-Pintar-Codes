<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="home.ascx.vb" Inherits="UKM3.home1" %>

<div class="imgcontainer">
    <img src="imgs/ppintar.png" ><br />
    <h1>Ujian UKM 3</h1>
</div>

<div class="container">
    <asp:Button ID="btnSTEM" runat="server" Text="Ujian STEM" CssClass="logbtn" />
    <br />
    <asp:Button ID="btnEQ" runat="server" Text="Ujian Kepintaran Emosi" CssClass="logbtn" />
    <br /><br />
    <asp:Button ID="btnKPP" runat="server" Text="Penilaian Instruktor KPP" CssClass="logbtn" />
    <br />
    <asp:Button ID="btnPPCS" runat="server" Text="Penilaian Instruktor PPCS" CssClass="logbtn" />
    <br />
    <asp:Button ID="btnRAPPCS" runat="server" Text="Penilaian RA PPCS" CssClass="logbtn" />
    <br />
    <asp:Button ID="btnTAPPCS" runat="server" Text="Penilaian TA PPCS" CssClass="logbtn" />
    <br />
    
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