<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.studentprofile.import.aspx.vb" Inherits="permatapintar.admin_studentprofile_import" %>
<%@ Register src="commoncontrol/studentprofile_import.ascx" tagname="studentprofile_import" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_import ID="studentprofile_import1" runat="server" />
</asp:Content>
