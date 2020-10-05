<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="student.schoolprofile.create.aspx.vb" Inherits="permatapintar.student_schoolprofile_create" %>

<%@ Register Src="commoncontrol/schoolprofile_create.ascx" TagName="schoolprofile_create" TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/studentprofile_header.ascx" TagName="studentprofile_header" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc2:studentprofile_header ID="studentprofile_header1" runat="server" />
    &nbsp;<uc1:schoolprofile_create ID="schoolprofile_create1" runat="server" />
    <asp:LinkButton ID="lnkStudentProfileView" runat="server">Maklumat Pelajar</asp:LinkButton>
</asp:Content>
