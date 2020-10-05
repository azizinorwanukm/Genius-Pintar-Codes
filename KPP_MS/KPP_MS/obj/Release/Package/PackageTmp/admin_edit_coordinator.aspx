<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_edit_coordinator.aspx.vb" Inherits="KPP_MS.admin_edit_coordinator" %>

<%@ Register Src="~/commoncontrol/coordinator_Update.ascx" TagPrefix="uc1" TagName="coordinator_Update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:coordinator_Update runat="server" id="coordinator_Update" />
</asp:Content>
