<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/question.Master" CodeBehind="logged.aspx.vb" Inherits="UKM3.logged1" %>

<%@ Register Src="Control/logged.ascx" TagName="logged" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:logged ID="login1" runat="server" />
    &nbsp;
</asp:Content>
