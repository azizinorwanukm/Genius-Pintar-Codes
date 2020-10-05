<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pembantupengajar/main.Master" CodeBehind="laporan.mingguan.view.aspx.vb" Inherits="permatapintar.laporan_mingguan_view" %>
<%@ Register src="../commoncontrol/studentprofile_header.ascx" tagname="studentprofile_header" tagprefix="uc1" %>
<%@ Register src="../commoncontrol/laporan_mingguan_view.ascx" tagname="laporan_mingguan_view" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_header ID="studentprofile_header1" runat="server" />
    <uc2:laporan_mingguan_view ID="laporan_mingguan_view1" runat="server" />
&nbsp;
</asp:Content>
