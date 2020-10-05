<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/main.Master" CodeBehind="ukm2.permata.end.aspx.vb" Inherits="permatapintar.ukm2_permata_end" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width="900px" style="border: 1px dotted black; text-align: center; font-family: @Arial Unicode MS;
        font-size: 14px;">
        <tr>
            <td style="text-align: center;">
                <img src="pic\sijil-top.png" alt="PP" />
            </td>
        </tr>
        <tr>
            <td class="fbform_sap">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center; font-family: @Arial Unicode MS; font-size: 36px; font-weight: bold;">
                Sijil Penyertaan
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                Dengan ini disahkan bahawa
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center; font-family: @Arial Unicode MS; font-size: 18px; font-weight: bold;">
                <asp:Label ID="StudentFullname" runat="server" Text="" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center; font-family: @Arial Unicode MS; font-size: 18px; font-weight: bold;">
                <asp:Label ID="MYKAD" runat="server" Text="" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center; font-family: @Arial Unicode MS; font-size: 18px; font-weight: bold;">
                <asp:Label ID="lblSchoolCode" runat="server" Text="" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center; font-family: @Arial Unicode MS; font-size: 18px; font-weight: bold;">
                <asp:Label ID="SchoolName" runat="server" Text="" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center; font-family: @Arial Unicode MS; font-size: 18px; font-weight: bold;">
                <asp:Label ID="lblSchoolcity" runat="server" Text="" Font-Bold="true"></asp:Label>,&nbsp;
                <asp:Label ID="SchoolState" runat="server" Text="" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                telah menamatkan
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center; font-family: @Arial Unicode MS; font-size: 18px; font-weight: bold;">
                Ujian Pencarian Bakat UKM2
                <asp:Label ID="lblExamYear" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center; font-family: @Arial Unicode MS; font-size: 18px; font-weight: bold;">
                Peringkat Kebangsaan
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <b>Tarikh MULA:
                    <asp:Label ID="lblExamStartDisp" runat="server" Text=""></asp:Label></b>
            </td>
        </tr>
        <tr>
            <td>
                <b>Tarikh TAMAT:
                    <asp:Label ID="lblExamEndDisp" runat="server" Text=""></asp:Label></b>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center; font-family: @Arial Unicode MS; font-size: 18px; font-weight: bold;">
                Tahniah dan Terima kasih
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <b>Pusat PERMATApintar Negara</b><br />
                Universiti Kebangsaan Malaysia<br />
                43600 Bangi, Selangor, Malaysia<br />
                Tel: +603-8921 7503<br />
                Fax: +603-8921 7525<br />
                E-Mail: permatapintar@ukm.my<br />
            </td>
        </tr>
        <tr>
            <td style="text-align: center; font-family: @Arial Unicode MS; font-size: 14px; font-style: italic;">
                (Sijil ini janaan komputer dan tidak perlu tandatangan)
            </td>
        </tr>
    </table>
    <br />
    <table width="900px" style="border: 0px dotted black; text-align: center;">
        <tr>
            <td class="fbform_sap">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <asp:Button ID="btnCreate" runat="server" Text="Jana fail PDF" CssClass="fbbutton" />&nbsp;
                [ Klik butang disebelah untuk menjana sijil penamatan Ujian Dalam Talian UKM2 dalam
                bentuk PDF ]
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td class="fbform_sap" style="text-align: left;">
                <asp:Label ID="lblMsg" runat="server" Text="" CssClass="labelMsg"></asp:Label>
                &nbsp;
                <asp:HyperLink ID="hyPDF" runat="server" Target="_blank">Klik disini.</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
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
            <td style="text-align: left;">
                Nota: Keputusan pelajar yang layak untuk mengambil Ujian Dalam Talian UKM2 akan
                diberitahu kelak. Sila layari laman utama PERMATApintar www.permatapintar.edu.my
                pada bulan
                <asp:Label ID="UKM2Result" runat="server" Text="" CssClass="labelMsg"></asp:Label>
                <asp:Label ID="lblLevel" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblTotalMin" runat="server" Text="0" Visible="false"></asp:Label>
    <asp:Label ID="lblDOB_Year" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
    <asp:Label ID="lblDuration" runat="server" Text="0" Visible="false"></asp:Label>
    <asp:Label ID="lblExamStart" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblExamEnd" runat="server" Text="" Visible="false"></asp:Label></asp:Content>