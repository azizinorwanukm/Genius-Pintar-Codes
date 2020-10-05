<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_laporanUKM3.aspx.vb" Inherits="KPP_MS.admin_laporanUKM3" %>

<%@ Register Src="~/commoncontrol/report_UKM3.ascx" TagPrefix="uc1" TagName="report_UKM3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:report_UKM3 runat="server" ID="report_UKM3" />

</asp:Content>
