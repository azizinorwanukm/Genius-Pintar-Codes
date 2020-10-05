<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_laporanUKM2.aspx.vb" Inherits="KPP_MS.admin_laporanUKM2" %>

<%@ Register Src="~/commoncontrol/report_UKM2.ascx" TagPrefix="uc1" TagName="report_UKM2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:report_UKM2 runat="server" ID="report_UKM2" />
</asp:Content>
