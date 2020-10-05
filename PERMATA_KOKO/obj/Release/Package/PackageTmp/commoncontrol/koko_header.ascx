<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="koko_header.ascx.vb" Inherits="permatapintar.koko_header" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Paparan Kokurikulum
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Tahun:
        </td>
        <td>
            <asp:Label ID="lblTahun" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Nama:
        </td>
        <td>
            <asp:Label ID="lblNama" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>