<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/main.Master" CodeBehind="ukm1.cert.print.aspx.vb" Inherits="permatapintar.ukm1_cert_print" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width="50%" border="0px">
        <tr>
            <td>
                <img src="pic\logosijilv2.png" alt="PP" />
            </td>
        </tr>
        <tr>
            <td>
                <h2>Pusat PERMATApintar Negara</h2>
                Universiti Kebangsaan Malaysia<br />
                43600 Bangi, Selangor, Malaysia<br />
                Tel: +603-8921 7503<br />
                Fax: +603-8921 7525<br />
                E-Mail: permatapintar@ukm.my<br />
            </td>
        </tr>
        <tr>
            <td class="fbform_sap">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                Dengan ini mengesahkan bahawa
            </td>
        </tr>
        <tr>
            <td>
                NAMA PELAJAR:
                <asp:Label ID="StudentFullname" runat="server" Text="" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                MYKAD/MYKID#:
                <asp:Label ID="MYKAD" runat="server" Text="" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                NAMA SEKOLAH:
                <asp:Label ID="SchoolName" runat="server" Text="" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                NEGERI:
                <asp:Label ID="SchoolState" runat="server" Text="" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                telah menamatkan Ujian Dalam Talian UKM1<b>
                <asp:Label ID="ExamEnd" runat="server" Text=""></asp:Label></b>
            </td>
        </tr>
        <tr>
            <td class="fbform_sap">
                <h4>Ujian Dalam Talian UKM1 TAHUN
                <asp:Label ID="ExamYear" runat="server" Text=""></asp:Label></h4>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnCreate" runat="server" Text="Jana fail PDF" CssClass="fbbutton" />&nbsp;
                [ Klik butang disebelah untuk menjana sijil penamatan Ujian Dalam Talian UKM1 dalam bentuk PDF
                ]
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td class="fbform_sap">
                <asp:Label ID="lblMsg" runat="server" Text="" CssClass="labelMsg"></asp:Label>
                &nbsp;
                <asp:HyperLink ID="hyPDF" runat="server" Target="_blank">Klik disini.</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <a href="http://get.adobe.com/reader/" target="_blank">
                    <img src="pic/get_adobe_reader.png" alt="GET ADOBE" /></a>&nbsp;[Muat turun
                dan Install ADOBE jika gagal menjana fail PDF]
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                Nota: Keputusan pelajar yang layak untuk mengambil Ujian Dalam Talian UKM2 akan diberitahu kelak.
                Sila layari laman utama PERMATApintar www.permatapintar.edu.my pada bulan
                <asp:Label ID="UKM1Result" runat="server" Text="" CssClass="labelMsg"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
