<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="studentprofile_merge_header.ascx.vb"
    Inherits="permatapintar.studentprofile_merge_header" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Maklumat Pelajar. REKOD UTAMA
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">
            MYKAD#
        </td>
        <td>
            :<asp:Label ID="lblMYKAD" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Nama Pelajar
        </td>
        <td>
            :<asp:Label ID="lblStudentFullname" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Alumni ID
        </td>
        <td>
            :<asp:Label ID="lblAlumniID" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
</table>
