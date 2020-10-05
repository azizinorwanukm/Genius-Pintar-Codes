<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="tempat_update.ascx.vb"
    Inherits="permatapintar.tempat_update1" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Kemaskini atau Hapus Tempat
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">
            Nama Tempat:
        </td>
        <td>
            <asp:TextBox ID="txtTempat" runat="server" Width="300px" MaxLength="250"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />
            &nbsp;
            <asp:Button ID="btnDelete" runat="server" Text="Hapus " CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
    <tr>
        <td class="column_width">
        </td>
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
        </td>
    </tr>
</table>
