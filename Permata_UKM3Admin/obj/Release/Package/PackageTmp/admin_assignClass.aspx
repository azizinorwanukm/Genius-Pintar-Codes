<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_assignClass.aspx.vb" Inherits="permatapintar.admin_assignClass1" %>

<%@ Register Src="~/commoncontrol/admin.assignClass.ascx" TagPrefix="uc1" TagName="adminassignClass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:adminassignClass runat="server" id="adminassignClass" />
</asp:Content>
