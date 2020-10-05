<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/ukm2.main.Master" CodeBehind="ukm2.12.00.aspx.vb" Inherits="permatapintar.ukm2_12_00" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<a href="#">
        <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
            height="35" class="logo" /></a>--%>
    <h1>Araken I-PROFILE</h1>

    <h2><asp:Label ID="ukm2_1200_header" runat="server" Text=""></asp:Label></h2>
    <p class="moreline">
        &nbsp;</p>
    <p class="contant1">
        <asp:Label ID="lbl12_instruction_sample" runat="server" Text="Pada laman seterusnya, sila klik pada semua gambar buah-buahan atau binatang sahaja. Baca dengan teliti arahan yang diberikan."></asp:Label>
    </p>
    <p class="moreline">
        &nbsp;</p>
    <asp:Button ID="btnNext" runat="server" Text="Mula >>" CssClass="mybutton" />

    <asp:Label ID="lblLoadStart" runat="server" Text="0"></asp:Label>


</asp:Content>
