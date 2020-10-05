<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="instruktor_view_header.ascx.vb" Inherits="permatapintar.instruktor_view_header" %>

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
        <td>MYKAD#:
        </td>
        <td>
            <asp:Label ID="lblMYKAD" runat="server" Text=""></asp:Label>
        </td>
    </tr>


    <tr class="fbform_header">
        <td colspan="4">Maklumat Kokurikulum
        </td>
    </tr>
    <tr>
        <td>Tahun:</td>
        <td>
            <asp:Label ID="lblTahun" runat="server" Text=""></asp:Label>&nbsp;<asp:Label ID="lblPeperiksaan" runat="server" Text=""></asp:Label></td>
        <td>Kelas:</td>
        <td>
            <asp:Label ID="lblKelas" runat="server" Text=""></asp:Label>
    </tr>
    <tr>
        <td style="vertical-align: top;">Badan Beruniform:
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
        <td><asp:Label ID="lblKetuaPersatuan" runat="server" Text="" Font-Bold="false"></asp:Label></td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Sukan & Permainan:
        </td>
        <td>
            <asp:LinkButton ID="lnkSukan" runat="server"></asp:LinkButton>&nbsp;
        </td>
        <td>Ketua Sukan & Permainan:</td>
        <td><asp:Label ID="lblKetuaSukan" runat="server" Text="" Font-Bold="false"></asp:Label></td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Rumah Sukan:
        </td>
        <td>
            <asp:LinkButton ID="lnkRumahsukan" runat="server" Enabled="false"></asp:LinkButton>&nbsp;
        </td>
        <td>Ketua Rumah Sukan:</td>
        <td><asp:Label ID="lblKetuaRumahsukan" runat="server" Text="" Font-Bold="false"></asp:Label></td>
    </tr>
    <tr>
        <td></td>
        <td colspan="3">
            <asp:Label ID="lblMsg" runat="server" CssClass="fblabel_msg" Text="Klik Badan Beruniform, Kelab & Persatuan atau Sukan & Permainan yang ingin dimasukkan markah. Hanya KETUA Instruktor diberi kebenaran. Masukkan Markah Pencapaian Penglibatan Jawatan dan Kehadiran per 100%"></asp:Label></td>
    </tr>
    </table>
<asp:Label ID="lblUniformID" runat="server" Text="" Visible="false"></asp:Label>&nbsp;
<asp:Label ID="lblPersatuanID" runat="server" Text="" Visible="false"></asp:Label>&nbsp;
<asp:Label ID="lblSukanID" runat="server" Text="" Visible="false"></asp:Label>&nbsp;
<asp:Label ID="lblRumahsukanID" runat="server" Text="" Visible="false"></asp:Label>&nbsp;



