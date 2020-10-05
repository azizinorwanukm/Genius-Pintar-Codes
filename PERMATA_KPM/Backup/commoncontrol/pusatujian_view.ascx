<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pusatujian_view.ascx.vb"
    Inherits="permatapintar.pusatujian_view" %>
<table class="fbform">
    <tr class="fbform_header">
        <td>
            Maklumat Pusat Ujian
        </td>
        <td style="text-align: right;">
            <asp:LinkButton ID="lnkEdit" runat="server">Kemaskini</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td style="width: 15%;">
            Nama Pusat
        </td>
        <td>
            :<asp:Label ID="lblPusatName" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Alamat
        </td>
        <td>
            :<asp:Label ID="lblPusatAddress" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Poskod
        </td>
        <td>
            :<asp:Label ID="lblPusatPostcode" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Bandar
        </td>
        <td>
            :<asp:Label ID="lblPusatCity" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Negeri
        </td>
        <td>
            :<asp:Label ID="lblPusatState" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Jenis Pusat
        </td>
        <td>
            :<asp:Label ID="lblPusatType" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            PPD
        </td>
        <td>
            :<asp:Label ID="lblPusatPPD" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Tel#
        </td>
        <td>
            :<asp:Label ID="lblPusatNoTel" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Fax#
        </td>
        <td>
            :<asp:Label ID="lblPusatNoFax" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Email
        </td>
        <td>
            :<asp:Label ID="lblPusatEmail" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Tahun
        </td>
        <td>
            :<asp:Label ID="lblPusatTahun" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Jum. Lab
        </td>
        <td>
            :<asp:Label ID="lblPusatJumlahLab" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Jum.Komputer
        </td>
        <td>
            :<asp:Label ID="lblPusatJumlahKomp" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">
            Remarks
        </td>
        <td>
            :<asp:Label ID="lblKomen" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>
