<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="instruktor_view.ascx.vb" Inherits="permatapintar.instruktor_view" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Maklumat Instruktor
        </td>
    </tr>

    <tr>
        <td class="fbtd_left">Nama Penuh:
        </td>
        <td>
            <asp:Label ID="lblFullname" runat="server" Text=""></asp:Label>
        </td>
        <td>&nbsp;</td>
        <td></td>
    </tr>
    <tr>
        <td>MYKAD#:
        </td>
        <td>
            <asp:Label ID="lblMYKAD" runat="server" Text=""></asp:Label>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>No. Telefon:
        </td>
        <td>
            <asp:Label ID="lblContactNo" runat="server" Text=""></asp:Label>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>Email:
        </td>
        <td>
            <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Alamat:
        </td>
        <td>
            <asp:Label ID="lblAddress1" runat="server" Text=""></asp:Label>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td style="vertical-align: top;"></td>
        <td>
            <asp:Label ID="lblAddress2" runat="server" Text=""></asp:Label>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Poskod:
        </td>
        <td>
            <asp:Label ID="lblPostcode" runat="server" Text=""></asp:Label>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Bandar:
        </td>
        <td>
            <asp:Label ID="lblCity" runat="server" Text=""></asp:Label>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Negeri:
        </td>
        <td>
            <asp:Label ID="lblState" runat="server" Text=""></asp:Label>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr class="fbform_header">
        <td colspan="4">Maklumat Login
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Login ID:
        </td>
        <td>
            <asp:Label ID="lblLoginID" runat="server" Text=""></asp:Label>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Kata Laluan:
        </td>
        <td>
            <asp:Label ID="lblPwd" runat="server" Text=""></asp:Label>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr class="fbform_header">
        <td colspan="4">Maklumat Kewangan
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Nama Bank:
        </td>
        <td>
            <asp:Label ID="lblBankName" runat="server" Text=""></asp:Label>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Akaun#:
        </td>
        <td>
            <asp:Label ID="lblAcctNo" runat="server" Text=""></asp:Label>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr class="fbform_header">
        <td colspan="4">Maklumat Kokurikulum
        </td>
    </tr>
    <tr>
        <td>Tahun:</td>
        <td>
            <asp:Label ID="lblTahun" runat="server" Text=""></asp:Label>

        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>Kelas:</td>
        <td>
            <asp:Label ID="lblKelas" runat="server" Text=""></asp:Label></td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>Badan Beruniform:
        </td>
        <td>
            <asp:LinkButton ID="lnkUniform" runat="server"></asp:LinkButton>&nbsp;
        </td>
        <td>Ketua Badan Uniform:</td>
        <td>
            <asp:Label ID="lblKetuaUniform" runat="server" Text="" Font-Bold="false"></asp:Label></td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Kelab & Persatuan:
        </td>
        <td>
            <asp:LinkButton ID="lnkPersatuan" runat="server"></asp:LinkButton>&nbsp;
        </td>
        <td>Ketua Kelab & Persatuan:</td>
        <td>
            <asp:Label ID="lblKetuaPersatuan" runat="server" Text="" Font-Bold="false"></asp:Label></td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Sukan & Permainan:
        </td>
        <td>
            <asp:LinkButton ID="lnkSukan" runat="server"></asp:LinkButton>&nbsp;
        </td>
        <td>Ketua Sukan & Permainan:</td>
        <td>
            <asp:Label ID="lblKetuaSukan" runat="server" Text="" Font-Bold="false"></asp:Label></td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Rumah Sukan:
        </td>
        <td>
            <asp:LinkButton ID="lnkRumahsukan" runat="server" Enabled="false"></asp:LinkButton>&nbsp;
        </td>
        <td>Ketua Rumah Sukan:</td>
        <td>
            <asp:Label ID="lblKetuaRumahsukan" runat="server" Text="" Font-Bold="false"></asp:Label></td>
    </tr>
    <tr>
        <td class="column_width"></td>
        <td colspan="3">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
        </td>
    </tr>
</table>

<asp:Label ID="lblDebug" runat="server" Text=""></asp:Label>