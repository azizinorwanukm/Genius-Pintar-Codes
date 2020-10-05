<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pembantupengajar/main.Master" CodeBehind="student.profile.student.list.aspx.vb" Inherits="permatapintar.student_profile_student_list4" %>

<%@ Register Src="../commoncontrol/ppcs_list_classid_session.ascx" TagName="ppcs_list_classid_session"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ppcs_list_classid_session ID="ppcs_list_classid_session1" runat="server" />
    &nbsp;
</asp:Content>
