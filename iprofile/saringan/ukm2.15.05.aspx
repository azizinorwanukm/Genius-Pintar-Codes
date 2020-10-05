<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/ukm2.main.Master" CodeBehind="ukm2.15.05.aspx.vb" Inherits="permatapintar.ukm2_15_05" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<a href="#">
        <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
            height="35" class="logo" /></a>--%>
    <h1>Araken I-PROFILE</h1>

    <h2>
        <asp:Label ID="ukm2_1500_header" runat="server" Text=""></asp:Label>[20/20]</h2>
    <asp:Label ID="lbl15_instruction" runat="server" Text="Sila baca pembayang dan tuliskan jawapan di ruangan yang disediakan."></asp:Label>
    <table class="mytablemain">
        <tr>
            <td>
                <asp:Label ID="lbl15_01" runat="server" Text="Ia adalah alat komunikasi tanpa wayar…</br>boleh digunakan di merata tempat …</br>dan mempunyai pelbagai bentuk serta saiz"
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
                <asp:Label ID="lbl15_02" runat="server" Text="Berkait dengan pendapatan dan perbelanjaan…</br>berskala kecil atau besar…</br>dan bermatlamatkan keuntungan"
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
                <asp:Label ID="lbl15_03" runat="server" Text="Ia adalah satu harapan… </br> yang merupakan perancangan masa hadapan...</br>dan dikonseptualisasi  "
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
                <asp:Label ID="lbl15_04" runat="server" Text="Ia merupakan satu proses demokrasi… </br> perlu calon dan penyokong… </br>menang berdasarkan bilangan majoriti."
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
