<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm1.default.Master" CodeBehind="default.00.aspx.vb" Inherits="permatapintar.default_01" %>

<%@ Register Src="commoncontrol/ukm1_top_list.ascx" TagName="ukm1_top_list" TagPrefix="uc1" %>
<%@ Register src="commoncontrol/ukm1_home.ascx" tagname="ukm1_home" tagprefix="uc2" %>
<%@ Register src="commoncontrol/ukm1_masalah_kod_sekolah.ascx" tagname="ukm1_masalah_kod_sekolah" tagprefix="uc3" %>
<%@ Register src="commoncontrol/ukm1_daftar_kelompok.ascx" tagname="ukm1_daftar_kelompok" tagprefix="uc4" %>
<%@ Register src="commoncontrol/ukm1_perigatan_penting.ascx" tagname="ukm1_perigatan_penting" tagprefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="jquery-ui.css" />
    <script type="text/javascript" src="jquery-1.10.2.js"></script>
    <script type="text/javascript" src="jquery-ui.js"></script>

    <script type="text/javascript">
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
                m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-47793747-1', 'auto');
        ga('send', 'pageview');

        function popuponclick(strURL) {
            my_window = window.open(strURL, "Jadual UKM1");

            //my_window.document.write('<h1>The Popup Window</h1>');
        }

        function closepopup() {
            if (false == my_window.closed) {
                my_window.close();
            }
            else {
                alert('Window already closed!');
            }
        }

        function laman_utama() {
            document.getElementById("home_info").style.display = "block";
            document.getElementById("kodSekolah_info").style.display = "none";
            document.getElementById("DaftarKelompok_info").style.display = "none";
            document.getElementById("PanduanUjian_info").style.display = "none";
        }

        function kod_Sekolah() {
            document.getElementById("home_info").style.display = "none";
            document.getElementById("kodSekolah_info").style.display = "block";
            document.getElementById("DaftarKelompok_info").style.display = "none";
            document.getElementById("PanduanUjian_info").style.display = "none";
        }

        function Daftar_Kelompok() {
            document.getElementById("home_info").style.display = "none";
            document.getElementById("kodSekolah_info").style.display = "none";
            document.getElementById("DaftarKelompok_info").style.display = "block";
            document.getElementById("PanduanUjian_info").style.display = "none";
        }

        function Panduan_Ujian() {
            document.getElementById("home_info").style.display = "none";
            document.getElementById("kodSekolah_info").style.display = "none";
            document.getElementById("DaftarKelompok_info").style.display = "none";
            document.getElementById("PanduanUjian_info").style.display = "block";
        }

        //$(document).ready(function () {
        //    $.fancybox("#dialog-message");
        //});

        $(function () {
            $("#dialog-message").dialog({
                modal: true,
                buttons: {
                    Ok: function () {
                        $(this).dialog("close");
                    }
                }
            });
        });

    </script>

    <table class="fbsection" border="0px">
        <tr class="fbsection_header">
            <td style="text-align: center;" colspan="2">
                <h2>UJIAN UKM1 <asp:Label ID="Lbl04" runat="server" Text=""></asp:Label></h2>
                <h3>1Hari 1Sekolah 1Murid</h3>
            </td>   
        </tr>
        <tr >
            <td style="text-align:left;" >
                <h3>
                   <b>Murid Ulangan / Sambungan Ujian: </b>
                </h3>
                <p>
                    Sila teruskan mengemaskini maklumat atau menyambung ujian UKM1 <asp:Label ID="Lbl05" runat="server" Text=""></asp:Label> <b><a href="default.01.aspx">klik di sini.</a></b>
                </p>
            </td>
            <td style="vertical-align: top; text-align: left; width: 20%;" rowspan="4">
                <table style="border: 1px solid #dd3c10;">
                    <tr style="text-align: center;">
                        <td style="text-align: center; font-size: 14px;">
                            <table style="border: 0px solid; width: 100%;">
                                <tr >
                                    <td style="text-align: center; font-weight: bolder; " colspan="3">Ujian UKM1<br />
                                        DIBUKA SEMULA SEHINGGA
                                        <asp:Label ID="lblUKM1DisplayEnd" runat="server" Text="Label"></asp:Label><br />
                                        Jumlah hari yang tinggal ialah
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 40%;">&nbsp;</td>
                                    <td style="width: 20%; color: white;">
                                        <div class="flex fab shadow-btn">
                                            <asp:Label ID="lblBalance" runat="server" Text="" Font-Bold="true" ForeColor="White"></asp:Label>
                                        </div>
                                    </td>
                                    <td style="width: 40%;">&nbsp;</td>
                                </tr>
                            </table>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td>Jumlah Pengunjung:<asp:Label ID="lblTotalNumberOfUsers" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Pengunjung Dalam Talian:<asp:Label ID="lblCurrentNumberOfUsers" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;">
                            <b>Proses Pencarian Pelajar Pintar &amp; Berbakat</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a href="pic/permata-process.png" target="_blank">
                                <img src="pic/permata-process-small.png" width="250px" height="176px" alt="permata-process" /></a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <i>Click image to enlarge</i>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:ukm1_top_list ID="ukm1_top_list1" runat="server" />
                            &nbsp;
                            <table style="width: 100%; text-align: left; vertical-align: top; border: none 0px;">
                                <tr>
                                    <td class="fbnav_header" colspan="2">Ringkasan Ujian UKM1
                                    </td>
                                </tr>
                                <tr class="fbnav_items">
                                    <td style="width: 1%;"></td>
                                    <td class="fbnav_items">
                                        <img style="vertical-align: middle;" src="icons/friend.png" width="16px" height="16px" alt="::" />
                                        <a href="public.ukm1.schoolprofile.list.mas.aspx" target="_blank">Ringkasan Ujian Sekolah</a>
                                    </td>
                                </tr>
                                <tr class="fbnav_items">
                                    <td style="width: 1%;"></td>
                                    <td class="fbnav_items">
                                        <img style="vertical-align: middle;" src="icons/friend.png" width="16px" height="16px" alt="::" />
                                        <a href="public.ukm1.schoolstate.summary.aspx" target="_blank">Ringkasan Ujian Negeri</a>
                                    </td>
                                </tr>
                                <tr class="fbnav_items">
                                    <td style="width: 1%;"></td>
                                    <td class="fbnav_items">
                                        <img style="vertical-align: middle;" src="icons/friend.png" width="16px" height="16px" alt="::" />
                                        <a href="public.ukm1.dobyear.summary.aspx" target="_blank">Ringkasan Ujian Umur</a>
                                    </td>
                                </tr>
                                <tr class="fbnav_items">
                                    <td style="width: 1%;"></td>
                                    <td class="fbnav_items">
                                        <img style="vertical-align: middle;" src="icons/friend.png" width="16px" height="16px" alt="::" />
                                        <a href="public.ukm1.studentgender.summary.aspx" target="_blank">Ringkasan Ujian Jantina</a>
                                    </td>
                                </tr>
                                <tr class="fbnav_items">
                                    <td style="width: 1%;"></td>
                                    <td class="fbnav_items">
                                        <img style="vertical-align: middle;" src="icons/friend.png" width="16px" height="16px" alt="::" />
                                        <a href="public.ukm1.studentrace.summary.aspx" target="_blank">Ringkasan Ujian Bangsa</a>
                                    </td>
                                </tr>
                                <tr class="fbnav_items">
                                    <td style="width: 1%;"></td>
                                    <td class="fbnav_items">
                                        <img style="vertical-align: middle;" src="icons/friend.png" width="16px" height="16px" alt="::" />
                                        <a href="public.ukm1.schooltype.summary.aspx" target="_blank">Ringkasan Ujian Jenis Sekolah</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <th><button id="lamanUtama" type="button" style="background-color: #CDDC39;font-weight: bold; cursor: pointer; display: inline-block; border-radius: 25px; padding: 10px 30px;font-size: 16px;" onclick="laman_utama()" >Laman Utama</button></th>
                        <th><button id="kodSekolah" type="button" style="background-color: #CDDC39;font-weight: bold; cursor: pointer; display: inline-block; border-radius: 25px; padding: 10px 30px;font-size: 16px;" onclick="kod_Sekolah()" >Masalah Kod Sekolah</button></th>
                        <th><button id="DaftarKelompok" type="button" style="background-color: #CDDC39;font-weight: bold; cursor: pointer; display: inline-block; border-radius: 25px; padding: 10px 30px;font-size: 16px;" onclick="Daftar_Kelompok()">Daftar Kelompok</button></th>
                        <th><button id="PanduanUjian" type="button" style="background-color: #CDDC39;font-weight: bold; cursor: pointer; display: inline-block; border-radius: 25px; padding: 10px 30px;font-size: 16px;" onclick="Panduan_Ujian()" >Panduan Ujian</button></th>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="font-size: 14px; color: red; padding-left:200px;"><b> Tekan butang di atas untuk maklumat lanjut </b></td>
        </tr>
        
        <tr>
            <td style="width:80%" >
                <uc2:ukm1_home ID="ukm1_home1" runat="server" />

                <uc3:ukm1_masalah_kod_sekolah ID="ukm1_masalah_kod_sekolah1" runat="server" />

                <uc4:ukm1_daftar_kelompok ID="ukm1_daftar_kelompok1" runat="server" />
                
                <uc5:ukm1_perigatan_penting ID="ukm1_perigatan_penting1" runat="server" />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblDebug" runat="server" Text=""></asp:Label>
    <div id="dialog-message" title="MAKLUMAN PENTING" style="background-color: #810000; color:#E6E6E6; width:400px; " >
        <p>
            Untuk makluman, server sedang menerima jumlah akses yang tinggi berbanding akses pada tahun sebelum ini menyebabkan server sedia ada tidak dapat menampung jumlah trafik semasa.
        </p>
        <p>
            Pihak kami sedang dalam proses menaiktaraf kapasati server untuk mengatasi masalah tersebut. Proses menaik taraf dan pengujian akan mengambil masa dalam beberapa hari.
        </p>
        <p>
            Kami menjangkakan perkhidmatan akan kembali pulih selewat-lewatnya pada Isnin 26hb Feb 2018.
        </p>
        <p>
            Kami memohon maaf di atas kesulitan yang dihadapi
        </p>
    </div>
</asp:Content>
