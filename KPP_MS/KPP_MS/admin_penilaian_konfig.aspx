<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_penilaian_konfig.aspx.vb" Inherits="KPP_MS.admin_penilaian_konfig" %>

<%@ Register Src="~/commoncontrol/admin_assessment_config.ascx" TagPrefix="uc1" TagName="admin_assessment_config" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:admin_assessment_config runat="server" id="admin_assessment_config" />
</asp:Content>
