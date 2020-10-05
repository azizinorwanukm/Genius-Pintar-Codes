<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.pcisadmin.create.aspx.vb" Inherits="araken.pcisadmin.admin_pcisadmin_create" %>

<%@ Register Src="~/commoncontrol/admin_create.ascx" TagPrefix="uc1" TagName="admin_create" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:admin_create runat="server" id="admin_create" />
</asp:Content>
