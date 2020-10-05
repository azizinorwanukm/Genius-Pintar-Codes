<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="studentuni_view.ascx.vb" Inherits="permatapintar.studentuni_view" %>

<table class="fbform">
    <tr class="fbform_header">
        <td>Maklumat Universiti
        </td>
        <td style="text-align: right;">
            <asp:LinkButton ID="lnkEdit" runat="server">Kemaskini</asp:LinkButton>
        </td>
    </tr>

    <tr>
        <td style="width: 20%;">Nama Universiti
        </td>
        <td>:<asp:Label ID="lblUniName" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>

    <tr>
        <td>Negara
        </td>
        <td>:<asp:Label ID="lblUniCountry" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Bidang Pengajian
        </td>
        <td>:<asp:Label ID="lblUniCourse" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>

</table>
