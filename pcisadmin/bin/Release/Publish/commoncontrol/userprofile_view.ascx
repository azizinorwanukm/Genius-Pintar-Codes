<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="userprofile_view.ascx.vb" Inherits="araken.pcisadmin.userprofile_view" %>

<table class="fbform">
    <tr class="fbform_header">
        <td class="fbtd_left">Maklumat Pelajar
        </td>
        <td style="text-align: right;">
            <asp:LinkButton ID="lnkEdit" runat="server">Kemaskini</asp:LinkButton>|<asp:LinkButton ID="lnkDelete" runat="server">Padam</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>MYKAD/MYKID#
        </td>
        <td>:<asp:Label ID="lblicno" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Nama Penuh
        </td>
        <td>:<asp:Label ID="lblfullname" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Alamat Rumah
        </td>
        <td>:<asp:Label ID="lbladdress" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Tel#
        </td>
        <td>:<asp:Label ID="lblphoneno" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Email
        </td>
        <td>:<asp:Label ID="lblemail" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>

    <tr>
        <td>Nama Ibu
        </td>
        <td>:<asp:Label ID="lblmothername" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Pekerjaan Ibu
        </td>
        <td>:<asp:Label ID="lblmotheroccupation" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Nama Bapa
        </td>
        <td>:<asp:Label ID="lblfathername" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Pekerjaan Bapa
        </td>
        <td>:<asp:Label ID="lblfatheroccupation" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
</table>
&nbsp;
<table class="fbform">
    <tr class="fbform_header">
        <td class="fbtd_left">Maklumat PAPN
        </td>
        <td style="text-align: right;">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>Nama Pusat
        </td>
        <td>:<asp:Label ID="lbllearningcentrename" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Alamat
        </td>
        <td>:<asp:Label ID="lbllearningcentreaddress" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Negeri
        </td>
        <td>:<asp:Label ID="lbllearningcentrestatename" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Tel#
        </td>
        <td>:<asp:Label ID="lbllearningcentrephoneno" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Nama Pembantu
        </td>
        <td>:<asp:Label ID="lblassistantname" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>

    <tr>
        <td>Tel#
        </td>
        <td>:<asp:Label ID="lblassistantphoneno" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
</table>
<asp:Label ID="lblMsg" runat="server" Text="" Visible="false"></asp:Label>