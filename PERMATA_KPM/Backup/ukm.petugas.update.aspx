<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm.Master" CodeBehind="ukm.petugas.update.aspx.vb" Inherits="permatapintar.ukm_petugas_update" %>
<%@ Register src="commoncontrol/petugas_update.ascx" tagname="petugas_update" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:petugas_update ID="petugas_update1" runat="server" />
</asp:Content>
