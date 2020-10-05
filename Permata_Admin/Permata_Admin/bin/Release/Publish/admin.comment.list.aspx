<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="admin.comment.list.aspx.vb" Inherits="permatapintar.admin_comment_list" %>

<%@ Register Src="commoncontrol/user_comment.ascx" TagName="user_comment" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:user_comment ID="user_comment1" runat="server" />
</asp:Content>
