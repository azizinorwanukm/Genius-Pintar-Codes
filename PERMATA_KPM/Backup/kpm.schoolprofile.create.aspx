<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/kpm.Master" CodeBehind="kpm.schoolprofile.create.aspx.vb" Inherits="permatapintar.kpm_schoolprofile_create" %>
<%@ Register src="commoncontrol/schoolprofile_create.ascx" tagname="schoolprofile_create" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:schoolprofile_create ID="schoolprofile_create1" runat="server" />
</asp:Content>
