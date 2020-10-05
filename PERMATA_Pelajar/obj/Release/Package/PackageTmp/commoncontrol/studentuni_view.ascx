<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="studentuni_view.ascx.vb" Inherits="permatapintar.studentuni_view" %>

<table class="fbform">
    <tr class="fbform_header">
        <td class="fbtd_left">Maklumat Universiti/Institusi
        </td>
        <td style="text-align: right;" colspan="3">
            <asp:LinkButton ID="lnkEdit" runat="server" ForeColor="Black">Kemaskini</asp:LinkButton>
        </td>
    </tr>

    <tr>
        <td class="fbtd_left">Nama Universiti/Institusi
        </td>
        <td>:<asp:Label ID="lblUniName" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
        <td class="fbtd_left">Tajaan
        </td>
        <td>:<asp:Label ID="lblUniTajaan" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Negara
        </td>
        <td>:<asp:Label ID="lblUniCountry" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>Bidang Pengajian
        </td>
        <td>:<asp:Label ID="lblUniCourse" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
        <td>Tahap Pengajian
        </td>
        <td>:<asp:Label ID="lblUniLevel" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Tahun Mula
        </td>
        <td>:<asp:Label ID="lblUniStartYear" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
        <td>Tahun Tamat
        </td>
        <td>:<asp:Label ID="lblUniEndYear" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
</table>
