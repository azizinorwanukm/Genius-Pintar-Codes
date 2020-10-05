<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="admin.security_login_trail.list.aspx.vb" Inherits="permatapintar.admin_security_login_trail_list" %>

<%@ Register Src="commoncontrol/security_login_trail_list.ascx" TagName="security_login_trail_list"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label2" runat="server" Text="Lain-lain>Aktiviti Sistem" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:security_login_trail_list ID="security_login_trail_list1" runat="server" />
</asp:Content>
