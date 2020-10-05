<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/ukm2.main.Master"
    CodeBehind="ukm2.14.05.aspx.vb" Inherits="permatapintar.ukm2_14_05" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <%--<a href="#">
        <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
            height="35" border="0" class="logo" /></a>--%>
    <h1>Araken I-PROFILE</h1>

    <h2>
        <asp:Label ID="ukm2_1400_header" runat="server" Text=""></asp:Label>[29/29]</h2>
    <asp:Label ID="lbl14_instruction" runat="server" Text="Baca soalan dengan teliti, kira jawapan secara mental (congak). Isi jawapan di ruangan yang disediakan. "></asp:Label>
    <table class="mytablemain">
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Start  " class="mybutton" /><br />
                <asp:Label ID="lbl14_01" runat="server" Text="Harga tambang murah penerbangan dari LCCT, Kuala Lumpur ke Bali, Indonesia ialah RM135 setelah diberi diskaun sebanyak 70%. Berapakah harga asal tambang tersebut?"
                    CssClass="lbl02" Visible="false"></asp:Label>
            </td>
           <td style="vertical-align:bottom;">
                <asp:TextBox ID="TextBox1" runat="server" Width="200px" CssClass="textbox14"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button2" runat="server" Text="Start  " class="mybutton" /><br />
                <asp:Label ID="lbl14_02" runat="server" Text="Harga minyak telah diturunkan sebanyak 5 kali sejak Jun 2008.  Jika Harga asal RM2.70 dan kali terakhir minyak diturunkan kepada RM1.90 berapakah purata penurunan minyak?"
                    CssClass="lbl02" Visible="false"></asp:Label>
            </td>
            <td style="vertical-align:bottom;">
                <asp:TextBox ID="TextBox2" runat="server" Width="200px" CssClass="textbox14"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button3" runat="server" Text="Start  " class="mybutton" /><br />
                <asp:Label ID="lbl14_03" runat="server" Text="Dua orang  dapat menyiapkan 8 buah bakul rotan sehari.  Berapa orang pekerja yang diperlukan untuk menyiapkan tempahan 60 bakul dalam sehari?"
                    CssClass="lbl02" Visible="false"></asp:Label>
            </td>
           <td style="vertical-align:bottom;">
                <asp:TextBox ID="TextBox3" runat="server" Width="200px" CssClass="textbox14"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button4" runat="server" Text="Start  " class="mybutton" /><br />
                <asp:Label ID="lbl14_04" runat="server" Text="Perjalanan Zamri menaiki KLIA Transit dari KLIA ke Putra Sentral ialah 1 jam 45 minit.  Lim tinggal 50 km dari Putra Sentral.  Dia memandu 100km/jam.  Jika Zamri bertolak dari KLIA pada pukul 3.45 petang, pukul berapa Lim perlu bertolak dari rumahnya supaya sampai 15 minit lebih awal daripada Zamri?"
                    CssClass="lbl02" Visible="false"></asp:Label>
            </td>
            <td style="vertical-align:bottom;">
                <asp:TextBox ID="TextBox4" runat="server" Width="200px" CssClass="textbox14"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button5" runat="server" Text="Start  " class="mybutton" /><br />
                <asp:Label ID="lbl14_05" runat="server" Text="Bas ekspres bertolak 1 jam awal daripada teksi.  Bas ekspres  dipandu dengan kelajuan purata 80km/jam dan teksi 100km/jam.  Jika kedua-duanya mempunyai destinasi yang sama, berapa jauh bas ekspres mendahului teksi selepas 3 jam perjalanan?"
                    CssClass="lbl02" Visible="false"></asp:Label>
            </td>
           <td style="vertical-align:bottom;">
                <asp:TextBox ID="TextBox5" runat="server" Width="200px" CssClass="textbox14"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnLangkau" runat="server" Text="Langkau Modul" CssClass="mybutton" /><img
                    src="images/white-space.png" width="400px" alt="" />
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
