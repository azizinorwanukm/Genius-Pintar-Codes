﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/ukm2.main.Master" CodeBehind="ukm2.13.00.aspx.vb" Inherits="permatapintar.ukm2_13_00" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<a href="#">
        <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
            height="35" border="0" class="logo" /></a>--%>
    <h1>Araken I-PROFILE</h1>


    <h2>
        <asp:Label ID="lblSample" runat="server" Text=""></asp:Label></h2>
    <h2>
        <asp:Label ID="ukm2_1300_header" runat="server" Text=""></asp:Label></h2>
    <p>&nbsp;</p>
    <asp:Label ID="lbl13_instruction_sample" runat="server" Text="Baca soalan berikut dengan teliti dan taipkan jawapan anda dalam ruangan yang disediakan. Ini adalah contoh dan latihan semata-mata, tiada permarkahan diambil."></asp:Label>
    <table class="mytablemain">
        <tr>
            <td>
                <asp:Label ID="lbl13_00" runat="server" Text="Berapa bulankah dalam setahun?"
                    CssClass="lbl02"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txt13_00" runat="server" TextMode="MultiLine" Rows="3" Width="90%" Text=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="ukm2_1300_01" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnNext" runat="server" Text="Mula >>" CssClass="mybutton" />
                <asp:Label ID="ukm2_1300_02" runat="server" Text="Tekan [Mula >>] jika anda sudah bersedia dan faham apa yang perlu dilakukan."></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblLoadStart" runat="server" Text="0"></asp:Label>


</asp:Content>
