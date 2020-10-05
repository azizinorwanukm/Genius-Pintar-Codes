<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pcis_config_update.ascx.vb" Inherits="araken.pcisadmin.pcis_config_update" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Sistem Konfigurasi
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">configCode
        </td>
        <td>:<asp:TextBox ID="txtconfigCode" runat="server" Width="500px" MaxLength="150" Text=""></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>configString
        </td>
        <td>:<asp:TextBox ID="txtconfigString" runat="server" Width="500px" MaxLength="250" Text=""></asp:TextBox>
        </td>
    </tr>
     <tr>
        <td class="fbform_sap" colspan="2"></td>
    </tr>
    <tr>
        <td  colspan="2">
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnDelete" runat="server" Text="Padam" CssClass="fbbutton" />&nbsp;|&nbsp;
            <asp:LinkButton ID="lnkView" runat="server">Sistem Konfigurasi</asp:LinkButton>
        </td>
    </tr>
    <tr class="fbform_msg">
        <td colspan="2">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
    </tr>
</table>

