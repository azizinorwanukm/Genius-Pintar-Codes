<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="question.ascx.vb" Inherits="UKM3.question2" %>

<div align="center"><h3>UJIAN STEM</h3></div>

<br /><br />

<table width="100%">
    <tr>
        <td width="200">Candidate Name:</td>
        <td><asp:Label ID="lblName" Text="" runat="server" /></td>
        <td><div class="logout">
    <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="cancelbtn" />
            </div>
        </td>
    </tr>
    <tr>
        <td>NRIC:</td>
        <td><asp:Label ID="lblMykad" Text="" runat="server" /></td>
    </tr>
</table>

<br />
    
Question <asp:Label ID="questNo" Text="" runat="server" />
<p>&nbsp;</p>

 <asp:Label ID="lblQuest" Text="" runat="server" />
<br />
<div class="imgcontainer" oncontextmenu="return false">
    <asp:Image ID="imgControl" runat="server" />
</div>

<p>Choose your answer</p>

<asp:RadioButtonList ID="radioAnswers" runat="server" EnableViewState="False" RepeatDirection="Vertical" RepeatLayout="Flow" AutoPostBack="true">
    <asp:ListItem Value="1" Text="A"></asp:ListItem>
    <asp:ListItem Value="2" Text="B"></asp:ListItem>
    <asp:ListItem Value="3" Text="C"></asp:ListItem>
    <asp:ListItem Value="4" Text="D"></asp:ListItem>
</asp:RadioButtonList>

<p>&nbsp;</p>

<asp:Label ID="lblStudentAnswer" Text="" runat="server" />

<p>&nbsp;</p>
<asp:Button ID="btnDisable" runat="server" Text="Please select your answer first" Enabled="false" CssClass="disbtn" />
<asp:Button ID="btnConfirm" runat="server" Text="Confirm and to next question" Visible="false" Enabled="false" CssClass="logbtn" />

<style>
body {font-family: Arial, Helvetica, sans-serif;
      font-size: medium;
}

.logbtn {
    background-color: #4CAF50;
    color: white;
    padding: 14px 20px;
    margin: 8px 0;
    border: none;
    cursor: pointer;
    width: 400px;
}

.disbtn {
    background-color: #808080;
    color: white;
    padding: 14px 20px;
    margin: 8px 0;
    border: none;    
    width: 400px;
}

.logout{
    text-align: right;
}

.cancelbtn {
    width: auto;
    padding: 10px 18px;
    cursor: pointer;
    background-color: #f44336;
}

.imgcontainer {
    text-align: center;
    border-style: solid;
}

img.avatar {
    width: 40%;
    border-radius: 50%;
}

/* Change styles for span and cancel button on extra small screens */
@media screen and (max-width: 300px) {
    span.psw {
       display: block;
       float: none;
    }
    .cancelbtn {
       width: 100%;
    }
}

</style>