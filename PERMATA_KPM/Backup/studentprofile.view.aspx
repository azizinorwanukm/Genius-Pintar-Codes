<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/kpm.Master"
    CodeBehind="studentprofile.view.aspx.vb" Inherits="permatapintar.studentprofile_view" %>

<%@ Register Src="commoncontrol/studentprofile_view.ascx" TagName="studentprofile_view"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/parentprofile_view.ascx" TagName="parentprofile_view"
    TagPrefix="uc3" %>
<%@ Register Src="commoncontrol/studentschool_view.ascx" TagName="studentschool_view"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_view ID="studentprofile_view1" runat="server" />
    &nbsp;<uc4:studentschool_view ID="studentschool_view1" runat="server" />
    &nbsp;
    <uc3:parentprofile_view ID="parentprofile_view1" runat="server" />
</asp:Content>
