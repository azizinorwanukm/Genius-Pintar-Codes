<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="MsgInbox_create.ascx.vb" Inherits="permatapintar.MsgInbox_create" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Mesej Baru
        </td>
    </tr>
    <tr>
        <td>
            Dari
        </td>
        <td>
            :<asp:TextBox ID="txtMsgFrom" runat="server" Width="150px" MaxLength="50" Text="" ReadOnly="true" CssClass="fbreadonly"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td style="width: 10%;">
            Kepada
        </td>
        <td>
            :<asp:DropDownList ID="ddlMsgTo" runat="server" AutoPostBack="true" Width="250px">
            </asp:DropDownList>*
        </td>
    </tr>
    <tr>
        <td>
            Subjek
        </td>
        <td>
            :<asp:TextBox ID="txtMsgSubject" runat="server" Width="450px" MaxLength="250"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td style="vertical-align:top;">
            Mesej
        </td>
        <td style="vertical-align:top;">
            &nbsp;<asp:TextBox ID="txtMsgBody" runat="server" Width="450px" TextMode="MultiLine" Rows="20"></asp:TextBox>*
        </td>
    </tr>

    <tr>
        <td>
            &nbsp;
        </td>
        <td class="fbform_sap">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td style="text-align: left;">
            <asp:Button ID="btnSubmit" runat="server" Text="Hantar" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="fbbutton" />
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label></div>