<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/main.Master" CodeBehind="student.parentprofile.create.aspx.vb" Inherits="permatapintar.student_parentprofile_create" %>

<%@ Register Src="commoncontrol/parentprofile_create.ascx" TagName="parentprofile_create"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/studentprofile_header.ascx" TagName="studentprofile_header"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc2:studentprofile_header ID="studentprofile_header1" runat="server" />
    &nbsp;<uc1:parentprofile_create ID="parentprofile_create1" runat="server" />
</asp:Content>
