<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/kpm.Master" CodeBehind="kpm.pusatujian.list.aspx.vb" Inherits="permatapintar.kpm_pusatujian_list" %>
<%@ Register src="commoncontrol/pusatujian_list.ascx" tagname="pusatujian_list" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pusatujian_list ID="pusatujian_list1" runat="server" />
</asp:Content>
