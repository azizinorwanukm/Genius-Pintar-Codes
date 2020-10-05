<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/kpm.Master" CodeBehind="kpm.userprofile.update.aspx.vb" Inherits="permatapintar.kpm_userprofile_update" %>

<%@ Register Src="commoncontrol/userprofile_update_kpm.ascx" TagName="userprofile_update_kpm" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:userprofile_update_kpm ID="userprofile_update_kpm1" runat="server" />
    &nbsp;
</asp:Content>
