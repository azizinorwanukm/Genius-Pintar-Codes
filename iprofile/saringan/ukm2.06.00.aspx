<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/ukm2.main.Master" CodeBehind="ukm2.06.00.aspx.vb" Inherits="permatapintar.ukm2_06_00" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<a href="#">
        <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
            height="35" border="0" class="logo" /></a>--%>
    <h1>Araken I-PROFILE</h1>

            
    <h2><asp:Label ID="lblSample" runat="server" Text=""></asp:Label></h2>
    <h2><asp:Label ID="ukm2_0600_header" runat="server" Text=""></asp:Label></h2>
        <p>&nbsp;</p>
        <asp:Label ID="lbl06_instruction_sample" runat="server" Text="Taipkan maksud bagi perkataan-perkataan berikut dalam ruangan yang disediakan. Ini adalah contoh dan latihan semata-mata, tiada permarkahan diambil."></asp:Label>
        
    <table class="mytablemain">
        <tr>
            <td>
                <asp:Label ID="lbl06_00" runat="server" Text="PENSEL" CssClass="lbl02"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txt06_00" runat="server" TextMode="MultiLine" Rows="3" Width="90%" Text="" MaxLength="300"></asp:TextBox><br />
                <asp:Label ID="ukm2_0600_01" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnNext" runat="server" Text="Mula >>" CssClass="mybutton" />
                <asp:Label ID="ukm2_0600_02" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblLoadStart" runat="server" Text="0"></asp:Label>

</asp:Content>
