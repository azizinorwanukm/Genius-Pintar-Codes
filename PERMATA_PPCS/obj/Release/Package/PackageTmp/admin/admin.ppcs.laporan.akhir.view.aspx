<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="admin.ppcs.laporan.akhir.view.aspx.vb" Inherits="permatapintar.admin_ppcs_laporan_akhir_view" %>

<%@ Register Src="../commoncontrol/laporan_akhir_view.ascx" TagName="laporan_akhir_view"
    TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/studentprofile_header.ascx" TagName="studentprofile_header"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc2:studentprofile_header ID="studentprofile_header1" runat="server" />
    &nbsp;
    <uc1:laporan_akhir_view ID="laporan_akhir_view1" runat="server" />
</asp:Content>
