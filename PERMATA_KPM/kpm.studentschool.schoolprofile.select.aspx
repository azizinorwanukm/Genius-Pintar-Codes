<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/kpm.Master" CodeBehind="kpm.studentschool.schoolprofile.select.aspx.vb" Inherits="permatapintar.kpm_studentschool_schoolprofile_select" %>
<%@ Register src="commoncontrol/studentschool.schoolprofile.select.ascx" tagname="studentschool" tagprefix="uc1" %>
<%@ Register src="commoncontrol/schoolprofile_view_old.ascx" tagname="schoolprofile_view_old" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc2:schoolprofile_view_old ID="schoolprofile_view_old1" runat="server" />&nbsp;
    <uc1:studentschool ID="studentschool1" runat="server" />
    
</asp:Content>
