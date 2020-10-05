<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="sukan_view.ascx.vb" Inherits="permatapintar.sukan_view" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Paparan Sukan
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
        <td>Nama Sukan & Permainan:
        </td>
        <td>
            <asp:Label ID="lblSukan" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Hari:
        </td>
        <td>
            <asp:Label ID="lblHari" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Masa:
        </td>
        <td>
            <asp:Label ID="lblMasa" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Tempat:
        </td>
        <td>
            <asp:Label ID="lblTempat" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td></td>
        <td class="fbform_sap">&nbsp;</td>
    </tr>
    <tr>
        <td class="column_width">&nbsp;
        </td>
        <td>
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini " CssClass="fbbutton" />
            &nbsp;
            <asp:Button ID="btnDelete" runat="server" Text="Hapuskan " CssClass="fbbutton" />
            &nbsp;|&nbsp;<asp:LinkButton ID="lnkList" runat="server">Senarai Sukan & Permainan</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td class="column_width"></td>
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
        </td>
    </tr>
</table>
