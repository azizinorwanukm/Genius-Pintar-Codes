<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ukm1_home.ascx.vb" Inherits="permatapintar.ukm1_home" %>
<style type="text/css">
   
    .auto-style1 {
        width: 61px;
        text-align: center;
        border: 1px solid black;
     }
    .auto-style2 {
        width: 627px;
        border: 1px solid black;
        padding: 5px;
    }
    .auto-style3 {
        width: 34px;
        vertical-align: top;
    }
    .tbltext{
        vertical-align: top;
    }
    .table1 {
        border-collapse: collapse;
        margin-left: 5%;
        margin-right: 5%;
    }

    .auto-style4 {
        width: 631px;
        border: 1px solid black;
        padding: 5px;
    }
    
 
</style>
<div style="display: block; height: 1000px" id="home_info">
    <table >
        <tr>
            <td style="width: 100%; vertical-align: top; border-collapse: collapse;">
                <h3>Murid Baharu (Pertama kali): </h3>
                <p>
                    Murid,  ibu bapa atau guru,  mohon baca maklumat berikut sebelum murid memulakan ujian.
                </p>
                <p> 
                    UJIAN UKM1 merupakan ujian saringan pertama untuk pencalonan murid-murid Malaysia  berusia 8 hingga 15 tahun yang dijalankan sejak tahun 2009 untuk menyertai program-progran berikut.
                </p>
                <div>
                 <table class=table1 style="width: 70%;  ">
                    <tr >
                        <th style="border: 1px solid black;text-align: center;"><b>Bil</b></th>
                        <th style="border: 1px solid black;text-align: center;"><b>Kriteria Umur</b></th>
                        <th style="border: 1px solid black;text-align: center;"><b>Program</b></th>
                    </tr>
                    <tr>
                        <td class="auto-style1">1</td>
                        <td class="auto-style2">
                            Murid beragama <b>Islam</b> berusia <b>8–11 tahun</b> (Tahun 2 – 5) pada <br />tahun <asp:Label ID="lblYear01" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="auto-style4">
                            Program Perkhemahan Cuti Sekolah  (PPCS) STEM, Universiti Sains Islam Malaysia (PPCS @USIM Disember <asp:Label ID="lblYear02" runat="server" Text=""></asp:Label>)
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">2</td>
                        <td class="auto-style2">
                            Murid beragama <b>Islam</b> berusia <b>11-12 tahun </b>(Tahun 6) pada  tahun <asp:Label ID="lblYear03" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="auto-style4">
                            Program Mukhayyam Al Abrar Kolej PERMATA Insan (KPI) pemilihan ambilan Asas 1 tahun 2020
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">3</td>
                        <td class="auto-style2">
                            Murid pelbagai <b>agama dan bangsa</b> berusia  <b>9-15 tahun</b>  <br />(Tahun 3 – Tingkatan 3) pada <asp:Label ID="lblYear04" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="auto-style4">
                            PPCS STEM Universiti Kebangsaan Malaysia (PPCS @UKM Disember <asp:Label ID="lblYear05" runat="server" Text=""></asp:Label>)
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">4</td>
                        <td class="auto-style2">
                            Murid  pelbagai <b>agama dan bangsa</b> berusia <b>11 - 12 tahun</b> (Tahun 5 – Tahun 6) pada tahun <asp:Label ID="lblYear06" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="auto-style4">
                            Kolej PERMATApintar® UKM ambilan Asas 1 tahun 2020
                        </td>
                    </tr>
                </table>
                </div>
                <br />
                <br />

                <table >
                    <tr>
                        <td class="auto-style3">1.</td>
                        <td class="tbltext">
                            Ujian terbuka ini boleh diambil mulai 1 Februari hingga 31 Mei setiap tahun. 
                            Ujian ini adalah sebahagian usaha pencarian murid pintar intelek semula jadi yang dikendalikan oleh 
                            Pusat PERMATApintar Negara dengan kerjasama Kementerian Pendidikan Malaysia; Jabatan Pedidikan Negeri, 
                            Pejabat Pendidikan Daerah,<b><a href="http://permatainsan.usim.edu.my/" target="_blank">Kolej PERMATA Insan</a></b> (kendalian Universiti Sains Malaysia - USIM), Sekolah, dan Maktab Rendah Sains Mara.
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3">2.</td>
                        <td class="tbltext">
                            Ujian dalam talian ini adalah percuma dan boleh diambil terus tanpa halangan birokrasi di sekolah, 
                                rumah dan lain-lain tempat yang mempunyai kemudahan Internet dengan membuka laman ini.
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3">3.</td>
                        <td class="tbltext">
                           Murid hanya perlu tahu perkara-perkara asas penggunaan komputer seperti menggunakan tetikus (mouse), 
                                pad sentuh (touchpad) dan papan kekunci (keyboard). Sekiranya murid berhenti atau talian terputus, 
                                maka murid boleh menyambung semula ujian di waktu yang lain sehingga tamat.
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3">4.</td>
                        <td class="tbltext">
                            Murid telah menamatkan ujian boleh mendapatkan Sijil Penyertaan Ujian Pencarian Bakat UKM1 Peringkat Kebangsaan 
                                Tahun <asp:Label ID="lblYear07" runat="server" Text=""></asp:Label>. Sijil ini mempunyai skor minima untuk mendapatkannya. 
                                Jika tidak mencapai skor ini, sijik tidak dikeluarkan. <b> Murid yang layak mendapat sijil penyertaan UKM1 tidak semestinya murid tersebut melepasi skor kelayakan untuk ke ujian saringan kedua UKM2.</b> 
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3">5.</td>
                        <td class="tbltext">
                            Sila pastikan kod sekolah adalah betul sebelum dicetak. Murid juga boleh mengemaskini maklumat dan mencetak semula sijil 
                                penyertaan jika terdapat kesilapan di alamat <b><a href="http://pelajar.permatapintar.edu.my/" target="_blank">http://pelajar.permatapintar.edu.my/</a> </b>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3">6.</td>
                        <td class="tbltext">
                            Murid yang melepasi skor kelayakan akan ditawarkan untuk sertai ujian saringan kedua UKM2 secara terkawal yang akan diadakan dalam bulan Julai hingga Ogos.  
                            Semakan kelayakan boleh dibuat di laman <b><a href="http://semak.permatapintar.edu.my/" target="_blank">http://semak.permatapintar.edu.my/</a> </b> mulai bulan Julai. Murid/guru/ibu bapa perlu memastikan nombor telefon dan emel yang betul dalam maklumat murid.
                        </td>
                    </tr>
                </table>
         </td>
        </tr>
        <tr>
                <td class="fbform_sap" colspan="2">&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnNextPage" runat="server" Text="Seterusnya >>" CssClass="fbbutton"
                        Visible="True" />
                </td>
            </tr>
    </table>
</div>