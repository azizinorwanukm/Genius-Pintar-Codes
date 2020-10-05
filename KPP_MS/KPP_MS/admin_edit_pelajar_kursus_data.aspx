<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_edit_pelajar_kursus_data.aspx.vb" Inherits="KPP_MS.admin_edit_pelajar_kursus_data" %>

<%@ Register Src="~/commoncontrol/student_update_course.ascx" TagPrefix="uc1" TagName="student_update_course" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:student_update_course runat="server" id="student_update_course" />
</asp:Content>
