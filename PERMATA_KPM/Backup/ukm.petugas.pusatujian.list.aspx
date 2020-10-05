<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm.Master" CodeBehind="ukm.petugas.pusatujian.list.aspx.vb" Inherits="permatapintar.ukm_petugas_pusatujian_list" %>
<%@ Register src="commoncontrol/petugas_pusatujian_list.ascx" tagname="petugas_pusatujian_list" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:petugas_pusatujian_list ID="petugas_pusatujian_list1" runat="server" />
</asp:Content>
