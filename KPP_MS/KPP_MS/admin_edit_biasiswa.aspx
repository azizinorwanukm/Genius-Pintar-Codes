<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_edit_biasiswa.aspx.vb" Inherits="KPP_MS.admin_edit_biasiswa" %>

<%@ Register Src="~/commoncontrol/scholarship_update.ascx" TagPrefix="uc1" TagName="scholarship_update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:scholarship_update runat="server" ID="scholarship_update" />

</asp:Content>
