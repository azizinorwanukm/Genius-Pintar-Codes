<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="tempahan_view.ascx.vb" Inherits="permatapintar.tempahan_view" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Tempahan
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">TempahanID:
        </td>
        <td>
            <asp:Label ID="lblTempahanID" runat="server" Text=""></asp:Label>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>Tahun:
        </td>
        <td>
            <asp:Label ID="lblTahun" runat="server" Text=""></asp:Label>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>Nama Kemudahan:
        </td>
        <td>
            <asp:Label ID="lblKemudahan" runat="server" Text=""></asp:Label>
        </td>
        <td></td>
        <td></td>
    </tr>

    <tr>
        <td style="vertical-align: top;">Tarikh Tempahan:
        </td>
        <td>
            <asp:Label ID="lblBookingDate" runat="server" Text=""></asp:Label>
        </td>
        <td></td>
        <td></td>
    </tr>
     <tr>
        <td style="vertical-align:top;">Masa:</td>
        <td>AM:<asp:CheckBox ID="Time07" Text="07:00-08:00" runat="server" AutoPostBack="true" /><asp:CheckBox ID="Time08" Text="08:00-09:00" runat="server" AutoPostBack="true" /><asp:CheckBox ID="Time09" Text="09:00-10:00" runat="server" AutoPostBack="true" /><asp:CheckBox ID="Time10" Text="10:00-11:00" runat="server" AutoPostBack="true" /><asp:CheckBox ID="Time11" Text="11:00-12:00" runat="server" AutoPostBack="true" /><asp:CheckBox ID="Time12" Text="12:00-01:00" runat="server" AutoPostBack="true" /><br />
            PM:<asp:CheckBox ID="Time13" Text="01:00-02:00" runat="server" AutoPostBack="true" /><asp:CheckBox ID="Time14" Text="02:00-03:00" runat="server" AutoPostBack="true" /><asp:CheckBox ID="Time15" Text="03:00-04:00" runat="server" AutoPostBack="true" /><asp:CheckBox ID="Time16" Text="04:00-05:00" runat="server" AutoPostBack="true" /><asp:CheckBox ID="Time17" Text="05:00-06-00" runat="server" AutoPostBack="true" /><asp:CheckBox ID="Time18" Text="06:00-07:00" runat="server" AutoPostBack="true" /><br />
            PM:<asp:CheckBox ID="Time19" Text="07:00-08:00" runat="server" AutoPostBack="true" /><asp:CheckBox ID="Time20" Text="08:00-09:00" runat="server" AutoPostBack="true" /><asp:CheckBox ID="Time21" Text="09:00-10:00" runat="server" AutoPostBack="true" /><asp:CheckBox ID="Time22" Text="10:00-11:00" runat="server" AutoPostBack="true" /><asp:CheckBox ID="Time23" Text="11:00-12:00" runat="server" AutoPostBack="true" />
        </td>
    </tr>
    <tr>
        <td colspan="4" class="fbform_sap"></td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Nama Pemohon:
        </td>
        <td>
            <asp:Label ID="lblPemohon" runat="server" Text=""></asp:Label>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>Tel#:</td>
        <td>
            <asp:Label ID="lblContactNo" runat="server" Text=""></asp:Label>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr style="vertical-align: top;">
        <td>Catatan:
        </td>
        <td>
            <asp:Label ID="lblCatatan" runat="server" Text=""></asp:Label>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>Status:</td>
        <td>
            <asp:Label ID="lblStatusTempahan" runat="server" Text=""></asp:Label></td>
        <td></td>
        <td></td>
    </tr>
     <tr style="vertical-align: top;">
        <td>Catatan Pengarah:
        </td>
        <td>
            <asp:Label ID="lblCatatanPengarah" runat="server" Text=""></asp:Label>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>Masa Cetakan:</td>
        <td>
            <asp:Label ID="lblPrintDate" runat="server" Text=""></asp:Label></td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td></td>
        <td colspan="3">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
        </td>
    </tr>
</table>
