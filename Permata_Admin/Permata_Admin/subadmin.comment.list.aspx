<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/subadmin.Master" CodeBehind="subadmin.comment.list.aspx.vb" Inherits="permatapintar.subadmin_comment_list1" %>

<%@ Register Src="commoncontrol/user_comment.ascx" TagName="user_comment" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:user_comment ID="user_comment1" runat="server" />
</asp:Content>