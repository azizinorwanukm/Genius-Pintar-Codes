<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pembantupelajar/main.Master" CodeBehind="laporan.harian.class.select.aspx.vb" Inherits="permatapintar.laporan_harian_class_select" %>
<%@ Register src="../commoncontrol/ppcs_class_select_session.ascx" tagname="ppcs_class_select_session" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ppcs_class_select_session ID="ppcs_class_select_session1" runat="server" />
</asp:Content>
