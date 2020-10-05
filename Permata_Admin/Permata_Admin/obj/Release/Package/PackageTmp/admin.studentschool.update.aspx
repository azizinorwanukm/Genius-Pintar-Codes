<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.studentschool.update.aspx.vb" Inherits="permatapintar.admin_studentschool_update" %>

<%@ Register Src="commoncontrol/studentschool_update.ascx" TagName="studentschool_update" TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/studentprofile_header.ascx" TagName="studentprofile_header" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc2:studentprofile_header ID="studentprofile_header1" runat="server" />
    &nbsp;<uc1:studentschool_update ID="studentschool_update1" runat="server" />
    &nbsp;
</asp:Content>
