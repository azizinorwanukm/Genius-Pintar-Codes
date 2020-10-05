<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ppcs_user_view.ascx.vb"
    Inherits="permatapintar.ppcs_user_view" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Maklumat Petugas PPCS
        </td>
    </tr>
    <tr>
        <td>
            Login ID(E-Mail):
        </td>
        <td>
            <asp:Label ID="lblLoginID" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Kata Laluan:
        </td>
        <td>
            <asp:Label ID="lblPwd" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Nama penuh:
        </td>
        <td>
            <asp:Label ID="lblFullname" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Nombor IC:
        </td>
        <td>
            <asp:Label ID="lblICNo" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            No. Telefon:
        </td>
        <td>
            <asp:Label ID="lblContactNo" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">
            Alamat:
        </td>
        <td>
            <asp:Label ID="lblAddress" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">
            Poskod:
        </td>
        <td>
            <asp:Label ID="lblPostcode" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">
            Bandar:
        </td>
        <td>
            <asp:Label ID="lblCity" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">
            Negeri:
        </td>
        <td>
            <asp:Label ID="lblState" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
</table>
