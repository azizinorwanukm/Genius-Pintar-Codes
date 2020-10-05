<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/ukm2.main.Master"
    CodeBehind="ukm2.14.04.aspx.vb" Inherits="permatapintar.ukm2_14_04" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <%--<a href="#">
        <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
            height="35" border="0" class="logo" /></a>--%>
    <h1>Araken I-PROFILE</h1>

    <h2>
        <asp:Label ID="ukm2_1400_header" runat="server" Text=""></asp:Label>[24/29]</h2>
    <asp:Label ID="lbl14_instruction" runat="server" Text="Baca soalan dengan teliti, kira jawapan secara mental (congak). Isi jawapan di ruangan yang disediakan. "></asp:Label>
    <table class="mytablemain">
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Start  " class="mybutton" /><br />
                <asp:Label ID="lbl14_01" runat="server" Text="Tiga puluh orang pelajar mendaftar kelas Bahasa Jepun.  Pada bulan berikutnya 11 orang menarik diri. Berapa orang pelajar yang tinggal dalam kelas tersebut?"
                    CssClass="lbl02" Visible="false"></asp:Label></td>
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
                <asp:Label ID="lbl14_02" runat="server" Text="Rosnah membeli dua buah buku latihan berharga RM1 setiap satu dan sekotak pensel warna  RM4.  Jika dia membayar dengan wang RM10, berapakah baki wang yang akan dikembalikan kepadanya?"
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
                <asp:Label ID="lbl14_03" runat="server" Text="Shahrir mempunyai  2 kali ganda wang yang dimiliki Basri.  Shahrir mempunyai RM17.  Berapa ringgit wang yang dimiliki oleh Basri?"
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
                <asp:Label ID="lbl14_04" runat="server" Text="Dua ratus empat puluh orang pelancong mendaftar untuk melancong ke Johor.  Sekiranya sebuah bas boleh membawa 40 orang, berapa buah bas diperlukan untuk membawa semua pelancong itu?"
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
                <asp:Label ID="lbl14_05" runat="server" Text="Ahmad membeli sehelai baju dengan harga diskaun satu pertiga kurang berbanding harga asal.  Jika Ahmad membayar RM40, berapakah harga asal baju tersebut?"
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
                <asp:Label ID="lbl14_06" runat="server" Text="Jarak perjalanan pulang ke kampung Kamal ialah 500 km. Selepas memandu selama 3 jam, dia berhenti rehat. Kemudian dia memandu semula selama 2 jam. Berapakah  purata kelajuan Kamal memandu?"
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
