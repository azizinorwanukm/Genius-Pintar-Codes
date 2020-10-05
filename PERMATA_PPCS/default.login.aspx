<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master"
    CodeBehind="default.login.aspx.vb" Inherits="permatapintar.default_login" %>

<%@ Register src="commoncontrol/ukm2.ppcs.login.control.ascx" tagname="ukm2" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <uc1:ukm2 ID="ukm21" runat="server" />
    
</asp:Content>
