<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_laporanPPCS.aspx.vb" Inherits="KPP_MS.admin_laporanPPCS" %>

<%@ Register Src="~/commoncontrol/report_PPCS.ascx" TagPrefix="uc1" TagName="report_PPCS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:report_PPCS runat="server" ID="report_PPCS" />
</asp:Content>
