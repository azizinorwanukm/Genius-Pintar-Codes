<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="master_PPCSStatus_update.ascx.vb" Inherits="permatapintar.master_PPCSStatus_update1" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Kemaskini PPCS Status
        </td>
    </tr>
    
    <tr>
        <td class="fbtd_left">PPCS Status</td>
        <td>
            <asp:TextBox ID="PPCSStatus" runat="server" Width="300px"></asp:TextBox>
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
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini " CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnDelete" runat="server" Text="Padam " CssClass="fbbutton" />&nbsp;|&nbsp;<asp:LinkButton ID="lnkBrowse" runat="server">Senarai PPCS Status</asp:LinkButton>
        </td>
    </tr>
</table>