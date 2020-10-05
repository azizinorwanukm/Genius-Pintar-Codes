<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/ukm2.main.Master"
    CodeBehind="ukm2.15.01.aspx.vb" Inherits="permatapintar.ukm2_15_01" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<a href="#">
        <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
            height="35" border="0" class="logo" /></a>--%>
    <h1>Araken I-PROFILE</h1>

    <h2>
        <asp:Label ID="ukm2_1500_header" runat="server" Text=""></asp:Label>[04/20]</h2>
    <asp:Label ID="lbl15_instruction" runat="server" Text="Sila baca pembayang dan tuliskan jawapan di ruangan yang disediakan."></asp:Label>
    <table class="mytablemain">
        <tr>
            <td>
                <asp:Label ID="lbl15_01" runat="server" Text="Ia ada pintu, ada rak dan boleh menyimpan pakaian."
                    CssClass="lbl02"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" Width="350px" CssClass="textbox14" TextMode="MultiLine"
                    Rows="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl15_02" runat="server" Text="Ia adalah sesuatu yang digunakan untuk melindungi kaki anda"
                    CssClass="lbl02"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server" Width="350px" CssClass="textbox14" TextMode="MultiLine"
                    Rows="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl15_03" runat="server" Text="Ia berada di langit… </br> dan akan kelihatan bila hari hendak hujan"
                    CssClass="lbl02"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox3" runat="server" Width="350px" CssClass="textbox14" TextMode="MultiLine"
                    Rows="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl15_04" runat="server" Text="Ia mempunyai beberapa bilah… </br> dan tergantung di siling rumah"
                    CssClass="lbl02"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox4" runat="server" Width="350px" CssClass="textbox14" TextMode="MultiLine"
                    Rows="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnLangkau" runat="server" Text="Langkau Modul" CssClass="mybutton" /><img src="images/white-space.png" width="400px" alt="" />
                <asp:Button ID="btnNext" runat="server" Text="Seterusnya >>" CssClass="mybutton" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>

    </table>
    <asp:Label ID="lblLoadStart" runat="server" Text="0"></asp:Label>

</asp:Content>
