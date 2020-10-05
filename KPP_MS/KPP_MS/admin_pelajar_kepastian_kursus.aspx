<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_pelajar_kepastian_kursus.aspx.vb" Inherits="KPP_MS.admin_pelajar_kepastian_kursus" %>

<%@ Register Src="~/commoncontrol/student_view_course.ascx" TagPrefix="uc1" TagName="student_view_course" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:student_view_course runat="server" id="student_view_course" />
</asp:Content>
