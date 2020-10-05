<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="student.profile.list.aspx.vb" Inherits="permatapintar.student_profile_list5" %>

<%@ Register Src="../commoncontrol/studentprofile_list.ascx" TagName="studentprofile_list"
    TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/ppcs_class_view.ascx" TagName="ppcs_class_view"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc2:ppcs_class_view ID="ppcs_class_view1" runat="server" />
    &nbsp;<uc1:studentprofile_list ID="studentprofile_list1" runat="server" />
    &nbsp;
</asp:Content>
