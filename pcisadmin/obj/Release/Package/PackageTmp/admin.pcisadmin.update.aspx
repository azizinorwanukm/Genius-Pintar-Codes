<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.pcisadmin.update.aspx.vb" Inherits="araken.pcisadmin.admin_pcisadmin_update" %>

<%@ Register Src="~/commoncontrol/admin_update.ascx" TagPrefix="uc1" TagName="admin_update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:admin_update runat="server" id="admin_update" />
</asp:Content>
