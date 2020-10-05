<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengajar/main.Master" CodeBehind="laporan.mingguan.aspx.vb" Inherits="permatapintar.laporan_mingguan" %>


<%@ Register Src="../commoncontrol/ppcs_list_classid_session.ascx" TagName="ppcs_list_classid_session"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ppcs_list_classid_session ID="ppcs_list_classid_session1" runat="server" />
    &nbsp;
</asp:Content>
