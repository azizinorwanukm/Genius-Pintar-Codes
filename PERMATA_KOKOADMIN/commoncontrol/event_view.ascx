<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="event_view.ascx.vb" Inherits="permatapintar.event_view" %>

<table class="fbform">
    <tr class="fbform_header">
        <td>Paparan Acara</td>
        <td style="vertical-align: top; text-align: right;">
            <asp:LinkButton ID="lnkUpdate" runat="server">Kemaskini</asp:LinkButton></td>
    </tr>
    <tr>
        <td class="fbtd_left">Tarikh:
        </td>
        <td>
            <asp:Label ID="lblEventDate" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Kokurikulum:
        </td>
        <td>
            <asp:Label ID="lblNama" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Tajuk:
        </td>
        <td>
            <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="vertical-align:top;">Agenda:
        </td>
        <td>
            <asp:Literal ID="ltAgenda" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr class="fbform_header">
        <td colspan="2">Maklumat Instruktor
        </td>
    </tr>

    <tr>
        <td class="fbtd_left">Nama Penuh:
        </td>
        <td>
            <asp:Label ID="lblFullname" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>MYKAD#:
        </td>
        <td>
            <asp:Label ID="lblMYKAD" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:Label ID="lblMsg" runat="server" CssClass="fblabel_msg" Text=""></asp:Label></td>
    </tr>
</table>
<asp:Label ID="lblInstruktorID" runat="server" Text="" Visible="false"></asp:Label>
