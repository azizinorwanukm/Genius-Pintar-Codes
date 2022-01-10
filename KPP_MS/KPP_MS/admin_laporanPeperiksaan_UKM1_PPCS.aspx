<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_laporanPeperiksaan_UKM1_PPCS.aspx.vb" Inherits="KPP_MS.admin_laporanPeperiksaan_UKM1_PPCS" %>

<%@ Register Src="~/commoncontrol/report_UKM1_PPCS.ascx" TagPrefix="uc1" TagName="report_UKM1_PPCS" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:report_UKM1_PPCS runat="server" id="report_UKM1_PPCS" />

</asp:Content>
