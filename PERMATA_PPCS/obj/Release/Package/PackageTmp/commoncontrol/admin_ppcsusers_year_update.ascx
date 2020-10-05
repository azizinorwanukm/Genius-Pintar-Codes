<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin_ppcsusers_year_update.ascx.vb"
    Inherits="permatapintar.admin_ppcsusers_year_update" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Kemaskini Maklumat Petugas PPCS
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">
            PPCS Date:
        </td>
        <td>
            <asp:DropDownList ID="ddlPPCSDate" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            User Type:
        </td>
        <td>
            <asp:DropDownList ID="ddlUserType" runat="server" AutoPostBack="false" Width="200px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr style="text-align: left">
        <td>
            &nbsp;
        </td>
        <td class="fbaside_sap">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red" Style="text-align: left"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Button ID="btnUpdate" runat="server" Text=" Kemaskini " CssClass="fbbutton" />&nbsp;|<asp:LinkButton
                ID="lnkppcsuserlist" runat="server">Senarai Petugas</asp:LinkButton>
        </td>
    </tr>
</table>
