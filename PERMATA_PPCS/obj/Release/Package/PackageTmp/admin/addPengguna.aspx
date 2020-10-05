<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="addPengguna.aspx.vb" Inherits="permatapintar.addPengguna" %>

<%@ Register Src="../commoncontrol/admin_ppcsusers_create.ascx" TagName="admin_ppcsusers_create"
    TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/admin_ppcs_user_list_usertype.ascx" TagName="admin_ppcs_user_list_usertype"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:admin_ppcsusers_create ID="admin_ppcsusers_create1" runat="server" />
</asp:Content>
