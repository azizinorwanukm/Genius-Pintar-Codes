<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.login.success.aspx.vb" Inherits="permatapintar.admin_login_success" %>

<%@ Register Src="../commoncontrol/tor_instruktor.ascx" TagName="tor_instruktor" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:tor_instruktor ID="tor_instruktor1" runat="server" />
    &nbsp;
</asp:Content>