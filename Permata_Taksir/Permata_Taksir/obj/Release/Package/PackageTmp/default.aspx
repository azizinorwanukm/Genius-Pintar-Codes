<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master" CodeBehind="default.aspx.vb" Inherits="UKM3._default" %>

<%@ Register Src="~/Control/login_trail.ascx" TagPrefix="uc1" TagName="login_trail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:login_trail runat="server" id="login_trail" />
    &nbsp;
</asp:Content>
