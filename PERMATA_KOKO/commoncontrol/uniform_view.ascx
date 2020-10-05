<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="uniform_view.ascx.vb" Inherits="permatapintar.uniform_view" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Paparan Badan Beruniform
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
        <td>Nama Badan Beruniform:
        </td>
        <td>
            <asp:Label ID="lblUniform" runat="server" Text=""></asp:Label>
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
            &nbsp;|&nbsp;<asp:LinkButton ID="lnkList" runat="server">Senarai Badan Beruniform</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td class="column_width"></td>
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
        </td>
    </tr>
</table>
