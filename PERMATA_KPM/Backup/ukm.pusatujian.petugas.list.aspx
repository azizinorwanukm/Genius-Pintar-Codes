<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm.Master"
    CodeBehind="ukm.pusatujian.petugas.list.aspx.vb" Inherits="permatapintar.ukm_pusatujian_petugas_list" %>

<%@ Register src="commoncontrol/pusatujian_petugas_list.ascx" tagname="pusatujian_petugas_list" tagprefix="uc1" %>
<%@ Register src="commoncontrol/pusatujian_view.ascx" tagname="pusatujian_view" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc2:pusatujian_view ID="pusatujian_view1" runat="server" />&nbsp; 
    <uc1:pusatujian_petugas_list ID="pusatujian_petugas_list1" runat="server" />
</asp:Content>
