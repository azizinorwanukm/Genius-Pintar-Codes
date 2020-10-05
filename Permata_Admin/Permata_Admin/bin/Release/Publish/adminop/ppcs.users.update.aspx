<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="ppcs.users.update.aspx.vb" Inherits="permatapintar.ppcs_users_update" %>

<%@ Register Src="../commoncontrol/ppcs_users_year_list.ascx" TagName="ppcs_users_year_list"
    TagPrefix="uc2" %>
<%@ Register Src="../commoncontrol/ppcs_user_update.ascx" TagName="ppcs_user_update"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ppcs_user_update ID="ppcs_user_update1" runat="server" />
    &nbsp;&nbsp;
    <uc2:ppcs_users_year_list ID="ppcs_users_year_list1" runat="server" />
</asp:Content>
