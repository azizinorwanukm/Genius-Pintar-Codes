<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="master_config_create.ascx.vb" Inherits="permatapintar.master_config_create" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Kemaskini Sistem Konfigurasi
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">configCode</td>
        <td>
            <asp:TextBox ID="txtconfigCode" runat="server" Width="300px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">configString</td>
        <td>
            <asp:TextBox ID="txtconfigString" runat="server" Width="300px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">configDesc</td>
        <td>
            <asp:TextBox ID="txtconfigDesc" runat="server" Width="300px"></asp:TextBox>
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
            <asp:Button ID="btnCreate" runat="server" Text="Tambah" CssClass="fbbutton" />&nbsp;|&nbsp;<asp:LinkButton ID="lnkList" runat="server">Sistem Konfigurasi</asp:LinkButton>
        </td>
    </tr>
</table>
