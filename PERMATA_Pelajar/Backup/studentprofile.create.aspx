<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master"
    CodeBehind="studentprofile.create.aspx.vb" Inherits="permatapintar.studentprofile_create2" %>

<%@ Register Src="commoncontrol/studentprofile_create.ascx" TagName="studentprofile_create"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_create ID="studentprofile_create1" runat="server" />
</asp:Content>
