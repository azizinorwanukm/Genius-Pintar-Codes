<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="MsgInbox_view.ascx.vb" Inherits="permatapintar.MsgInbox_view" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Kandungan Mesej
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
            :<asp:TextBox ID="txtMsgTo" runat="server" Width="450px" MaxLength="50" Text="" ReadOnly="true" CssClass="fbreadonly"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Subjek
        </td>
        <td>
            :<asp:TextBox ID="txtMsgSubject" runat="server" Width="450px" MaxLength="500" ReadOnly="true" CssClass="fbreadonly"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td style="vertical-align:top;">
            Mesej
        </td>
        <td style="vertical-align:top;">
            &nbsp;<asp:TextBox ID="txtMsgBody" runat="server" Width="450px" TextMode="MultiLine" Rows="20" ReadOnly="true" CssClass="fbreadonly"></asp:TextBox>*
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
            <asp:Button ID="btnReply" runat="server" Text="Reply" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label></div>