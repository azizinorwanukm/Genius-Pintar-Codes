<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="ppcs.users.year.update.aspx.vb" Inherits="permatapintar.ppcs_users_year_update" %>

<%@ Register Src="commoncontrol/ppcs_user_view.ascx" TagName="ppcs_user_view" TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/ppcs_users_year_update.ascx" TagName="ppcs_users_year_update"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ppcs_user_view ID="ppcs_user_view1" runat="server" />
    &nbsp;<uc2:ppcs_users_year_update ID="ppcs_users_year_update1" runat="server" />
    &nbsp;
</asp:Content>
