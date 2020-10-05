<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="studentprofile_master.ascx.vb"
    Inherits="permatapintar.studentprofile_master" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Maklumat Anda
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">
            MYKAD/MYKID#:
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblMYKAD" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Nama Pelajar:
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblStudentFullname" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
</table>
