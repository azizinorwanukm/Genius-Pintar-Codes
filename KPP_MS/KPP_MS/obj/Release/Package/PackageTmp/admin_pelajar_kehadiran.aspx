<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_pelajar_kehadiran.aspx.vb" Inherits="KPP_MS.admin_pelajar_kehadiran" %>

<%@ Register Src="~/commoncontrol/student_attendance.ascx" TagPrefix="uc1" TagName="student_attendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <uc1:student_attendance runat="server" ID="student_attendance" />

</asp:Content>
