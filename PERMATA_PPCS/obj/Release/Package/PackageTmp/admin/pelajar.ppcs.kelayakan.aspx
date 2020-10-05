<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="pelajar.ppcs.kelayakan.aspx.vb" Inherits="permatapintar.pelajar_ppcs_kelayakan" %>
<%@ Register src="../commoncontrol/ppcs_kelayakan_select.ascx" tagname="ppcs_kelayakan_select" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ppcs_kelayakan_select ID="ppcs_kelayakan_select1" runat="server" />
</asp:Content>
