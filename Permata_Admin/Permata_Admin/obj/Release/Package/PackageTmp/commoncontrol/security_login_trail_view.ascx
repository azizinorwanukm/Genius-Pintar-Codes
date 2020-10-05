<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="security_login_trail_view.ascx.vb" Inherits="permatapintar.security_login_trail_view" %>
<table class="fbform">
    <tr class="fbform_header">
        <td class="fbtd_left">Security Login Trail
        </td>
        <td>
        </td>
    </tr>
   
    <tr>
        <td>LoginID
        </td>
        <td>:<asp:Label ID="lblLoginID" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>LogTime
        </td>
        <td>:<asp:Label ID="lblLogTime" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>UserHostAddress
        </td>
        <td>:<asp:Label ID="lblUserHostAddress" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>UserHostName
        </td>
        <td>:<asp:Label ID="lblUserHostName" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>UserBrowser
        </td>
        <td>:<asp:Label ID="lblUserBrowser" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    
    <tr>
        <td >Activity
        </td>
        <td>:<asp:Label ID="lblActivity" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    
    <tr>
        <td>AuditDetail
        </td>
        <td>:<asp:Label ID="lblAuditDetail" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>

</table>