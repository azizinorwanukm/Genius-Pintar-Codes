<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.sukanmandatory.update.p2.aspx.vb" Inherits="permatapintar.admin_sukanmandatory_update_p2" %>

<%@ Register Src="~/commoncontrol/instruktor_view_header.ascx" TagPrefix="uc1" TagName="instruktor_view_header" %>
<%@ Register Src="~/commoncontrol/koko_pelajar_mark_sukanmandatory_p2.ascx" TagPrefix="uc2" TagName="koko_pelajar_mark_sukanmandatory_p2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:instruktor_view_header runat="server" ID="instruktor_view_header" />
    &nbsp;
    <uc2:koko_pelajar_mark_sukanmandatory_p2 runat="server" id="koko_pelajar_mark_sukanmandatory_p2" />
</asp:Content>
