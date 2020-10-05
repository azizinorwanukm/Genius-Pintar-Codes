<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengurusakademik/main.Master" CodeBehind="activity.list.aspx.vb" Inherits="permatapintar.activity_list1" %>
<%@ Register src="../commoncontrol/activity.list.ascx" tagname="activity" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:activity ID="activity1" runat="server" />
</asp:Content>
