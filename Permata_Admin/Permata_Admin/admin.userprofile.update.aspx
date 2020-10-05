<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.userprofile.update.aspx.vb" Inherits="permatapintar.admin_userprofile_update" %>

<%@ Register Src="commoncontrol/userprofile_update.ascx" TagName="userprofile_update" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:userprofile_update ID="userprofile_update1" runat="server" />
</asp:Content>
