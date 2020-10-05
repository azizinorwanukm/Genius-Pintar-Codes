<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="kelaskoko_view_header.ascx.vb" Inherits="permatapintar.kelaskoko_view_header" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Paparan Kumpulan
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Sukan & Permainan:
        </td>
        <td>
            <asp:Label ID="lblKOKOName" runat="server" Text=''></asp:Label>&nbsp;<asp:Label ID="lblTahun" runat="server" Text=''></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Nama Kumpulan:
        </td>
        <td>
            <asp:Label ID="lblKelas" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="column_width"></td>
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
        </td>
    </tr>
</table>
<asp:Label ID="lblKokoID" runat="server" Text="" ForeColor="red" Visible="false"></asp:Label>
