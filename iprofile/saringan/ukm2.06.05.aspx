<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/ukm2.main.Master" CodeBehind="ukm2.06.05.aspx.vb" Inherits="permatapintar.ukm2_06_05" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <%--<a href="#">
        <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
            height="35" border="0" class="logo" /></a>--%>
    <h1>Araken I-PROFILE</h1>

    <h2>
        <asp:Label ID="ukm2_0600_header" runat="server" Text=""></asp:Label>[20/30]</h2>
    <asp:Label ID="lbl06_instruction" runat="server" Text="Taipkan maksud bagi perkataan-perkataan berikut dalam ruangan yang disediakan. Ini adalah contoh dan latihan semata-mata, tiada permarkahan diambil."></asp:Label>
    <table class="mytablemain">
        <tr>
            <td>
                <asp:Label ID="lbl06_17" runat="server" Text="SEJARAH" CssClass="lbl02"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txt06_01" runat="server" TextMode="MultiLine" Rows="3" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl06_18" runat="server" Text="ALIH" CssClass="lbl02"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txt06_02" runat="server" TextMode="MultiLine" Rows="3" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl06_19" runat="server" Text="SAING" CssClass="lbl02"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txt06_03" runat="server" TextMode="MultiLine" Rows="3" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl06_20" runat="server" Text="WAWASAN" CssClass="lbl02"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txt06_04" runat="server" TextMode="MultiLine" Rows="3" Width="90%"></asp:TextBox>
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
