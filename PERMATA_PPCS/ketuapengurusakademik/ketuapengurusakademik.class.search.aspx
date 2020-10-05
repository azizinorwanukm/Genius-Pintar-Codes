<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ketuapengurusakademik/main.Master" CodeBehind="ketuapengurusakademik.class.search.aspx.vb" Inherits="permatapintar.ketuapengurusakademik_class_search" %>

<%@ Register Src="../commoncontrol/class_search_view.ascx" TagName="class_search_view" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:class_search_view ID="class_search_view1" runat="server" />
    &nbsp;
</asp:Content>