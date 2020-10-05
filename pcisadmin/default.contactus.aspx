<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master" CodeBehind="default.contactus.aspx.vb" Inherits="araken.pcisadmin.default_contactus" %>

<%@ Register Src="commoncontrol/our_address.ascx" TagName="our_address" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:our_address ID="our_address1" runat="server" />
    &nbsp;
</asp:Content>
