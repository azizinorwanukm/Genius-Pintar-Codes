<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ppcs_class_view.ascx.vb" Inherits="permatapintar.ppcs_class_view" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Maklumat Kursus dan Kelas
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">
            *Kod Kursus:
        </td>
        <td>
            <asp:TextBox ID="txtPPCSCourse" runat="server" Width="150px" MaxLength="254" ReadOnly="true"
                CssClass="fbreadonly"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            *Nama Kursus (BM):
        </td>
        <td>
            <asp:TextBox ID="txtCourseNameBM" runat="server" Width="350px" MaxLength="150" ReadOnly="true"
                CssClass="fbreadonly"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td class="column_width">
            *Kod Kelas:
        </td>
        <td>
            <asp:TextBox ID="txtClassCode" runat="server" Width="150px" MaxLength="254" ReadOnly="true"
                CssClass="fbreadonly"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td class="column_width">
            *Nama Kelas:
        </td>
        <td>
            <asp:TextBox ID="txtClassNameBM" runat="server" Width="350px" MaxLength="254" ReadOnly="true"
                CssClass="fbreadonly"></asp:TextBox>&nbsp;
        </td>
    </tr>
</table>
