<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/nocheck.Master"
    CodeBehind="default.intro.aspx.vb" Inherits="permatapintar.default_intro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="tablelogin" border="0px">
        <tr>
            <td style="width: 30px;">&nbsp;
            </td>
            <td valign="middle">
                <h1>Araken I-PROFILE</h1>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>
                <h2>Perhatian:</h2>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>Ujian ini khas untuk kegunaan <b>Araken I-PROFILE</b>.
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>
                <asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>1) PERHATIAN: Buat masa kini, ujian ini hanya boleh dijalankan menggunakan perisian pelayaran:<br />
                <ul>
                    <li>
                        <img src="../images/IE.png" alt="IE" />Microsoft Internet Explorer</li>
                    <li>
                        <img src="../images/chrome.png" alt="IE" />Google Chrome</li>
                </ul>
                <br />
                2) Memahami <b>ARAHAN</b> ujian adalah salah satu bahagian yang diuji dalam saringan
                kedua ini. Jadi, anda tidak perlu bertanya soalan kepada pengawas. Anda hanya dibenarkan
                memanggil pengawas jika berlaku gangguan teknikal seperti masa untuk memaparkan
                gambar yang terlalu lama, gangguan teknikal komputer seperti komputer tiba-tiba
                terbehenti operasi ('hang'), papan kekunci (keyboard) dan tetikus (mouse) yang tidak
                berfungsi atau pergerakan tetikus yang tidak lancar. Anda akan dipindahkan ke komputer
                lain. Jika talian terputus dan gangguan teknikal yang lain, sila login semula dan
                sambung ujian.<br />
                <br />
                3) Ujian ini telah diprogramkan untuk dijawab dalam tempoh <b>
                    <asp:Label ID="lblDuration" runat="server" Text="0"></asp:Label>
                    MINIT</b> sahaja. Jumlah masa ini telah mengambil kira jika berlaku gangguan
                teknikal, kelajuan internet di pusat ujian, dan jika anda beralih ke komputer yang
                lain. Pastikan anda menjawab sepantas dan setepat yang boleh kerana anda juga diuji
                untuk kadar pemprosesan maklumat yang di fikiran anda. Seboleh-bolehnya menjawab
                dalam tempoh 65-70 minit dengan fokus. Masa akan diambil kira apabila anda menekan
                butang <b>MULA</b>.<br />
                <br />
                4) Anda perlu <b>FOKUS</b> semasa menjawab ujian ini. Jika anda berkomunikasi dengan
                pihak lain melalui internet, membuat rujukan dan bertanya rakan di sebelah anda,
                ia akan mengganggu tumpuan anda dan juga rakan yang lain dan juga masa ujian.<br />
                <br />
                5) Alamat Internet yang digunakan oleh komputer anda direkodkan. Selain pemantauan
                secara bersemuka oleh pengawas dan pemantau di pusat ujian, anda juga dipantau secara
                dalam talian oleh pihak ARAKEN semasa menjawab soalan. Semua pergerakan anda
                dalam menjawab soalan direkodkan.<br />
                <br />
                6) Terdapat 15 Modul dengan 375 item. Anda perlu berhati-hati menggunakan butang
                <b>LANGKAU</b> modul. Ia bukannya untuk melangkau ke soalan berikutnya dalam sesuatu
                modul. Ia hanya digunakan sekiranya anda telah mengalami <b>KEBUNTUAN</b> untuk
                menjawab soalan dan masa yang lama diambil dalam sesuatu modul ini. Anda tidak boleh
                berpatah balik ke modul yang sebelumnya.
                <br />
                <br />
                7) Dalam setiap modul, terdapat beberapa soalan. Anda perlu menjawab dengan tepat
                dan pantas. Setiap jawapan akan direkodkan terus di komputer pelayan (server). Dengan
                itu, jika anda berpatah balik menggunakan kemudahan Back atau Forward ikon , dan
                menjawab semula soalan, maka ia tidak diambil kira. Oleh sebab itu teruskan menjawab
                soalan berikutnya tanpa membuang masa<br />
                <br />
                8) Calon <b>TIDAK BOLEH</b> menggunakan sebarang pensil dan kertas untuk membuat
                pengiraan atau catatan kerana dikhuatiri ia mengambil masa ujian dan ujian juga
                menguji pemikiran kerja pelajar tanpa bantuan alat lain.<br />
                <br />
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td></td>
            <td style="font-size: large; text-align: left; font-weight: bold;">Calon perlu JUJUR dan BERTANGGUNGJAWAB.<br />
            </td>
        </tr>
        <tr>
            <td></td>
            <td style="font-size: large; text-align: left; font-weight: bold;">
                <p>
                    TUHAN MELIHAT APA YANG ANDA LAKUKAN.
                </p>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>
                <asp:CheckBox ID="chkAgree" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnNext" runat="server" Text="Seterusnya >>" CssClass="mybutton" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>
                <asp:Label ID="default_03" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblDebug" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblStudentFullname" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>
