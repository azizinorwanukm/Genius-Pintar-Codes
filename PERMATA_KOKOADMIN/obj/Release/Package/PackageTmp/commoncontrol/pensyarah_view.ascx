<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pensyarah_view.ascx.vb" Inherits="permatapintar.pensyarah_view" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Maklumat Pensyarah
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Tahun:</td>
        <td>
            <asp:Label ID="lblTahun" runat="server" Text=""></asp:Label>
    </tr>
    <tr>
        <td>Nama Penuh:
        </td>
        <td>
            <asp:Label ID="lblFullname" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>MYKAD#:
        </td>
        <td>
            <asp:Label ID="lblMYKAD" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>No. Telefon:
        </td>
        <td>
            <asp:Label ID="lblContactNo" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Email:
        </td>
        <td>
            <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Alamat:
        </td>
        <td>
            <asp:Label ID="lblAddress1" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;"></td>
        <td>
            <asp:Label ID="lblAddress2" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Poskod:
        </td>
        <td>
            <asp:Label ID="lblPostcode" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Bandar:
        </td>
        <td>
            <asp:Label ID="lblCity" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Negeri:
        </td>
        <td>
            <asp:Label ID="lblState" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr class="fbform_header">
        <td colspan="2">Maklumat Kewangan
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Nama Bank:
        </td>
        <td>
            <asp:Label ID="lblBankName" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Akaun#:
        </td>
        <td>
            <asp:Label ID="lblAcctNo" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="column_width"></td>
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
        </td>
    </tr>
</table>

