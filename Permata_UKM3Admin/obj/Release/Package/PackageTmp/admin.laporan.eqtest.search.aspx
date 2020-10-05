<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.laporan.eqtest.search.aspx.vb" Inherits="permatapintar.admin_laporan_eqtest_search" %>

<%@ Register Src="~/commoncontrol/eqtest_search.ascx" TagPrefix="uc1" TagName="eqtest_search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:eqtest_search runat="server" id="eqtest_search" />
</asp:Content>
