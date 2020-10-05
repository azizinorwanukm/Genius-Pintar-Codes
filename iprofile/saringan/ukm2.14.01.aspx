<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/ukm2.main.Master"
    CodeBehind="ukm2.14.01.aspx.vb" Inherits="permatapintar.ukm2_14_01" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <%--<a href="#">
        <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
            height="35" border="0" class="logo" /></a>--%>
    <h1>Araken I-PROFILE</h1>

    <h2>
        <asp:Label ID="ukm2_1400_header" runat="server" Text=""></asp:Label>[06/29]</h2>
    <asp:Label ID="lbl14_instruction" runat="server" Text="Baca soalan dengan teliti, kira jawapan secara mental (congak). Isi jawapan di ruangan yang disediakan. 
    Sekiranya jawapan anda salah sebanyak empat kali berturut-turut, anda akan dibawa ke modul seterusnya."></asp:Label>
    <table class="mytablemain">
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Start  " class="mybutton" /><br />
                <asp:Label ID="lbl14_01" runat="server" Text="Terdapat 2 batang pembaris plastik dan 3 batang pembaris besi. Berapakah jumlah pembaris?"
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
                <asp:Label ID="lbl14_02" runat="server" Text="Borhan mempunyai 5 buah buku cerita.  Sebuah buku telah hilang.  Berapa buah buku masih ada padanya?"
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
                <asp:Label ID="lbl14_03" runat="server" Text="Johari ada 5 potong kek.  Sepotong kek diberikan kepada Bala dan sepotong lagi diberikan kepada Ali.  Berapa potong kek yang masih tinggal?"
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
                <asp:Label ID="lbl14_04" runat="server" Text="Jika sebiji tembikai dibelah dua, berapa bahagian buah tembikai yang saya ada?"
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
                <asp:Label ID="lbl14_05" runat="server" Text="Johan mempunyai RM4.00 dan ibunya memberikan RM2.00 lagi kepadanya.  Berapakah jumlah wang yang ada pada Johan?"
                    CssClass="lbl02" Visible="false"></asp:Label>
            </td>
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
                <asp:Label ID="lbl14_06" runat="server" Text="Jika anda memegang 3 batang pen pada setiap tangan, berapakah jumlah pen yang ada pada anda?"
                    CssClass="lbl02" Visible="false"></asp:Label>
            </td>
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
