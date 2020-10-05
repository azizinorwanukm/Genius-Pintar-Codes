<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ukm1_daftar_kelompok.ascx.vb" Inherits="permatapintar.ukm1_daftar_kelompok" %>

<div style="display: none; height: 1000px" id="DaftarKelompok_info">
    <table>
        <tr>
            <th><br />
                DAFTAR KELOMPOK
            </th>
        </tr>
        <tr>
            <td>
                <p>Guru sekolah boleh mendaftarkan secara berkelompok melebih 50 orang murid.</p>
                <p>1. Sila muat turun fail  Excel mengikut  format berikut; 
                <b><a href="download/daftar_kelompok_template.xlsx">Excel XLSX Format</a></b><br />
                
                </p>
                <p>2. Masukkan maklumat yang diperlukan bagi murid <b>BAHARU</b> sahaja.</p>
                <p>3. Sila ikut format yang telah ditetapkan. Contoh juga disertakan.</p>
                <p>4. Jangan tukar nama <b>'HEADER'</b> bagi setiap lajur yang telah ditetapkan.</p>
                <p>5. Pastikan turutan nombor pada kolum <b>'BIL'</b> betul.</p>
                <p>6. Sila rujuk tab <b>'reference'</b> sebagai panduan melengkapkan maklumat.</p>
                <p>7. Email ke <asp:Label Font-Bold="true" ID="lblDaftarEmail" runat="server" Text=""></asp:Label>. Tarikh Tutup pendaftaran berkelompok:<asp:Label Font-Bold="true" ID="lblUKM1EmailDate" runat="server" Text=""></asp:Label></p>
                <p>8. Fail akan diproses dalam masa 2-3 hari bekerja. Tuan/Puan akan dimaklumkan setelah fail diproses.</p>
            </td>
        </tr>
    </table>
</div>