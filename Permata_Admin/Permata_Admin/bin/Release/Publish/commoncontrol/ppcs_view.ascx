<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ppcs_view.ascx.vb"
    Inherits="permatapintar.ppcs_view" %>
<table class="fbform">
    <tr class="fbform_header">
        <td>Maklumat PPCS
        </td>
        <td style="text-align: right;">
            <asp:LinkButton ID="lnkHarian" runat="server">Laporan Harian</asp:LinkButton>&nbsp;|&nbsp;
            <asp:LinkButton ID="lnkMingguan" runat="server">Laporan Mingguan</asp:LinkButton>&nbsp;|&nbsp;
            <asp:LinkButton ID="lnkAkhir" runat="server">Laporan Akhir</asp:LinkButton>
            <asp:LinkButton ID="lnkEdit" runat="server" ForeColor="Black" Visible="true">Kemaskini</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Sessi PPCS: 
        </td>
        <td>:<asp:Label ID="lblPPCSDate" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Kursus
        </td>
        <td>:<asp:Label ID="lblPPCSCourse" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Kelas
        </td>
        <td>:<asp:Label ID="lblPPCSClass" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Tempat
        </td>
        <td>:<asp:Label ID="lblTempat" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>

    <tr>
        <td>Status
        </td>
        <td>:<asp:Label ID="lblPPCSStatus" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>

    <tr>
        <td>Nama Asrama
        </td>
        <td>:<asp:Label ID="lblNamaAsrama" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>No Bilik
        </td>
        <td>:<asp:Label ID="lblNoBilik" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Saiz Baju
        </td>
        <td>:<asp:Label ID="lblSaizBaju" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Sakit/Alahan
        </td>
        <td>:<asp:Label ID="lblSakitAlahan" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr class="fbform_msg">
        <td colspan="2">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
    </tr>
</table>
<asp:Label ID="lblPPCSID" runat="server" Text="" CssClass="fblabel_view"></asp:Label>|
<asp:Label ID="lblCourseID" runat="server" Text="" CssClass="fblabel_view"></asp:Label>|
<asp:Label ID="lblClassID" runat="server" Text="" CssClass="fblabel_view"></asp:Label>|
