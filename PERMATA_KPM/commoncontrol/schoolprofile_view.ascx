<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="schoolprofile_view.ascx.vb"
    Inherits="permatapintar.schoolprofile_view" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="3">Paparan Maklumat Sekolah
        </td>
        <td style="text-align: right;">
            <asp:LinkButton ID="lnkEdit" runat="server">Kemaskini</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Kod Sekolah
        </td>
        <td>:<asp:Label ID="lblSchoolCode" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
        <td class="fbtd_left">Nama Sekolah
        </td>
        <td>:<asp:Label ID="lblSchoolName" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Alamat Sekolah
        </td>
        <td>:<asp:Label ID="lblSchoolAddress" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
        <td>&nbsp;
        </td>
        <td>&nbsp;
        </td>
    </tr>
    <tr>
        <td>Poskod
        </td>
        <td>:<asp:Label ID="lblSchoolPostcode" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
        <td>Bandar
        </td>
        <td>:<asp:Label ID="lblSchoolCity" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Negeri
        </td>
        <td>:<asp:Label ID="lblSchoolState" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
        <td>Jenis Sekolah
        </td>
        <td>:<asp:Label ID="lblSchoolType" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>PPD
        </td>
        <td>:<asp:Label ID="lblSchoolPPD" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
        <td>Negara
        </td>
        <td>:<asp:Label ID="lblSchoolCountry" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>

    </tr>
    <tr>
        <td>Tel#
        </td>
        <td>:<asp:Label ID="lblSchoolNoTel" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
        <td>Fax#
        </td>
        <td>:<asp:Label ID="lblSchoolNoFax" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Lokasi
        </td>
        <td>:<asp:Label ID="lblSchoolLokasi" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
        <td>Email
        </td>
        <td>:<asp:Label ID="lblSchoolEmail" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td></td>
        <td></td>
        <td>IsDeleted
        </td>
        <td>:<asp:Label ID="lblIsDeleted" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
</table>
