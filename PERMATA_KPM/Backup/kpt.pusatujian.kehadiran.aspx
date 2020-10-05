<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/kpt.Master" CodeBehind="kpt.pusatujian.kehadiran.aspx.vb" Inherits="permatapintar.kpt_pusatujian_kehadiran" %>

<%@ Register src="commoncontrol/pusatujian_list_kehadiran.ascx" tagname="pusatujian_list_kehadiran" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pusatujian_list_kehadiran ID="pusatujian_list_kehadiran1" runat="server" />
</asp:Content>
