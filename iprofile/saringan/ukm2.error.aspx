<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/nocheck.Master"
    CodeBehind="ukm2.error.aspx.vb" Inherits="permatapintar.ukm2_error" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <%--<a href="#">
        <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
            height="35" border="0" class="logo" /></a>--%>
    <h1>Araken I-PROFILE</h1>

    <h2>
        <asp:Label ID="lbl02_p01" runat="server" Text="Error Page" CssClass="lblHeader"></asp:Label></h2>
    <table class="mytablemain">
        <tr>
            <td>
                <asp:Label ID="AppMsg" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;02
            </td>
        </tr>
        <tr>
            <td>
                Please contact system admin and provide print screen of this page.
            </td>
        </tr>
    </table>
    
</asp:Content>
