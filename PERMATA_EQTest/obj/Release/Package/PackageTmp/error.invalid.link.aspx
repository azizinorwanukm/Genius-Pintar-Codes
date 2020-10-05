<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master"
    CodeBehind="error.invalid.link.aspx.vb" Inherits="PERMATA_EQTest.error_invalid_link" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>
        Sorry. You have an invalid link or intermittent network connection. Please try again
        later or go back and refresh.</h2>
    <p>
        You may check your email and click the link provided.</p>
    <p>
        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label></p>
</asp:Content>
