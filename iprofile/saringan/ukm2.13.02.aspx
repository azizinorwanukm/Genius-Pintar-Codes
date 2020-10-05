<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/ukm2.main.Master" CodeBehind="ukm2.13.02.aspx.vb" Inherits="permatapintar.ukm2_13_02" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <%--<a href="#">
        <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
            height="35" border="0" class="logo" /></a>--%>
    <h1>Araken I-PROFILE</h1>


    <h2>
        <asp:Label ID="ukm2_1300_header" runat="server" Text=""></asp:Label>[08/24]</h2>
    <asp:Label ID="lbl13_instruction" runat="server" Text="Baca soalan berikut dengan teliti dan taipkan jawapan anda dalam ruangan yang disediakan."></asp:Label>
    <table class="mytablemain">
        <tr>
            <td>
                <asp:Label ID="lbl13_05" runat="server" Text="Berapa meter bersamaan dengan satu kilometer?"
                    CssClass="lbl02"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txt13_01" runat="server" TextMode="MultiLine" Rows="3" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl13_06" runat="server" Text="Namakan satu bulan yang mengandungi 31 hari."
                    CssClass="lbl02"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txt13_02" runat="server" TextMode="MultiLine" Rows="3" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl13_07" runat="server" Text="Namakan bahan api semula jadi."
                    CssClass="lbl02"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txt13_03" runat="server" TextMode="MultiLine" Rows="3" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl13_08" runat="server" Text="Berapakah jumlah benua di dunia?"
                    CssClass="lbl02"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txt13_04" runat="server" TextMode="MultiLine" Rows="3" Width="90%"></asp:TextBox>
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
