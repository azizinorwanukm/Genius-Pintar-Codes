<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/subadmin.Master" CodeBehind="subadmin.pusatujian.create.aspx.vb" Inherits="permatapintar.subadmin_pusatujian_create" %>
<%@ Register src="commoncontrol/pusatujian_create.ascx" tagname="pusatujian_create" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pusatujian_create ID="pusatujian_create1" runat="server" />
</asp:Content>