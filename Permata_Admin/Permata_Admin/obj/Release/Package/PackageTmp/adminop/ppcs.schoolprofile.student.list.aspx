<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="ppcs.schoolprofile.student.list.aspx.vb" Inherits="permatapintar.ppcs_schoolprofile_student_list1" %>

<%@ Register Src="../commoncontrol/schoolprofile_view.ascx" TagName="schoolprofile_view"
    TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/ppcs_school_studentprofile_list.ascx" TagName="ppcs_school_studentprofile_list"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:schoolprofile_view ID="schoolprofile_view1" runat="server" />
    &nbsp;
    <uc2:ppcs_school_studentprofile_list ID="ppcs_school_studentprofile_list1" runat="server" />
</asp:Content>