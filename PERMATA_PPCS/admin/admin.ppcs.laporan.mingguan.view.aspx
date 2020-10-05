<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="admin.ppcs.laporan.mingguan.view.aspx.vb" Inherits="permatapintar.admin_ppcs_laporan_mingguan_view" %>

<%@ Register Src="../commoncontrol/studentprofile_header.ascx" TagName="studentprofile_header"
    TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/laporan_mingguan_view.ascx" TagName="laporan_mingguan_view"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_header ID="studentprofile_header1" runat="server" />
    &nbsp;
    <uc2:laporan_mingguan_view ID="laporan_mingguan_view1" runat="server" />
    &nbsp;
</asp:Content>
