<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="Site1.Master" CodeBehind="ukm2.error.aspx.vb" Inherits="permatapintar.ukm2_error1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <%--<a href="#">
        <img src="saringan/images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
            height="35" border="0" class="logo" /></a>--%>
    <h1>Araken I-PROFILE</h1>

    <h2>
        <asp:Label ID="lbl02_p01" runat="server" Text="Error Page" CssClass="lblHeader"></asp:Label></h2>
    <table class="mytablemain" style="background-color:White;">
        <tr>
            <td>
                <asp:Label ID="AppMsg" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;01
            </td>
        </tr>
        <tr>
            <td>
                Your browser session end due to network problem. Please re-login. Click on Home link below.
            </td>
        </tr>
    </table>
    

</asp:Content>
