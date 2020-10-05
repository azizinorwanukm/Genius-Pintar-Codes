<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master" CodeBehind="default.aspx.vb" Inherits="permatapintar._default1" %>

<%@ Register Src="commoncontrol/user_login.ascx" TagName="user_login" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:user_login ID="user_login1" runat="server" />
    &nbsp;
</asp:Content>
