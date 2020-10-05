<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="instruktor_login.ascx.vb" Inherits="permatapintar.instruktor_login" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Log Masuk
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">
            <asp:Label ID="Label4" runat="server" Text="Tahun:"></asp:Label>
        </td>
        <td>:<asp:Label ID="lblTahun" runat="server" Text=""></asp:Label>

        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Login ID"></asp:Label>
        </td>
        <td>:<asp:TextBox ID="txtLoginID" runat="server" Text="" Width="200px" MaxLength="50"></asp:TextBox>&nbsp;*<br />
            <asp:Label ID="lbl15_instruction" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Kata Laluan"></asp:Label>
        </td>
        <td>:<asp:TextBox ID="txtPwd" runat="server" TextMode="Password" Width="200px" MaxLength="15"></asp:TextBox>&nbsp;*<br />
            <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td>
            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="fbbutton" />
            &nbsp;|&nbsp;<asp:LinkButton ID="lnkForgot" runat="server">Lupa Kata Laluan</asp:LinkButton>
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Medan bertanda * adalah wajib diisi."></asp:Label>
</div>
<asp:Label ID="lblDebug" runat="server" Text="" ForeColor="red"></asp:Label>

