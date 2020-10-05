<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/asasi.Master"
    CodeBehind="asasi.studentprofile.view.aspx.vb" Inherits="permatapintar.asasi_studentprofile_view" %>

<%@ Register Src="commoncontrol/schoolprofile_view.ascx" TagName="schoolprofile_view"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/studentschool_view.ascx" TagName="studentschool_view"
    TagPrefix="uc2" %>
<%@ Register Src="commoncontrol/studentprofile_view.ascx" TagName="studentprofile_view"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc3:studentprofile_view ID="studentprofile_view1" runat="server" />
    &nbsp;
    <uc2:studentschool_view ID="studentschool_view1" runat="server" />
</asp:Content>
