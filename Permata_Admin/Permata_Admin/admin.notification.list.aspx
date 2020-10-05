<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="admin.notification.list.aspx.vb" Inherits="permatapintar.admin_notification_list" %>

<%@ Register Src="commoncontrol/user_post.ascx" TagName="user_post" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:user_post ID="user_post1" runat="server" />
</asp:Content>
