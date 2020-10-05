<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm.Master" CodeBehind="ukm.petugas.list.aspx.vb" Inherits="permatapintar.ukm_petugas_list" %>
<%@ Register src="commoncontrol/petugas_list.ascx" tagname="petugas_list" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:petugas_list ID="petugas_list1" runat="server" />
</asp:Content>
