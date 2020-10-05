<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm1.default.Master" CodeBehind="default.org.aspx.vb" Inherits="permatapintar.default_org" %>

<%@ Register Src="commoncontrol/ukm1_top_list.ascx" TagName="ukm1_top_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        (function(i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function() {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
  m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-47793747-1', 'auto');
        ga('send', 'pageview');

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbsection" border="0px">
        <tr class="fbsection_header">
            <td>
                UJIAN UKM1. KEMPEN 1Hari 1Sekolah 1Pelajar. Sila beritahu kawan disebelah anda mengenai
                UJIAN UKM1!
                <p>
                    Jika anda pelajar ulangan atau ingin menyambung ujian, sila teruskan mengemaskini
                    maklumat atau menyambung ujian <a href="default.01.aspx">klik di sini.</a>
                </p>
            </td>
            <td style="text-align: center; font-weight: bolder;">
                Ujian UKM1<br />
                DIBUKA SEMULA SEHINGGA 31 MEI 2014<br />
                Masa yang tinggal ialah
            </td>
        </tr>
        <%--<tr><td colspan="2" style="color:Red;"><h1>UJIAN UKM1 BAGI TAHUN 2012 TELAH DITAMATKAN! SILA DATANG LAGI TAHUN HADAPAN.</h1></td></tr>--%>
        <tr>
            <td style="width: 80%; vertical-align: top;">
                <p>
                    <b>Jika anda pengguna atau pelajar baru, sila baca maklumat berikut sebelum memulakan
                        ujian.</b></p>
                <p>
                    UJIAN UKM1 ini adalah dalam Bahasa Malaysia dan Bahasa Inggeris sesuai digunakan
                    untuk menyaring darjah kecerdasan (IQ) pelajar dari umur 8 hingga 15 tahun yang
                    dijalankan sejak tahun 2009. Ujian terbuka ini boleh diambil mulai 1 Februari hingga
                    31 Mei setiap tahun . Ujian ini adalah sebahagian usaha pencarian dan pengurusan
                    bakat intelek semula jadi dalam yang dikendalikan oleh Pusat PERMATApintar Negara
                    <a href="http://www.ukm.my/permatapintar" target="_blank">http://www.ukm.my/permatapintar</a>
                    dengan kerjasama Kementerian Pendidikan Malaysia <a href="http://www.moe.gov.my"
                        target="_blank">http://www.moe.gov.my</a>, Jabatan Pedidikan Negeri, Pejabat
                    Pendidikan Daerah, Sekolah, dan Maktab Rendah Sains Mara.
                </p>
                <p>
                    Ujian UKM1 merupakan ujian saringan pertama untuk menyertai program-program berikut:<br />
                    1) Program Perkhemahan Cuti Sekolah PERMATApintar UKM-JHU (untuk pelajar 9-15 tahun/Tingkatan
                    3 pada 2014)<br />
                    2) Program PERMATA Insan Universiti Sains Islam Malaysia (untuk pelajar 8 tahun
                    / Darjah 2 pada tahun 2014 dan beragama Islam)<br />
                    3) Kolej PERMATApintar ASAS 1 2015 di UKM (untuk pelajar 11-12 thn
                    pada tahun 2014)<br />
                    4) Kolej PERMATApintar TAHAP 1 2015 di UKM (untuk pelajar 14-15 thn
                    pada 2014)<br />
                </p>
                <p>
                    Ujian dalam talian ini adalah percuma dan boleh diambil terus tanpa halangan birokrasi
                    di sekolah, rumah dan lain-lain tempat yang mempunyai kemudahan Internet dengan
                    membuka alamat <a href="http://ukm1.permatapintar.edu.my/default.01.aspx">http://ukm1.permatapintar.edu.my/default.01.aspx</a>.
                    Pelajar hanya perlu tahu perkara-perkara asas penggunaan komputer seperti menggunakan
                    tetikus dan papan kekunci. Sekiranya pelajar berhenti atau talian terputus, maka
                    pelajar boleh menyambung semula ujian di waktu yang lain sehingga tamat. Pelajar
                    telah menamatkan ujian boleh mendapatkan Sijil Penyertaan Ujian Pencarian Bakat
                    UKM1 Peringkat Kebangsaan Tahun 2014. Sila masukkan nombor mykad/mykid anda di laman
                    web <a href="http://ukm1.permatapintar.edu.my/default.01.aspx">http://ukm1.permatapintar.edu.my/default.01.aspx</a>
                    . Sila pastikan kod sekolah adalah betul sebelum dicetak. Pelajar juga boleh mengemaskini
                    maklumat dan mencetak semula sijil penyertaan jika terdapat kesilapan di alamat
                    <a href="http://pelajar.permatapintar.edu.my" target="_blank">http://pelajar.permatapintar.edu.my</a>
                </p>
                <h4>
                    NOTA PENTING</h4>
                <p>
                    1. Bagi pelajar yang pernah mengambil ujian UKM1 pada tahun-tahun yang lepas, setelah
                    memasukkan nombor mykad/mykid, anda hanya perlu mengemaskini maklumat pelajar, maklumat
                    sekolah (jika berpindah sekolah) dan maklumat keluarga (supaya pihak pengurusan
                    PERMATApintar, KPM/JPN/PPD dapat menghubungi keluarga pelajar bagi urusan ujian
                    UKM2 dan tawaran ke program perkhemahan cuti sekolah).</p>
                <p>
                    2. Perlu diingatkan bahawa pelajar tidak dibenarkan mendapatkan bantuan dari orang
                    lain semasa menjawab soalan dalam ujian saringan ini. Pelajar hanya dibenarkan mengambil
                    ujian saringan ini sekali sahaja. Pelajar perlu memasukkan maklumat peribadi, sekolah
                    dan ibu bapa atau penjaga sebelum memulakan ujian saringan ini. Ibubapa, penjaga
                    atau guru dibenarkan membantu pelajar untuk memasukkan maklumat pelajar, sekolah
                    dan ibu bapa atau penjaga sahaja. Sekiranya orang dewasa yang membuatkan ujian ini
                    untuk pelajar dan pelajar berjaya untuk menduduki UKM2, orang dewasa telah menganiaya
                    pelajar berkenaan dan juga menyusahkan pihak kami mengaturkan sesi ujian dan pusat
                    ujian. Setiap tahun kami dapati ada beberapa pelajar tidak dapat melakukan ujian
                    UKM2 termasuk tidak tahu menggunakan komputer. Setelah ditemu bual, pelajar memberitahu
                    bahawa mereka tidak pernah mengambil ujian UKM1; ujian dibuat oleh orang lain. Kami
                    juga dapat mengesan ada guru yang membuat ujian ini untuk pelajar.</p>
                <p>
                    3. Terdapat aduan yang pihak kami terima iaitu mengambil masa yang lambat untuk
                    mengakses laman ujian ini. Server ujian sentiasa dipantau penggunaannya dengan jumlah
                    pengguna yang bersesuaian. Jika terdapat kelambatan mengakses, ia berkemungkinan
                    kelajuan internet di sekolah yang menghadapi masalah kerana ia boleh menampung beberapa
                    pengguna serentak dalam sekolah berkenaan.
                </p>
                <p>
                    4. Terdapat panggilan telefon kepada kami. Pihak kami amat menghargai jika sebarang
                    kemusykilan atau pertanyaan diajukan melalui emel <b>permatapintar@ukm.my</b> kerana
                    komunikasi ini direkodkan dan pihak kami akan memanjangkan kepada pihak lain yang
                    dipertanggungjawabkan.
                </p>
                <h4>
                    MASALAH KOD SEKOLAH</h4>
                <p>
                    a) Bagi sekolah-sekolah Kementerian Pelajaran dan MRSM, sekiranya kod sekolah adalah
                    seperti XXX2013448522, ini bermakna kod adalah salah. Gunakan panduan carian sekolah
                    diberi semasa mengisi maklumat sekolah. Sila pastikan anda <b>memilih negeri</b>
                    terlebih dahulu dan gunakan satu perkataan kata kunci yang unik.
                </p>
                <p>
                    b) Sekiranya sekolah-sekolah sekolah-sekolah Kementerian Pelajaran dan MRSM yang
                    baru dibuka pada pertengahan tahun 2013 dan awal tahun 2014, harap dapat maklumkan
                    kepada kami melalui emel <b>permatapintar@ukm.my</b> untuk kami dapatkan kod sekolah.
                </p>
                <p>
                    c) Bagi sekolah persendirian dan sekolah rendah agama yang tidak berdaftar di Kementerian
                    Pelajaran, sekolah ini boleh didaftarkan sebagai sekolah baru dan akan mendapat
                    kod XXX2013448522 dan pihak PERMATApintar akan menetapkan kod untuk sekolah berkenaan.</p>
                <p>
                    d) Jika sekiranya menghadapi masalah untuk mendaftar sekolah sila berikan maklumat
                    sekolah (nama sekolah, alamat lengkap, telefon, faks dan emel sekolah) kepada <b>permatapintar@ukm.my</b>
                    untuk didaftarkan dan diberi kod sekolah.
                </p>
                <p>
                    e) Sebarang pertanyaan tentang ujian ini boleh dikemukakan kepada alamat <b>permatapintar@ukm.my</b>.
                    Sila sertakan maklumat berikut di dalam email tuan/puan:<br />
                    1. *MYKAD/MYKID#:<br />
                    2. *Nama Penuh:<br />
                    3. *Nama Sekolah:<br />
                    4. *Negeri Sekolah:<br />
                </p>
                <p>
                    <b>UNTUK PARA GURU SEKOLAH SAHAJA (KEMASUKAN DATA BERKELOMPOK. 50 pelajar ke atas.)</b><br />
                    1. Download Excel FORMAT di sini: a) <a href="download/student-profile-batch.xlsx"
                        target="_blank">Excel XLSX Format</a>&nbsp;&nbsp; b) <a href="download/student-profile-batch.xls"
                            target="_blank">Excel XLS Format</a><br />
                    2. Masukkan maklumat yang diperlukan bagi pelajar BARU sahaja.<br />
                    3. Sila ikut format yang telah ditetapkan. Contoh juga disertakan.<br />
                    4. Jangan tukar nama 'HEADER' bagi setiap lajur yang telah ditetapkan. <br />
                    5. Email ke <b>permatapintar@ukm.edu.my</b><br />
                    6. Fail akan diproses dalam masa 2-3 hari bekerja. Tuan/Puan akan dimaklumkan setelah
                    fail diproses.</p>
                <p>
                    <b>(Adalah lebih baik pelajar mengisi sendiri maklumat sebagai satu latihan literasi
                        maklumat)</b><br />
                        Nota: Maklumat PC ini akan disimpan bagi siasatan lanjut jika terdapat aduan penyalahgunaan MYKAD. 
                        </p>
            </td>
            <td style="vertical-align: top; text-align: left; width: 20%;">
                <table style="border: 1px solid #dd3c10;">
                    <tr>
                        <td style="text-align: center; font-size: 14px;">

                            <script language="javascript" type="text/javascript">
                                TargetDate = "5/31/2014 11:59 PM";
                                BackColor = "white";
                                ForeColor = "red";
                                CountActive = true;
                                CountStepper = -1;
                                LeadingZero = true;
                                DisplayFormat = "%%D%% Hari, %%H%% Jam, %%M%% Minit, %%S%% Saat.";
                                FinishMessage = "Ujian UKM1 ditamatkan!";
                            </script>

                            <script language="javascript" type="text/javascript" src="countdown.js"></script>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Jumlah Pengunjung:<asp:Label ID="lblTotalNumberOfUsers" runat="server" Text="0"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Pengunjung Dalam Talian:<asp:Label ID="lblCurrentNumberOfUsers" runat="server" Text="0"></asp:Label>
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
                                <img src="pic/permata-process-small.jpg" width="250px" height="176px" alt="permata-process" /></a>
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
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="fbform_sap" colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnNextPage" runat="server" Text="Seterusnya >>" CssClass="fbbutton"
                    Visible="True" />
            </td>
        </tr>
    </table>
</asp:Content>