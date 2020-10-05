<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/subadmin.Master"
    CodeBehind="subadmin.studentschool.schoolprofile.select.aspx.vb" Inherits="permatapintar.subadmin_studentschool_schoolprofile_select" %>

<%@ Register Src="commoncontrol/schoolprofile_view_old.ascx" TagName="schoolprofile_view_old"
    TagPrefix="uc2" %>
<%@ Register Src="commoncontrol/studentschool_schoolprofile_select.ascx" TagName="studentschool_schoolprofile_select"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc2:schoolprofile_view_old ID="schoolprofile_view_old1" runat="server" />
    &nbsp;
    <uc1:studentschool_schoolprofile_select ID="studentschool_schoolprofile_select1"
        runat="server" />
</asp:Content>
