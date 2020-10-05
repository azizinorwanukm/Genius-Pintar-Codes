<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/ukm2.main.Master" CodeBehind="ukm2.10.02.aspx.vb" Inherits="permatapintar.ukm2_10_02" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<a href="#">
        <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
            height="35" border="0" class="logo" /></a>--%>
    <h1>Araken I-PROFILE</h1>

    <h2>
        <asp:Label ID="ukm2_1000_header" runat="server" Text=""></asp:Label>[14/62]</h2>
    <asp:Label ID="lbl10_instruction" runat="server" Text="Lihat simbol di sebelah kiri (berwarna merah) dan pilih ADA sekiranya simbol itu terdapat pada sebelah kanan atau TIADA sekiranya tiada persamaan."></asp:Label>
    <table class="mytablebottom" border="0px">
        <tr>
            <td class="mytd_10text">&#8745;
            </td>
            <td class="mytd_10text">&#7908;
            </td>
            <td class="mytd_10blank">&nbsp;
            </td>
            <td class="mytd_codingtext">»
            </td>
            <td class="mytd_codingtext">¶
            </td>
            <td class="mytd_codingtext">=
            </td>
            <td class="mytd_codingtext">«
            </td>
            <td class="mytd_codingtext">~
            </td>
            <td class="mytd_10blank">&nbsp;
            </td>
            <td class="mytd_10answer">
                <asp:RadioButton ID="RadioButton1" runat="server" Text="ADA" GroupName="Q01" />
            </td>
            <td class="mytd_10answer">
                <asp:RadioButton ID="RadioButton2" runat="server" Text="TIADA" GroupName="Q01" />
            </td>
        </tr>

        <tr>
            <td class="mytd_10text">±
            </td>
            <td class="mytd_10text">Ø
            </td>
            <td class="mytd_10blank">&nbsp;
            </td>
            <td class="mytd_codingtext">&#926;
            </td>
            <td class="mytd_codingtext">&#920;
            </td>
            <td class="mytd_codingtext">&#1069;
            </td>
            <td class="mytd_codingtext">&#1200;
            </td>
            <td class="mytd_codingtext">&#1256;
            </td>
            <td class="mytd_10blank">&nbsp;
            </td>
            <td class="mytd_10answer">
                <asp:RadioButton ID="RadioButton3" runat="server" Text="ADA" GroupName="Q02" />
            </td>
            <td class="mytd_10answer">
                <asp:RadioButton ID="RadioButton4" runat="server" Text="TIADA" GroupName="Q02" />
            </td>
        </tr>


        <tr>
            <td class="mytd_10text">&#8356;
            </td>
            <td class="mytd_10text">&#8706;
            </td>
            <td class="mytd_10blank">&nbsp;
            </td>
            <td class="mytd_codingtext">&#8355;
            </td>
            <td class="mytd_codingtext">&#8486;
            </td>
            <td class="mytd_codingtext">&#8494;
            </td>
            <td class="mytd_codingtext">&#1047;
            </td>
            <td class="mytd_codingtext">&#8706;
            </td>
            <td class="mytd_10blank">&nbsp;
            </td>
            <td class="mytd_10answer">
                <asp:RadioButton ID="RadioButton5" runat="server" Text="ADA" GroupName="Q03" />
            </td>
            <td class="mytd_10answer">
                <asp:RadioButton ID="RadioButton6" runat="server" Text="TIADA" GroupName="Q03" />
            </td>
        </tr>

        <tr>
            <td class="mytd_10text">±
            </td>
            <td class="mytd_10text">»
            </td>
            <td class="mytd_10blank">&nbsp;
            </td>
            <td class="mytd_codingtext">#
            </td>
            <td class="mytd_codingtext">+
            </td>
            <td class="mytd_codingtext"><
            </td>
            <td class="mytd_codingtext">&#915;
            </td>
            <td class="mytd_codingtext">&#916;
            </td>
            <td class="mytd_10blank">&nbsp;
            </td>
            <td class="mytd_10answer">
                <asp:RadioButton ID="RadioButton7" runat="server" Text="ADA" GroupName="Q04" />
            </td>
            <td class="mytd_10answer">
                <asp:RadioButton ID="RadioButton8" runat="server" Text="TIADA" GroupName="Q04" />
            </td>
        </tr>


        <tr>
            <td class="mytd_10text">&#1256;
            </td>
            <td class="mytd_10text">&#8467;
            </td>
            <td class="mytd_10blank">&nbsp;
            </td>
            <td class="mytd_codingtext">&#1240;
            </td>
            <td class="mytd_codingtext">&#8364;
            </td>
            <td class="mytd_codingtext">&#8355;
            </td>
            <td class="mytd_codingtext">&#1256;
            </td>
            <td class="mytd_codingtext">&#8356;
            </td>
            <td class="mytd_10blank">&nbsp;
            </td>
            <td class="mytd_10answer">
                <asp:RadioButton ID="RadioButton9" runat="server" Text="ADA" GroupName="Q05" />
            </td>
            <td class="mytd_10answer">
                <asp:RadioButton ID="RadioButton10" runat="server" Text="TIADA" GroupName="Q05" />
            </td>
        </tr>

        <tr>
            <td class="mytd_10text">&#8224;
            </td>
            <td class="mytd_10text">&#8225;
            </td>
            <td class="mytd_10blank">&nbsp;
            </td>
            <td class="mytd_codingtext">&#8706;
            </td>
            <td class="mytd_codingtext">&#9532;
            </td>
            <td class="mytd_codingtext">&#9566;
            </td>
            <td class="mytd_codingtext">&#9574;
            </td>
            <td class="mytd_codingtext">=
            </td>
            <td class="mytd_10blank">&nbsp;
            </td>
            <td class="mytd_10answer">
                <asp:RadioButton ID="RadioButton11" runat="server" Text="ADA" GroupName="Q06" />
            </td>
            <td class="mytd_10answer">
                <asp:RadioButton ID="RadioButton12" runat="server" Text="TIADA" GroupName="Q06" />
            </td>
        </tr>

        <tr>
            <td class="mytd_10text">#
            </td>
            <td class="mytd_10text">~
            </td>
            <td class="mytd_10blank">&nbsp;
            </td>
            <td class="mytd_codingtext">±
            </td>
            <td class="mytd_codingtext">&#1028;
            </td>
            <td class="mytd_codingtext">&#8249;
            </td>
            <td class="mytd_codingtext">&#8596;
            </td>
            <td class="mytd_codingtext">&#8804;
            </td>
            <td class="mytd_10blank">&nbsp;
            </td>
            <td class="mytd_10answer">
                <asp:RadioButton ID="RadioButton13" runat="server" Text="ADA" GroupName="Q07" />
            </td>
            <td class="mytd_10answer">
                <asp:RadioButton ID="RadioButton14" runat="server" Text="TIADA" GroupName="Q07" />
            </td>
        </tr>

        <tr>
            <td colspan="12">
                <asp:Button ID="btnLangkau" runat="server" Text="Langkau Modul" CssClass="mybutton" /><img src="images/white-space.png" width="400px" alt="" />
                <asp:Button ID="btnNext" runat="server" Text="Seterusnya >>" CssClass="mybutton" />
            </td>
        </tr>
        <tr>
            <td colspan="12">
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>

    </table>
    <asp:Label ID="lblLoadStart" runat="server" Text="0"></asp:Label>

</asp:Content>
