<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_laporanUKM1.aspx.vb" Inherits="KPP_MS.admin_laporanUKM1" %>

<%@ Register Src="~/commoncontrol/report_UKM1.ascx" TagPrefix="uc1" TagName="report_UKM1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:report_UKM1 runat="server" ID="report_UKM1" />

</asp:Content>
