<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengajar/main.Master" CodeBehind="laporan.keseluruhan.view.aspx.vb" Inherits="permatapintar.laporan_keseluruhan_view1" %>

<%@ Register Src="../commoncontrol/studentprofile_header.ascx" TagName="student"
    TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/laporan_keseluruhan_view.ascx" TagName="laporan_keseluruhan_view"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:student ID="student1" runat="server" />
    &nbsp;<uc2:laporan_keseluruhan_view ID="laporan_keseluruhan_view1" runat="server" />
    &nbsp;
</asp:Content>
