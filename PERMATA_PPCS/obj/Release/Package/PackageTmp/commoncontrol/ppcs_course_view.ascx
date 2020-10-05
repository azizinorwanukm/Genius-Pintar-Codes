<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ppcs_course_view.ascx.vb"
    Inherits="permatapintar.ppcs_course_view" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Maklumat Kursus.
        </td>
    </tr>
    <tr>
        <td>
            Kod Kursus:
        </td>
        <td>
            <asp:TextBox ID="txtCourseCode" runat="server" Width="150px" MaxLength="254" ReadOnly="true"
                CssClass="fbreadonly"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            Nama Kursus (BM):
        </td>
        <td>
            <asp:TextBox ID="txtCourseNameBM" runat="server" Width="350px" MaxLength="150" ReadOnly="true"
                CssClass="fbreadonly"></asp:TextBox>&nbsp;
        </td>
    </tr>
</table>
