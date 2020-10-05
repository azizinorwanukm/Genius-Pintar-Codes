<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/main.Master" CodeBehind="student.schoolprofile.create.aspx.vb" Inherits="permatapintar.student_schoolprofile_create" %>
<%@ Register src="commoncontrol/schoolprofile_create.ascx" tagname="schoolprofile_create" tagprefix="uc1" %>
<%@ Register src="commoncontrol/studentprofile_header.ascx" tagname="studentprofile_header" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc2:studentprofile_header ID="studentprofile_header1" runat="server" />
&nbsp;<uc1:schoolprofile_create ID="schoolprofile_create1" runat="server" />
</asp:Content>
