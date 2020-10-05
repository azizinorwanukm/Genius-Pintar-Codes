<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="takwim_view.ascx.vb" Inherits="permatapintar.takwim_view" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Paparan Takwim
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
        <td >Kategori:
        </td>
        <td>
            <asp:Label ID="lblKategori" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td >Tarikh:
        </td>
        <td>
            <asp:Label ID="lblTarikh" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td >Masa:
        </td>
        <td>
            <asp:Label ID="lblMasa" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td >Tempat:
        </td>
        <td>
            <asp:Label ID="lblTempat" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td >Tajuk:
        </td>
        <td>
            <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width:20%; vertical-align:top;">Catatan:
        </td>
        <td>
            <asp:Literal ID="ltCatatan" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td class="column_width"></td>
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
        </td>
    </tr>
</table>

