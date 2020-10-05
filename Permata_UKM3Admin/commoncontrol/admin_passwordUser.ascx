<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin_passwordUser.ascx.vb" Inherits="permatapintar.admin_passwordUser" %>

<table class="fbform">
    <tr>
        <td>Login ID </td>
    </tr>
    <tr>
        <td>Login ID</td>
        <td>:</td>
        <td>
            <asp:Label runat="server" ID="lblloginID" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Password</td>
        <td>:</td>
        <td>
            <asp:Label runat ="server" ID ="lbluserPassword" Text=""></asp:Label>
        </td>
    </tr>

    <tr>
        <td>
            <asp:Button runat ="server" ID ="btnBack" Text="Kembali" />
        </td>
    </tr>


</table>