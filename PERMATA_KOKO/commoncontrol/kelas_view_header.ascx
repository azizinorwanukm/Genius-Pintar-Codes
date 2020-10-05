<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="kelas_view_header.ascx.vb" Inherits="permatapintar.kelas_view_header" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Paparan Kelas
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
        <td>Nama Kelas:
        </td>
        <td>
            <asp:Label ID="lblKelas" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>
