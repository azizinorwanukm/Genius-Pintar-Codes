<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm1.default.Master"
    CodeBehind="default.end.aspx.vb" Inherits="permatapintar._default" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" border="0px" style="vertical-align: top;">
        <tr>
            <td>
                <h1>UJIAN UKM1 telah tamat pada
                    <asp:Label ID="lblUKM1DisplayEnd" runat="server" Text=""></asp:Label><br />
                    Sila datang lagi pada tahun hadapan.</h1>
            </td>
        </tr>

        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <h3><a href="http://pelajar.permatapintar.edu.my/" target="_blank">
                    <img src="img/right.gif" alt="::" />KLIK DI SINI UNTUK MENCETAK SIJIL UJIAN UKM1</a></h3>
            </td>
        </tr>
        <tr>
            <td>Nota: Sijil hanya boleh dicetak bagi pelajar yang telah menamatkan Ujian UKM1.
            </td>
        </tr>
        <tr>
            <td>
                <h3><a href="http://semak.permatapintar.edu.my/" target="_blank">
                    <img src="img/right.gif" alt="::" />KLIK DI SINI UNTUK MENYEMAK KELAYAKKAN KE UKM2</a></h3>
            </td>
        </tr>
        <tr>
            <td>Nota: Keputusan kelayakkan ke UKM2 dijangka akan diumumkan pada
                <asp:Label ID="lblUKM1DisplayResult" runat="server" Text=""></asp:Label>
            </td>
        </tr>

        <tr>
            <td>
                <h3><a href="http://ukm1.permatapintar.edu.my/public.ukm1.kodsekolah.summary.aspx" target="_blank">
                    <img src="img/right.gif" alt="::" />KLIK DI SINI UNTUK MENYEMAK RINGKASAN UJIAN MENGIKUT KOD SEKOLAH</a></h3>
            </td>
        </tr>

    </table>
</asp:Content>
