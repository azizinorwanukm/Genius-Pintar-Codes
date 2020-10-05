<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="student.schoolprofile.create.aspx.vb" Inherits="permatapintar.student_schoolprofile_create" %>

<%@ Register Src="../commoncontrol/schoolprofile_create.ascx" TagName="schoolprofile_create"
    TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/studentprofile_header_ppcs.ascx" TagName="studentprofile_header_ppcs"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    &nbsp;<uc3:studentprofile_header_ppcs ID="studentprofile_header_ppcs1" runat="server" />
    <uc1:schoolprofile_create ID="schoolprofile_create1" runat="server" />
</asp:Content>
