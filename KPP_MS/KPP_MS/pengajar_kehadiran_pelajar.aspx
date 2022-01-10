<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengajar.Master" CodeBehind="pengajar_kehadiran_pelajar.aspx.vb" Inherits="KPP_MS.pengajar_kehadiran_pelajar" %>

<%@ Register Src="~/commoncontrol/lecturer_attendanceData.ascx" TagPrefix="uc1" TagName="lecturer_attendanceData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:lecturer_attendanceData runat="server" id="lecturer_attendanceData" />

</asp:Content>
