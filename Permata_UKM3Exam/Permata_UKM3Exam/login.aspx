<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/login.Master" CodeBehind="login.aspx.vb" Inherits="UKM3.login" %>

<%@ Register Src="Control/login.ascx" TagName="login" TagPrefix="uc1" %>

    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:login ID="login1" runat="server" />
    &nbsp;
</asp:Content>
