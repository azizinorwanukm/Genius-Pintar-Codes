<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pelajar_login.ascx.vb" Inherits="permatapintar.pelajar_login1" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Log Masuk
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">
            <asp:Label ID="Label4" runat="server" Text="Tahun:"></asp:Label>
        </td>
        <td>
            :<asp:Label ID="lblTahun" runat="server" Text=""></asp:Label>
            
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="MYKAD#"></asp:Label>
        </td>
        <td>:<asp:TextBox ID="txtMYKAD" runat="server" Text="" Width="200px" MaxLength="50"></asp:TextBox>&nbsp;*<br />
            <asp:Label ID="lbl15_instruction" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td>
            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Medan bertanda * adalah wajib diisi."></asp:Label>
</div>

