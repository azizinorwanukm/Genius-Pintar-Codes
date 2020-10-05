<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/main.Master"
    CodeBehind="default.main.aspx.vb" Inherits="permatapintar.default_main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbsection">
        <tr class="fbsection_header">
            <td colspan="2">
                Laman Utama Pelajar PERMATApintar Negara
            </td>
        </tr>
        <tr>
            <td class="fbsection_article">
                <img src="img/permatapintar.jpg" alt="PERMATApintar" />
            </td>
        </tr>
        <tr>
            <td>
                <b>Pusat PERMATApintar Negara</b><br />
                Fakulti Pendidikan<br />
                Universiti Kebangsaan Malaysia<br />
                43600 Bangi Selangor, Malaysia<br />
                <br />
                Tel: 03-8921 7152/ 7153/ 7154<br />
                Fax: 03-8921 6642<br />
                E-Mail: permatapintar@ukm.edu.my
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <div class="info">
        Selamat datang ke Laman Utama bagi Pelajar PERMATApintar.
        <ul>
            <li>Anda dikehendaki mengemaskini maklumat diri, sekolah dan ibubapa/penjaga anda. <a href="studentprofile.complete.view.aspx?studentid=<%=CType(Session.Item("permata_studentid"), String) %>" rel="nofollow" target="_self">Klik di sini.</a></li>
            <li>Anda dikehendaki mengemaskini maklumat peribadi anda terutam MYKAD# kepada format
                yagn betul. Contoh [990820086011]. Tanpa tanda sempang.</li>
        </ul>
    </div>
    <div class="warning">
        <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label></div>
</asp:Content>
