<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="instruktor_update_pwd.ascx.vb" Inherits="permatapintar.instruktor_update_pwd" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Tukar Kata Laluan
        </td>
    </tr>

    <tr>
        <td class="fbtd_left">Login ID:
        </td>
        <td>
            <asp:Label ID="lblLoginID" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Kata Laluan Asal:
        </td>
        <td>
            <asp:TextBox ID="txtPwd" runat="server" Width="250px" MaxLength="254" TextMode="Password"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td>Kata Laluan Baru:
        </td>
        <td>
            <asp:TextBox ID="txtPwdNew" runat="server" Width="250px" MaxLength="254" TextMode="Password"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td>Pastikan Kata Laluan Baru:
        </td>
        <td>
            <asp:TextBox ID="txtPwdVerify" runat="server" Width="250px" MaxLength="254" TextMode="Password"></asp:TextBox>&nbsp;
        </td>
    </tr>

    <tr>
        <td class="column_width">&nbsp;
        </td>
        <td>
            <asp:Button ID="btnUpdate" runat="server" Text=" Kemaskini " CssClass="fbbutton" />
        </td>
    </tr>
    <tr>
        <td class="column_width"></td>
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
        </td>
    </tr>
</table>
<asp:Label ID="lblPwdOrg" runat="server" Text="" ForeColor="red" Visible="false"></asp:Label>

