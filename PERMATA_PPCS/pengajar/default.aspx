<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/pengajar/main.Master"
    CodeBehind="default.aspx.vb" Inherits="permatapintar._default2" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" border="0px" style="vertical-align: top;">
        <tr>
            <td class="fbsection_header">
                Selamat datang para PENGAJAR ke Program Perkhemahan Cuti Sekolah PERMATApintar Negara
                2010
            </td>
        </tr>
        <tr>
            <td class="fbsection_article">
                <b>Perhatian.</b>
                <p>
                    Anda hanya dibenarkan untuk melihat Laporan Penaksiran Akademik, Inventori Tingkahlaku dan
                    Soalselidik Spiritual pelajar-pelajar di dalam kelas anda sahaja.
                </p>
                <p>
                    Data mengenai Laporan Penaksiran Akademik dan Inventori Tingkahlaku dimasukkan oleh Pembantu
                    Pengajar.</p>
                <p>
                    Soalselidik Spritual dilakukan oleh pelajar itu sendiri dan anda hanya boleh melihat
                    apa yang dijawab oleh para pelajar kelas anda.</p>
            </td>
        </tr>
    </table>
    <div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></div>
</asp:Content>
