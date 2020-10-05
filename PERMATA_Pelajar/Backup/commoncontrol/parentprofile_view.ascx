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
    <tr>
        <td>
            Tahap Pendidikan Bapa/Penjaga
        </td>
        <td>
            :<asp:Label ID="lblFatherEducation" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
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
    <tr>
        <td>
            Tahap Pendidikan Ibu
        </td>
        <td>
            :<asp:Label ID="lblMotherEducation" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr class="fbform_header">
        <td colspan="2">
            Maklumat Keluarga
        </td>
    </tr>
    <%--<tr>
        <td>
            Pendapatan Sekeluarga
        </td>
        <td>
            :<asp:Label ID="lblFamilyIncome" runat="server" Text="" CssClass="fblabel_view" Visible="false"></asp:Label>
        </td>
    </tr>--%>
    <tr>
        <td>
            Nombor Talipon Bapa
        </td>
        <td>
            :<asp:Label ID="lblFamilyContactNo" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Nombor Talipon Ibu
        </td>
        <td>
            :<asp:Label ID="lblFamilyContactNoIbu" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
</table>
