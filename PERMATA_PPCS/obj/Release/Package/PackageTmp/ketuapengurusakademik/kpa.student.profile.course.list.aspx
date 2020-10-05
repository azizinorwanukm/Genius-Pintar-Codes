<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ketuapengurusakademik/main.Master" CodeBehind="kpa.student.profile.course.list.aspx.vb" Inherits="permatapintar.kpa_student_profile_course_list" %>
<%@ Register Src="../commoncontrol/ppcs_course_select.ascx" TagName="ppcs_course_select"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ppcs_course_select ID="ppcs_course_select1" runat="server" />
    &nbsp;
</asp:Content>
