<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="admin.studentprofile.view.aspx.vb" Inherits="permatapintar.admin_studentprofile_view" %>

<%@ Register Src="../commoncontrol/studentprofile_view.ascx" TagName="studentprofile_view"
    TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/parentprofile_view.ascx" TagName="parentprofile_view"
    TagPrefix="uc3" %>
<%@ Register Src="../commoncontrol/studentschool_view.ascx" TagName="studentschool_view"
    TagPrefix="uc2" %>
<%@ Register Src="../commoncontrol/ppcs_view.ascx" TagName="ppcs_view" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_view ID="studentprofile_view1" runat="server" />
    <br />
    <uc2:studentschool_view ID="studentschool_view1" runat="server" />
    <br />
    <uc3:parentprofile_view ID="parentprofile_view1" runat="server" />
    <br />
    <uc4:ppcs_view ID="ppcs_view1" runat="server" />
</asp:Content>
