<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pelajar.Master" CodeBehind="pelajar_password_baru.aspx.vb" Inherits="WebApplication1.pelajar_password_baru" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <link rel="stylesheet" href="css/style.css" />

    <section class="container">
        <div class="login">
            <img src="img/ukm.jpg"><br /><br />
            <asp:TextBox runat="server" placeholder="Kata Laluan Lama"></asp:TextBox>
            <asp:TextBox TextMode="Password" runat="server" placeholder="Kata Laluan Baru"></asp:TextBox>
            <asp:TextBox TextMode="Password" runat="server" placeholder="Kata Laluan Baru Semula"></asp:TextBox>

            <div class="w3-container" style="text-align: center">
                <br />
                <asp:Button ID="ButtonKembali" runat="server" Text="Kembali" class="w3-button w3-light-blue" />
                <asp:Button runat="server" Text="Simpan" class="w3-button w3-light-blue" />
            </div>
        </div>
    </section>
</asp:Content>
