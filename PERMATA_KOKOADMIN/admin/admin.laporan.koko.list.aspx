<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.laporan.koko.list.aspx.vb" Inherits="permatapintar.admin_laporan_koko_list" %>

<%@ Register src="../commoncontrol/laporan_koko_list.ascx" tagname="laporan_koko_list" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:laporan_koko_list ID="laporan_koko_list1" runat="server" />
&nbsp;
</asp:Content>
