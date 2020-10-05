<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/ukm2.main.Master"
    CodeBehind="ukm2.14.02.aspx.vb" Inherits="permatapintar.ukm2_14_02" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <%--<a href="#">
        <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
            height="35" border="0" class="logo" /></a>--%>
    <h1>Araken I-PROFILE</h1>

    <h2>
        <asp:Label ID="ukm2_1400_header" runat="server" Text=""></asp:Label>[12/29]</h2>
    <asp:Label ID="lbl14_instruction" runat="server" Text="Baca soalan dengan teliti, kira jawapan secara mental (congak). Isi jawapan di ruangan yang disediakan. "></asp:Label>
    <table class="mytablemain">
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Start  " class="mybutton" /><br />
                <asp:Label ID="lbl14_01" runat="server" Text="Jika anda ada 10 keping roti dan memakan 3 keping, berapa keping roti yang tinggal?"
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
                <asp:Label ID="lbl14_02" runat="server" Text="Tiga orang pelajar baru telah ditempatkan ke dalam kelas yang telah mempunyai 12 orang pelajar.  Berapakah jumlah pelajar dalam kelas tersebut?"
                    CssClass="lbl02" Visible="false"></asp:Label></td>
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
                <asp:Label ID="lbl14_03" runat="server" Text="Mansor ada enam keping penanda buku dan membeli enam keping lagi.  Berapakah jumlah penanda buku yang ada padanya?"
                    CssClass="lbl02" Visible="false"></asp:Label></td>
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
                <asp:Label ID="lbl14_04" runat="server" Text="Ah Meng mengumpul 10 setem pada hari Sabtu dan 15 setem pada hari Ahad.  Berapakah jumlah setem yang dikumpulkannya?"
                    CssClass="lbl02" Visible="false"></asp:Label></td>
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
                <asp:Label ID="lbl14_05" runat="server" Text="Terdapat tiga ekor kambing di sebuah padang ragut. Dua ekor kambing lain datang. Kemudian dua ekor pula meninggalkan padang ragut.  Berapa ekor kambing yang tinggal di padang ragut?"
                    CssClass="lbl02" Visible="false"></asp:Label></td>
            <td style="vertical-align:bottom;">
                <asp:TextBox ID="TextBox5" runat="server" Width="200px" CssClass="textbox14"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button6" runat="server" Text="Start  " class="mybutton" /><br />
                <asp:Label ID="lbl14_06" runat="server" Text="Kuldip membeli empat biji limau dari sebuah gerai buah-buahan dan dua biji lagi dari gerai yang lain.  Sampai di rumah, ibunya memberi tiga biji buah limau lagi.  Berapa banyak buah limau yang dimilikinya?"
                    CssClass="lbl02" Visible="false"></asp:Label></td>
           <td style="vertical-align:bottom;">
                <asp:TextBox ID="TextBox6" runat="server" Width="200px" CssClass="textbox14"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;
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
