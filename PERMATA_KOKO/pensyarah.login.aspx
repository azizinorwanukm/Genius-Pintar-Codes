<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.master" CodeBehind="pensyarah.login.aspx.vb" Inherits="permatapintar.pensyarah_login" %>

<%@ Register Src="commoncontrol/pensyarah_login.ascx" TagName="pensyarah_login" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pensyarah_login ID="pensyarah_login1" runat="server" />
    &nbsp;
</asp:Content>
