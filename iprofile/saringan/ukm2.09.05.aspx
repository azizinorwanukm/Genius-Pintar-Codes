﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/ukm2.main.Master" CodeBehind="ukm2.09.05.aspx.vb" Inherits="permatapintar.ukm2_09_05" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<a href="#">
        <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
            height="35" border="0" class="logo" /></a>--%>
    <h1>Araken I-PROFILE</h1>

    <h2>
        <asp:Label ID="ukm2_0900_header" runat="server" Text=""></asp:Label>[19/19]</h2>
    <asp:Label ID="lbl09_instruction" runat="server" Text="Baca soalan berikut dengan teliti.  Huraikan jawapan anda dalam ruangan yang disediakan."></asp:Label>
    <table class="mytablemain">
        <tr>
            <td>
                <asp:Label ID="lbl09_17" runat="server" Text="Mengapakah perlu disediakan kemudahan untuk Orang Kurang Upaya (OKU) di sekolah?"
                    CssClass="lbl02"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txt09_01" runat="server" TextMode="MultiLine" Rows="3" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl09_18" runat="server" Text="Apakah kesan negatif daripada kemajuan Teknologi Maklumat dan Komunikasi (ICT)?"
                    CssClass="lbl02"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txt09_02" runat="server" TextMode="MultiLine" Rows="3" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl09_19" runat="server" Text="Mengapa kita perlu menghormati pendapat orang lain?"
                    CssClass="lbl02"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txt09_03" runat="server" TextMode="MultiLine" Rows="3" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnLangkau" runat="server" Text="Langkau Modul" CssClass="mybutton" /><img src="images/white-space.png" width="400px" alt="" />
                <asp:Button ID="btnNext" runat="server" Text="Seterusnya >>" CssClass="mybutton" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblLoadStart" runat="server" Text="0"></asp:Label>


</asp:Content>
