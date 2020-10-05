<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="studentprofile_header_ppcs.ascx.vb"
    Inherits="permatapintar.studentprofile_header_ppcs" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">
            Maklumat Pelajar
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">
            Gambar:
        </td>
        <td>
            <asp:Image ID="imgStudent" Style="width: 120px; height: 150px;" runat="server" />
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">
            ID#
        </td>
        <td >
            :&nbsp;<asp:Label ID="lblMYKAD" runat="server" Text="" ForeColor="Black"></asp:Label>
        </td>
        <td >
            Sekolah
        </td>
        <td >
            :&nbsp;<asp:Label ID="lblSchoolName" runat="server" Text="" ForeColor="Black"></asp:Label>
        </td>
    </tr>
    <tr>
        <td >
            Nama Penuh
        </td>
        <td >
            :&nbsp;<asp:Label ID="lblRespFullname" runat="server" Text="" ForeColor="Black"></asp:Label>
        </td>
        <td >
            Negeri
        </td>
        <td >
            :&nbsp;<asp:Label ID="lblSchoolState" runat="server" Text="" ForeColor="Black"></asp:Label>&nbsp;&nbsp;
        </td>
    </tr>
</table>
<asp:Label ID="lblStudentID" runat="server" Text="" ForeColor="Black" Visible="false"></asp:Label>