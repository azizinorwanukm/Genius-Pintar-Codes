<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/subadmin.Master" CodeBehind="subadmin.parentprofile.update.aspx.vb" Inherits="permatapintar.subadmin_parentprofile_update" %>

<%@ Register Src="commoncontrol/studentprofile_header.ascx" TagName="studentprofile_header"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/parentprofile_update.ascx" TagName="parentprofile_update"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_header ID="studentprofile_header1" runat="server" />&nbsp;
    <uc2:parentprofile_update ID="parentprofile_update1" runat="server" />
     
</asp:Content>
