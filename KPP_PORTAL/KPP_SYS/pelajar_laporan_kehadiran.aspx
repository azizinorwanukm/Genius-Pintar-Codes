<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pelajar.Master" CodeBehind="pelajar_laporan_kehadiran.aspx.vb" Inherits="KPP_SYS.pelajar_laporan_kehadiran" %>

<%@ Register Src="~/commoncontrol/student_attendanceData.ascx" TagPrefix="uc1" TagName="student_attendanceData" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:student_attendanceData runat="server" ID="student_attendanceData" />
</asp:Content>
