<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="user_login.ascx.vb"
    Inherits="permatapintar.user_login" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Login Screen
        </td>
    </tr>
    <tr>
        <td style="width: 80px;">
            <asp:Label ID="Label1" runat="server" Text="Login ID"></asp:Label>
        </td>
        <td>
            :<asp:TextBox ID="txtLoginID" runat="server" Text="" Width="200px" MaxLength="50"></asp:TextBox>&nbsp;*<br />
            <asp:Label ID="lbl15_instruction" runat="server" Text="*Masukkan MYKAD/MYKID/Surat Beranak yang digunakan semasa mengambil Ujian UKM1."></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Kata Laluan"></asp:Label>
        </td>
        <td>
            :<asp:TextBox ID="txtPwd" runat="server" TextMode="Password" Width="200px" MaxLength="50"></asp:TextBox>&nbsp;<br />
            <asp:Label ID="Label3" runat="server" Text="*Biarkan kosong bagi pengguna kali pertama dan tidak pernah menukar kata laluan."></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnRegister" runat="server" Text="Pendaftaran Baru" CssClass="fbbutton" />
        </td>
    </tr>
</table>
<div class="warning">
    <asp:Label ID="lblMsg" runat="server" Text="Medan bertanda * adalah wajib diisi." ></asp:Label></div>
