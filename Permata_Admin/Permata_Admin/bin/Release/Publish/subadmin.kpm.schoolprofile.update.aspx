<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/subadmin.Master"
    CodeBehind="subadmin.kpm.schoolprofile.update.aspx.vb" Inherits="permatapintar.subadmin_kpm_schoolprofile_update" %>

<%@ Register Src="commoncontrol/schoolprofile_update.ascx" TagName="schoolprofile_update"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:schoolprofile_update ID="schoolprofile_update1" runat="server" />
</asp:Content>
