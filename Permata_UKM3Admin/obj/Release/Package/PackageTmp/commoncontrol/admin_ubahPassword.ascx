<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin_ubahPassword.ascx.vb" Inherits="permatapintar.admin_ubahPassword" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Kemaskini Maklumat Pengguna Sistem
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Nama Pengguna
        </td>
        <td>:<asp:TextBox ID="txtFullname" runat="server" Width="250px" MaxLength="250" Text=""></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td>LoginID
        </td>
        <td>:<asp:TextBox ID="txtLoginID" runat="server" Width="250px" MaxLength="50"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>Password
        </td>
        <td>:<asp:TextBox ID="txtPwd" runat="server" Width="250px" MaxLength="50"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="2">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="fbbutton" />
        </td>
    </tr>
</table>
<asp:Label ID="lblLoginID" runat="server" Text="" Visible="false"></asp:Label>