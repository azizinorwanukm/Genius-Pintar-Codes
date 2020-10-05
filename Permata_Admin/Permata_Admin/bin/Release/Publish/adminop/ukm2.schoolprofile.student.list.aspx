<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="ukm2.schoolprofile.student.list.aspx.vb" Inherits="permatapintar.ukm2_schoolprofile_student_list1" %>

<%@ Register Src="../commoncontrol/ukm2_school_studentprofile_list.ascx" TagName="ukm2_school_studentprofile_list"
    TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/schoolprofile_view.ascx" TagName="schoolprofile_view"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc2:schoolprofile_view ID="schoolprofile_view1" runat="server" />
    &nbsp;<uc1:ukm2_school_studentprofile_list ID="ukm2_school_studentprofile_list1" runat="server" />
</asp:Content>
