<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/ukm2.main.Master"
    CodeBehind="ukm2.05.00.aspx.vb" Inherits="permatapintar.ukm2_05_00" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <%--<a href="#">
        <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
            height="35" class="logo" /></a>--%>
    <h1>Araken I-PROFILE</h1>

    <h2>
    <asp:Label ID="lblSample"
            runat="server" Text=""></asp:Label>
    </h2>
    <h2>
        <asp:Label ID="ukm2_0500_header" runat="server" Text=""></asp:Label></h2>
    <p>&nbsp;</p>
    <asp:Label ID="lbl05_instruction_sample" runat="server" Text="Jadual di bawah menunjukkan simbol-simbol yang mewakili nombor 1 hingga 9. Sila padankan simbol kepada nombor-nombor berikut dengan cara <b>menaipkan nombor bagi setiap kod di dalam ruang yang disediakan.</b> Ini adalah contoh dan latihan semata-mata, tiada permarkahan diambil."></asp:Label>
    <table style="border: dotted 1px;">
        <tr>
            <td class="mytdcoding">
                <img src="images/05.00.01.jpg" alt="x" />
            </td>
            <td class="mytdcoding">
                <img src="images/05.00.02.jpg" alt="x" />
            </td>
            <td class="mytdcoding">
                <img src="images/05.00.03.jpg" alt="x" />
            </td>
            <td class="mytdcoding">
                <img src="images/05.00.04.jpg" alt="x" />
            </td>
            <td class="mytdcoding">
                <img src="images/05.00.05.jpg" alt="x" />
            </td>
            <td class="mytdcoding">
                <img src="images/05.00.06.jpg" alt="x" />
            </td>
            <td class="mytdcoding">
                <img src="images/05.00.07.jpg" alt="x" />
            </td>
            <td class="mytdcoding">
                <img src="images/05.00.08.jpg" alt="x" />
            </td>
            <td class="mytdcoding">
                <img src="images/05.00.09.jpg" alt="x" />
            </td>
        </tr>
        <tr>
            <td class="mytdcoding" style="height:40px;">
                1
            </td>
            <td class="mytdcoding">
                2
            </td>
            <td class="mytdcoding">
                3
            </td>
            <td class="mytdcoding">
                4
            </td>
            <td class="mytdcoding">
                5
            </td>
            <td class="mytdcoding">
                6
            </td>
            <td class="mytdcoding">
                7
            </td>
            <td class="mytdcoding">
                8
            </td>
            <td class="mytdcoding">
                9
            </td>
        </tr>
    </table>
    <table class="mytablebottom">
        <tr>
            <td class="mytd_codingtext">
                <img src="images/05.00.08.jpg" alt="x" />
            </td>
            <td class="mytd_codingtext">
                <img src="images/05.00.03.jpg" alt="x" />
            </td>
            <td class="mytd_codingtext">
                <img src="images/05.00.06.jpg" alt="x" />
            </td>
            <td class="mytd_codingtext">
                <img src="images/05.00.02.jpg" alt="x" />
            </td>
            <td class="mytd_codingtext">
                <img src="images/05.00.07.jpg" alt="x" />
            </td>
            <td class="mytd_codingtext">
                <img src="images/05.00.05.jpg" alt="x" />
            </td>
            <td class="mytd_codingtext">
                <img src="images/05.00.08.jpg" alt="x" />
            </td>
            <td class="mytd_codingtext">
                <img src="images/05.00.03.jpg" alt="x" />
            </td>
            <td class="mytd_codingtext">
                <img src="images/05.00.02.jpg" alt="x" />
            </td>
            <td class="mytd_codingtext">
                <img src="images/05.00.04.jpg" alt="x" />
            </td>
            <td class="mytd_codingtext">
                <img src="images/05.00.06.jpg" alt="x" />
            </td>
            <td class="mytd_codingtext">
                <img src="images/05.00.03.jpg" alt="x" />
            </td>
            <td class="mytd_codingtext">
                <img src="images/05.00.01.jpg" alt="x" />
            </td>
            <td class="mytd_codingtext">
                <img src="images/05.00.07.jpg" alt="x" />
            </td>
            <td class="mytd_codingtext">
                <img src="images/05.00.09.jpg" alt="x" />
            </td>
        </tr>
        <tr>
            <td class="TDCoding">
                <asp:TextBox ID="txtAns1" Width="30pt" runat="server" Height="20pt" MaxLength="1"
                    CssClass="txtCoding"></asp:TextBox>
            </td>
            <td class="TDCoding">
                <asp:TextBox ID="txtAns2" Width="30pt" runat="server" Height="20pt" MaxLength="1"
                    CssClass="txtCoding"></asp:TextBox>
            </td>
            <td class="TDCoding">
                <asp:TextBox ID="txtAns3" Width="30pt" runat="server" Height="20pt" MaxLength="1"
                    CssClass="txtCoding"></asp:TextBox>
            </td>
            <td class="TDCoding">
                <asp:TextBox ID="txtAns4" Width="30pt" runat="server" Height="20pt" MaxLength="1"
                    CssClass="txtCoding"></asp:TextBox>
            </td>
            <td class="TDCoding">
                <asp:TextBox ID="txtAns5" Width="30pt" runat="server" Height="20pt" MaxLength="1"
                    CssClass="txtCoding"></asp:TextBox>
            </td>
            <td class="TDCoding">
                <asp:TextBox ID="txtAns6" Width="30pt" runat="server" Height="20pt" MaxLength="1"
                    CssClass="txtCoding"></asp:TextBox>
            </td>
            <td class="TDCoding">
                <asp:TextBox ID="txtAns7" Width="30pt" runat="server" Height="20pt" MaxLength="1"
                    CssClass="txtCoding"></asp:TextBox>
            </td>
            <td class="TDCoding">
                <asp:TextBox ID="txtAns8" Width="30pt" runat="server" Height="20pt" MaxLength="1"
                    CssClass="txtCoding"></asp:TextBox>
            </td>
            <td class="TDCoding">
                <asp:TextBox ID="txtAns9" Width="30pt" runat="server" Height="20pt" MaxLength="1"
                    CssClass="txtCoding"></asp:TextBox>
            </td>
            <td class="TDCoding">
                <asp:TextBox ID="txtAns10" Width="30pt" runat="server" Height="20pt" MaxLength="1"
                    CssClass="txtCoding"></asp:TextBox>
            </td>
            <td class="TDCoding">
                <asp:TextBox ID="txtAns11" Width="30pt" runat="server" Height="20pt" MaxLength="1"
                    CssClass="txtCoding"></asp:TextBox>
            </td>
            <td class="TDCoding">
                <asp:TextBox ID="txtAns12" Width="30pt" runat="server" Height="20pt" MaxLength="1"
                    CssClass="txtCoding"></asp:TextBox>
            </td>
            <td class="TDCoding">
                <asp:TextBox ID="txtAns13" Width="30pt" runat="server" Height="20pt" MaxLength="1"
                    CssClass="txtCoding"></asp:TextBox>
            </td>
            <td class="TDCoding">
                <asp:TextBox ID="txtAns14" Width="30pt" runat="server" Height="20pt" MaxLength="1"
                    CssClass="txtCoding"></asp:TextBox>
            </td>
            <td class="TDCoding">
                <asp:TextBox ID="txtAns15" Width="30pt" runat="server" Height="20pt" MaxLength="1"
                    CssClass="txtCoding"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="15">
                <asp:Button ID="btnNext" runat="server" Text="Mula >>" CssClass="mybutton" />
                <asp:Label ID="ukm2_0500_01" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblLoadStart" runat="server" Text="0"></asp:Label>

</asp:Content>
