<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="master_config_update.ascx.vb" Inherits="permatapintar.master_config_update" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Kemaskini Sistem Konfigurasi
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Penerangan</td>
        <td>
            <asp:TextBox ID="txtdescription" runat="server" Width="300px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Jenis</td>
        <td>
            <asp:TextBox ID="txttype" runat="server" Width="300px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td class="fbform_sap">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td style="text-align: left;">
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;<asp:Button ID="btnDelete" runat="server" Text="Hapuskan" CssClass="fbbutton" />&nbsp;|&nbsp;<asp:LinkButton ID="lnkList" runat="server">Sistem Konfigurasi</asp:LinkButton>
        </td>
    </tr>
</table>
