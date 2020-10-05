<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.security_login_trail.view.aspx.vb" Inherits="permatapintar.admin_security_login_trail_view" %>

<%@ Register Src="commoncontrol/security_login_trail_view.ascx" TagName="security_login_trail_view" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:security_login_trail_view ID="security_login_trail_view1" runat="server" />
</asp:Content>
