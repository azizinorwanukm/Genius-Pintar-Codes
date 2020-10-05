<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="ppcs.users.year.update.aspx.vb" Inherits="permatapintar.ppcs_users_year_update" %>

<%@ Register Src="../commoncontrol/admin_ppcsusers_view.ascx" TagName="admin_ppcsusers_view"
    TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/admin_ppcsusers_year_update.ascx" TagName="admin_ppcsusers_year_update"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:admin_ppcsusers_view ID="admin_ppcsusers_view1" runat="server" />
    &nbsp;
    <uc2:admin_ppcsusers_year_update ID="admin_ppcsusers_year_update1" runat="server" />
</asp:Content>
