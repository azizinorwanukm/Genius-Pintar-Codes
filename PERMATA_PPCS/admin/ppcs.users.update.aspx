<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="ppcs.users.update.aspx.vb" Inherits="permatapintar.ppcs_users_update8" %>

<%@ Register Src="../commoncontrol/admin_ppcsusers_update.ascx" TagName="admin_ppcsusers_update" TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/ppcs_users_year_list.ascx" TagName="ppcs_users_year_list" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:admin_ppcsusers_update ID="admin_ppcsusers_update1" runat="server" />
    &nbsp;
    <uc2:ppcs_users_year_list ID="ppcs_users_year_list1" runat="server" />
</asp:Content>
