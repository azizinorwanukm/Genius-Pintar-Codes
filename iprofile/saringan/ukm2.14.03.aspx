<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/ukm2.main.Master"
    CodeBehind="ukm2.14.03.aspx.vb" Inherits="permatapintar.ukm2_14_03" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <%--<a href="#">
        <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
            height="35" border="0" class="logo" /></a>--%>
    <h1>Araken I-PROFILE</h1>


    <h2>
        <asp:Label ID="ukm2_1400_header" runat="server" Text=""></asp:Label>[18/29]</h2>
    <asp:Label ID="lbl14_instruction" runat="server" Text="Baca soalan dengan teliti, kira jawapan secara mental (congak). Isi jawapan di ruangan yang disediakan. "></asp:Label>
    <table class="mytablemain">
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Start  " class="mybutton" /><br />
                <asp:Label ID="lbl14_01" runat="server" Text="Pak Ali ada 12 biji durian. Lima biji dijual kepada pelanggan.  Berapa biji durian yang tinggal?"
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
                <asp:Label ID="lbl14_02" runat="server" Text="Dalam sebuah bas terdapat lapan orang penumpang.  Apabila tiba di perhentian, empat orang turun dan dua orang penumpang naik.  Berapa orang penumpang yang berada dalam bas sekarang?"
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
                <asp:Label ID="lbl14_03" runat="server" Text="Dalam satu permainan, Latifah mendapat 17 mata pada pusingan pertama dan 15 mata dalam pusingan kedua.  Berapakah jumlah mata yang diperoleh Latifah?"
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
                <asp:Label ID="lbl14_04" runat="server" Text="Jika anda membeli tiga keping poskad pada harga 50 sen setiap satu, berapakah wang baki yang akan dikembalikan daripada wang RM5?"
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
                <asp:Label ID="lbl14_05" runat="server" Text="Faris mempunyai RM30 dan telah membelanjakan separuh daripadanya.  Jika sebuah majalah berharga RM5, berapa banyak majalah boleh dibeli sekiranya Faris menggunakan semua baki yang dia ada?"
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
                <asp:Label ID="lbl14_06" runat="server" Text="Terdapat lapan acara pertandingan dalam satu pesta makanan.  Jika setiap pertandingan memerlukan tiga buah pinggan,  berapa banyak pinggan yang diperlukan sekiranya pertandingan dijalankan serentak?"
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
