<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="parentprofile_view.ascx.vb"
    Inherits="permatapintar.parentprofile_view" %>
<table class="fbform">
    <tr class="fbform_header">
        <td>
            Maklumat Bapa/Penjaga
        </td>
        <td style="text-align: right;">
            <asp:LinkButton ID="lnkEdit" runat="server" ForeColor="Black">Kemaskini</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            MYKAD Bapa/Penjaga
        </td>
        <td>
            :<asp:Label ID="lblFatherMYKADNo" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            Nama Bapa/Penjaga
        </td>
        <td>
            :<asp:Label ID="lblFatherFullname" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 15%;">
            Pekerjaan Bapa/Penjaga
        </td>
        <td>
            :<asp:Label ID="lblFatherJob" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr class="fbform_header">
        <td colspan="2">
            Maklumat Ibu
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            MYKAD Ibu
        </td>
        <td>
            :<asp:Label ID="lblMotherMYKADNo" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            Nama Ibu
        </td>
        <td>
            :<asp:Label ID="lblMotherFullname" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 15%;">
            Pekerjaan Ibu
        </td>
        <td>
            :<asp:Label ID="lblMotherJob" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr class="fbform_header">
        <td colspan="2">
            Maklumat Keluarga
        </td>
    </tr>
    <tr>
        <td>
            Nombor Talipon
        </td>
        <td>
            :<asp:Label ID="lblFamilyContactNo" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
</table>
