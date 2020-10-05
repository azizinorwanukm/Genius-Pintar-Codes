<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="kelas_courseView.ascx.vb" Inherits="UKM3.kelas_courseView" %>
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

