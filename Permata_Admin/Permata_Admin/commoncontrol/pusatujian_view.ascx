<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pusatujian_view.ascx.vb"
    Inherits="permatapintar.pusatujian_view" %>
<table class="fbform">
    <tr class="fbform_header">
        <td class="fbtd_left">Pusat Ujian
        </td>
        <td style="text-align: right;" colspan="3">
            <asp:LinkButton ID="lnkUpdate" runat="server">Kemaskini</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Nama Pusat
        </td>
        <td>:<asp:Label ID="lblPusatName" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
        <td class="fbtd_left">Jenis Pusat
        </td>
        <td>:<asp:Label ID="lblPusatType" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Alamat
        </td>
        <td>:<asp:Label ID="lblPusatAddress" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
        <td>PPD
        </td>
        <td>:<asp:Label ID="lblPusatPPD" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Poskod
        </td>
        <td>:<asp:Label ID="lblPusatPostcode" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
        <td>Tel#
        </td>
        <td>:<asp:Label ID="lblPusatNoTel" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Bandar
        </td>
        <td>:<asp:Label ID="lblPusatCity" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
        <td>Fax#
        </td>
        <td>:<asp:Label ID="lblPusatNoFax" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Negeri
        </td>
        <td>:<asp:Label ID="lblPusatState" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
        <td>Email
        </td>
        <td>:<asp:Label ID="lblPusatEmail" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>

    <tr>
        <td>Tahun Ujian
        </td>
        <td>:<asp:Label ID="lblPusatTahun" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
        <td>Jum. Lab
        </td>
        <td>:<asp:Label ID="lblPusatJumlahLab" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Jum.Komputer
        </td>
        <td>:<asp:Label ID="lblPusatJumlahKomp" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
        <td style="vertical-align: top;">
        </td>
        <td style="vertical-align: top;">
        </td>
    </tr>
    <tr>
        <td colspan="4">Keterangan:<br />
            <asp:TextBox ID="txtKomen" runat="server" ReadOnly="true" TextMode="MultiLine" Width="98%" Rows="10"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
</table>
