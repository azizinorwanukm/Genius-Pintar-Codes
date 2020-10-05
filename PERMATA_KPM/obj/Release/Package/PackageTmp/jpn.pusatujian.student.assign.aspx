<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/jpn.Master" CodeBehind="jpn.pusatujian.student.assign.aspx.vb" Inherits="permatapintar.jpn_pusatujian_student_assign" %>
<%@ Register Src="commoncontrol/studentprofile_view.ascx" TagName="studentprofile_view"
    TagPrefix="uc1" %>
<%@ Register src="commoncontrol/studentschool_view.ascx" tagname="studentschool_view" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_view ID="studentprofile_view1" runat="server" />
    &nbsp;<uc2:studentschool_view ID="studentschool_view1" runat="server" />
</asp:Content>
