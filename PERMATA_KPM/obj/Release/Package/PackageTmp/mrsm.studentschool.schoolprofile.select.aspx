<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/mara.Master"
    CodeBehind="mrsm.studentschool.schoolprofile.select.aspx.vb" Inherits="permatapintar.mrsm_studentschool_schoolprofile_select" %>

<%@ Register Src="commoncontrol/studentschool.schoolprofile.select.ascx" TagName="studentschool"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/schoolprofile_view_old.ascx" TagName="schoolprofile_view_old"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc2:schoolprofile_view_old ID="schoolprofile_view_old1" runat="server" />
    &nbsp;
    <uc1:studentschool ID="studentschool1" runat="server" />
</asp:Content>
