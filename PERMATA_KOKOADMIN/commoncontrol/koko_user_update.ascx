<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="koko_user_update.ascx.vb" Inherits="permatapintar.koko_user_update" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Kemaskini Profil
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Login ID:
        </td>
        <td>
            <asp:TextBox ID="txtLoginID" runat="server" Width="300px" MaxLength="50"></asp:TextBox>*&nbsp;
        </td>
    </tr>
    <tr>
        <td>Kata Laluan:
        </td>
        <td>
            <asp:TextBox ID="txtPwd" runat="server" Width="300px" MaxLength="15"></asp:TextBox>*&nbsp;
        </td>
    </tr>
    <tr>
        <td>Nama Penuh:
        </td>
        <td>
            <asp:TextBox ID="txtFullname" runat="server" Width="300px" MaxLength="50"></asp:TextBox>&nbsp;
        </td>
    </tr>

    <tr>
        <td>&nbsp;</td>
        <td class="fbform_sap">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="column_width">&nbsp;
        </td>
        <td>
            <asp:Button ID="btnUpdate" runat="server" Text=" Kemaskini " CssClass="fbbutton" />
        </td>
    </tr>
    
</table>
<asp:Label ID="lblkokouserid" runat="server" Text="" ForeColor="red" Visible="false"></asp:Label>
<asp:Label ID="lblLoginIDOrg" runat="server" Text="" ForeColor="red" Visible="false"></asp:Label>

