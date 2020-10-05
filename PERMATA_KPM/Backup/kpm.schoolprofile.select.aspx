<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/kpm.Master"
    CodeBehind="kpm.schoolprofile.select.aspx.vb" Inherits="permatapintar.kpm_schoolprofile_select" %>

<%@ Register src="commoncontrol/schoolprofile_select.ascx" tagname="schoolprofile_select" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:schoolprofile_select ID="schoolprofile_select1" runat="server" />
&nbsp;
</asp:Content>
