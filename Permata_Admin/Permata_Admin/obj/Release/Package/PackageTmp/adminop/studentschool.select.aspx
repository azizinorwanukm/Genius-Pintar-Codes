<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="studentschool.select.aspx.vb" Inherits="permatapintar.studentschool_select1" %>

<%@ Register Src="../commoncontrol/studentschool_select.ascx" TagName="studentschool_select" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentschool_select ID="studentschool_select1" runat="server" />
    &nbsp;
</asp:Content>
