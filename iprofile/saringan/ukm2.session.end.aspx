<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/nocheck.Master"
    CodeBehind="ukm2.session.end.aspx.vb" Inherits="permatapintar.ukm2_session_end" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="tablelogin">
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <h2>
                    <asp:Label ID="lbl02_p01" runat="server" Text="TERDAPAT PERUBAHAN PADA URL ATAU ANDA BELUM LOGIN. SILA LOGIN SEMULA."
                        CssClass="lblHeader"></asp:Label></h2>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Label ID="Label9" runat="server" Font-Italic="true" Text="Nota: Anda akan dibawa ke laman ini jika anda menukar URL yang disediakan atau anda ditanda belum login. Jangan risau. Anda akan dibawa ke laman terakhir anda selepas anda login semula ke dalam ujian ini."></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <a href="../default.aspx?lang=ms-MY">
                    <asp:Label ID="lbl15_instruction" runat="server" Text="Sila tekan disini untuk login semula dan kembali ke laman terakhir anda."></asp:Label></a>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
