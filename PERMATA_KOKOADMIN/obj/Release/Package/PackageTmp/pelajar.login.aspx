<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.master" CodeBehind="pelajar.login.aspx.vb" Inherits="permatapintar.pelajar_login" %>

<%@ Register Src="commoncontrol/pelajar_login.ascx" TagName="pelajar_login" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pelajar_login ID="pelajar_login1" runat="server" />
    &nbsp;
</asp:Content>
