<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pembantupelajar/main.Master" CodeBehind="student.profile.view.aspx.vb" Inherits="permatapintar.student_profile_view7" %>
<%@ Register src="../commoncontrol/studentprofile_view.ascx" tagname="studentprofile_view" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:studentprofile_view ID="studentprofile_view1" runat="server" />

</asp:Content>