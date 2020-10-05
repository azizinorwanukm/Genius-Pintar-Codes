<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="default.aspx.vb" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>Login Form</title>
    <link rel="stylesheet" href="css/style.css" >
    <link rel="stylesheet" href="css/side_bar_frame.css">
</head>
<body>
    <form name="myForm" id="myForm" runat="server">
        <section class="container">
            <div class="login">
                <img src="img/ukm.jpg">
                <h1>Login</h1>
                <asp:TextBox ID="TxtLoginID" runat="server" placeholder="Username or Email" required="required"></asp:TextBox>
                <asp:TextBox ID="TxtPassword" TextMode="Password" runat="server" placeholder="Password"></asp:TextBox>

                <div class="w3-container" style="text-align: center">
                    <br />
                    <asp:Button ID="BtnLogin" runat="server" Text="Login" class="w3-button w3-light-blue" />
                </div>
            </div>

            <div class="login-help">
                <p>Forgot your password? <a href="index.html">Click here to reset it</a></p>
            </div>
        </section>
    </form>
</body>

</html>
