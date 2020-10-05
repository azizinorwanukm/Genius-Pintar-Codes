<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="iprofile_userlist.aspx.vb" Inherits="permatapintar.iprofile_userlist" %>

<%@ Register Src="user_select.ascx" TagName="user_select" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:user_select ID="user_select1" runat="server" />
    &nbsp;
</asp:Content>
