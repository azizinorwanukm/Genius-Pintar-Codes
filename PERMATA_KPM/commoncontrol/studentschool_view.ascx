<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="studentschool_view.ascx.vb" Inherits="permatapintar.studentschool_view" %>
<table class="fbform">
    <tr class="fbform_header">
        <td>
            Maklumat Sekolah
        </td>
        <td style="text-align: right;">
            <asp:LinkButton ID="lnkEdit" runat="server">Kemaskini</asp:LinkButton>
        </td>
    </tr>
    
    <tr>
        <td style="width: 20%;">
            Nama Sekolah
        </td>
        <td>
            :<asp:Label ID="lblSchoolName" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Kod Sekolah
        </td>
        <td>
            :<asp:Label ID="lblSchoolCode" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Alamat Sekolah
        </td>
        <td>
            :<asp:Label ID="lblSchoolAddress" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Poskod
        </td>
        <td>
            :<asp:Label ID="lblSchoolPostcode" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Bandar
        </td>
        <td>
            :<asp:Label ID="lblSchoolCity" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Negeri
        </td>
        <td>
            :<asp:Label ID="lblSchoolState" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Jenis Sekolah
        </td>
        <td>
            :<asp:Label ID="lblSchoolType" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
     <tr>
        <td>
            Tel#
        </td>
        <td>
            :<asp:Label ID="lblSchoolNoTel" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
     <tr>
        <td>
            Fak#
        </td>
        <td>
            :<asp:Label ID="lblSchoolNoFax" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
     <tr>
        <td>
            Email
        </td>
        <td>
            :<asp:Label ID="lblSchoolEmail" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
</table>