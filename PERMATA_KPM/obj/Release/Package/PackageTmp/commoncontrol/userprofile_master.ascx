<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="userprofile_master.ascx.vb"
    Inherits="permatapintar.userprofile_master" %>
<table class="fbform">
    <tr class="fbform_header">
        <td>
            Maklumat Anda
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            Login ID:
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblLoginID" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Nama Pengguna:
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblFullname" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
</table>
