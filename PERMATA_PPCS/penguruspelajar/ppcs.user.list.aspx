<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/penguruspelajar/main.Master" CodeBehind="ppcs.user.list.aspx.vb" Inherits="permatapintar.ppcs_user_list" %>

<%@ Register Src="../commoncontrol/ppcs_users_list.ascx" TagName="ppcs_users_list"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ppcs_users_list ID="ppcs_users_list1" runat="server" />
    &nbsp;
</asp:Content>