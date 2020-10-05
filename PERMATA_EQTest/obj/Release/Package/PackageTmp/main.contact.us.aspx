<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/main.Master" CodeBehind="main.contact.us.aspx.vb" Inherits="PERMATA_EQTest.main_contact_us" %>
<%@ Register src="commoncontrol/contact_us.ascx" tagname="contact_us" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:contact_us ID="contact_us1" runat="server" />
&nbsp;
</asp:Content>
