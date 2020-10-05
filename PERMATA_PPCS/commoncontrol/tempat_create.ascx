<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="tempat_create.ascx.vb"
    Inherits="permatapintar.tempat_create1" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Tambah Tempat Baru
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
        <td>&nbsp;
        </td>
        <td class="fbaside_sap">
            <asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red" Style="text-align: left"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Button ID="btnadd" runat="server" Text=" Tambah " CssClass="fbbutton" />
            &nbsp; &nbsp;
        </td>
    </tr>
</table>
