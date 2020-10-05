<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm1.default.Master"
    CodeBehind="page.studentprofile.create.aspx.vb" Inherits="permatapintar.page_studentprofile_create" %>

<%@ Register Src="commoncontrol/studentprofile_create.ascx" TagName="studentprofile_create"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_create ID="studentprofile_create1" runat="server" />
</asp:Content>
