<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.schoolprofile.list.pindah.confirm.aspx.vb" Inherits="permatapintar.admin_schoolprofile_list_pindah_confirm" %>

<%@ Register Src="commoncontrol/schoolprofile_list_pindah_confirm.ascx" TagName="schoolprofile_list_pindah_confirm" TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/schoolprofile_view_change.ascx" TagName="schoolprofile_view_change" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc3:schoolprofile_view_change ID="schoolprofile_view_change1" runat="server" />
    <uc1:schoolprofile_list_pindah_confirm ID="schoolprofile_list_pindah_confirm1" runat="server" />
</asp:Content>
