<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/ukm2.main.Master"
    CodeBehind="ukm2.10.00.aspx.vb" Inherits="permatapintar.ukm2_10_00" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <%--<a href="#">
        <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
            height="35" class="logo" /></a>--%>
    <h1>Araken I-PROFILE</h1>


    <h2><asp:Label ID="lblSample" runat="server" Text=""></asp:Label></h2>
    <h2><asp:Label ID="ukm2_1000_header" runat="server" Text=""></asp:Label></h2>
    <p>&nbsp;</p>
    <asp:Label ID="lbl10_instruction_sample" runat="server" Text="Lihat simbol di sebelah kiri (berwarna merah) dan pilih ADA sekiranya simbol itu terdapat pada sebelah kanan atau TIADA sekiranya tiada persamaan. Ini adalah contoh dan latihan semata-mata, tiada permarkahan diambil."></asp:Label>
    <table class="mytablebottom">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="mytd_10text">
                &#8719;
            </td>
            <td class="mytd_10text">
                &#8804;
            </td>
            <td class="mytd_10blank">
                &nbsp;
            </td>
            <td class="mytd_codingtext">
                &#8596;
            </td>
            <td class="mytd_codingtext">
                &#8719;
            </td>
            <td class="mytd_codingtext">
                &#8776;
            </td>
            <td class="mytd_codingtext">
                &#8805;
            </td>
            <td class="mytd_codingtext">
                &#9568;
            </td>
            <td class="mytd_10blank">
                &nbsp;
            </td>
            <td class="mytd_10answer">
                <asp:RadioButton ID="RadioButton1" runat="server" Text="ADA" GroupName="Q01" />
            </td>
            <td class="mytd_10answer">
                <asp:RadioButton ID="RadioButton2" runat="server" Text="TIADA" GroupName="Q01" />
            </td>
        </tr>
        <tr><td colspan="9">&nbsp;</td></tr>
        <tr>
            <td colspan="9">
            <asp:Label ID="ukm2_1000_01" runat="server" Text="Jawapan bagi soalan ini ialah ADA."></asp:Label>
                
            </td>
        </tr>

        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="9">
                <asp:Button ID="btnNext" runat="server" Text="Mula >>" CssClass="mybutton" />
                <asp:Label ID="ukm2_1000_02" runat="server" Text="Tekan [Mula >>] jika anda sudah bersedia dan faham apa yang perlu dilakukan."></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblLoadStart" runat="server" Text="0"></asp:Label>

</asp:Content>
