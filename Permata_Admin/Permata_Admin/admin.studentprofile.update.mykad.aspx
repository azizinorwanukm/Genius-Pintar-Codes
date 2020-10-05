<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.studentprofile.update.mykad.aspx.vb" Inherits="permatapintar.admin_studentprofile_update_mykad" %>

<%@ Register Src="commoncontrol/studentprofile_update_mykad.ascx" TagName="studentprofile_update_mykad"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_update_mykad ID="studentprofile_update_mykad1" runat="server" />
</asp:Content>