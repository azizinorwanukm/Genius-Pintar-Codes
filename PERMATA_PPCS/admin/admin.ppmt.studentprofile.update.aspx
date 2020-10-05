<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="admin.ppmt.studentprofile.update.aspx.vb" Inherits="permatapintar.admin_ppmt_studentprofile_update" %>

<%@ Register Src="../commoncontrol/studentprofile_update_mykad.ascx" TagName="studentprofile_update_mykad"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_update_mykad ID="studentprofile_update_mykad1" runat="server" />
</asp:Content>
