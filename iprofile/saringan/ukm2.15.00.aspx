<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/ukm2.main.Master" CodeBehind="ukm2.15.00.aspx.vb" Inherits="permatapintar.ukm2_15_00" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <%--<a href="#">
        <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
            height="35" border="0" class="logo" /></a>--%>
    <h1>Araken I-PROFILE</h1>

    <h2><asp:Label ID="lblSample" runat="server" Text=""></asp:Label></h2>
    <h2><asp:Label ID="ukm2_1500_header" runat="server" Text=""></asp:Label></h2>
    <p>&nbsp;</p>
    <asp:Label ID="lbl15_instruction_sample" runat="server" Text="Sila baca pembayang dan tuliskan jawapan di ruangan yang disediakan. Ini adalah contoh dan latihan semata-mata, tiada permarkahan diambil."></asp:Label>
    <table class="mytablemain">
        <tr>
            <td>
                <asp:Label ID="lbl15_00" runat="server" Text="Ia ada pintu, ada rak dan boleh menyimpan pakaian."
                    CssClass="lbl02"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt15_00" runat="server" Width="350px" CssClass="textbox14" TextMode="MultiLine"
                    Rows="3" Text="Almari pakaian, gerobok"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnNext" runat="server" Text="Mula >>" CssClass="mybutton" />
                <asp:Label ID="ukm2_1500_01" runat="server" Text="Tekan [Mula >>] jika anda sudah bersedia dan faham apa yang perlu dilakukan."></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblLoadStart" runat="server" Text="0"></asp:Label>

</asp:Content>
