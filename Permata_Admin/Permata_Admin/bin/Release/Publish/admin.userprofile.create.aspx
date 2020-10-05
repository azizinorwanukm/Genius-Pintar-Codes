<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="admin.userprofile.create.aspx.vb" Inherits="permatapintar.admin_userprofile_create" %>

<%@ Register Src="commoncontrol/userprofile_create.ascx" TagName="userprofile_create"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:userprofile_create ID="userprofile_create1" runat="server" />
</asp:Content>
