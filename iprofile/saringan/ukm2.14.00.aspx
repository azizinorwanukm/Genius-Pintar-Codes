<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/ukm2.main.Master" CodeBehind="ukm2.14.00.aspx.vb" Inherits="permatapintar.ukm2_14_00" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<a href="#">
        <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
            height="35" border="0" class="logo" /></a>--%>
    <h1>Araken I-PROFILE</h1>

    <h2>
        <asp:Label ID="lblSample" runat="server" Text=""></asp:Label></h2>
    <h2>
        <asp:Label ID="ukm2_1400_header" runat="server" Text=""></asp:Label></h2>

    <p>&nbsp;</p>
    <asp:Label ID="lbl14_instruction_sample" runat="server" Text="Baca soalan dengan teliti, kira jawapan secara mental (congak). Isi jawapan di ruangan yang disediakan. 
    Sekiranya jawapan anda salah sebanyak empat kali berturut-turut, anda akan dibawa ke modul seterusnya."></asp:Label>
    <table class="mytablemain">
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Start  " class="mybutton" /><br />
                <asp:Label ID="lbl14_00" runat="server" Text="Terdapat dua batang pembaris plastik dan tiga batang pembaris besi. Berapakah jumlah pembaris?"
                    CssClass="lbl02" Visible="false"></asp:Label>
            </td>
            <td style="vertical-align:bottom;">
                <asp:TextBox ID="txt14_00" runat="server" Width="200px" CssClass="textbox14" Text=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnNext" runat="server" Text="Start  " CssClass="mybutton" />
                <asp:Label ID="ukm2_1400_01" runat="server" Text="Tekan [Start  ] jika anda sudah bersedia dan faham apa yang perlu dilakukan."></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblLoadStart" runat="server" Text="0"></asp:Label>

</asp:Content>
