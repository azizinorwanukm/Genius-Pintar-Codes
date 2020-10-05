<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ukm1_masalah_kod_sekolah.ascx.vb" Inherits="permatapintar.ukm1_masalah_kod_sekolah" %>

<div style="display: none; height: 1000px" id="kodSekolah_info">
    <table>
        <tr>
            <th><br />
                MASALAH KOD SEKOLAH
            </th>
        </tr>
        <tr>
            <td>
                <p>a) Bagi sekolah-sekolah Kementerian Pendidikan dan MRSM, gunakan panduan carian sekolah diberi semasa mengisi maklumat sekolah.  
                    Sila pastikan anda memilih negeri terlebih dahulu dan gunakan satu perkataan kata kunci yang unik (tidak perlu nama penuh sekolah). </p>
                <p>b) Jika tidak menjumpai sekolah, sila berikan maklumat sekolah (nama sekolah, alamat lengkap sekolah, telefon, faks dan emel sekolah)
                    kepada permatapintar@ukm.edu.my untuk didaftarkan dan diberi kod sekolah.</p>
                <p>c) Sebarang pertanyaan tentang ujian ini boleh dikemukakan kepada alamat permatapintar@ukm.edu.my. Sila sertakan maklumat berikut di dalam emel tuan/puan:<br />
                    1. *MYKAD/MYKID#:<br />
                    2. *Nama Penuh:<br />
                </p>
                <p>
                    <asp:Table runat="server">
                        <asp:TableRow>
                            <asp:TableCell>
                                <b> Panduan mencari nama sekolah.</b> <br />
                                * klik untuk besarkan gambar
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                 <a href="pic/maklumat-sekolah-01.png" target="_blank">
                                <img src="pic/maklumat-sekolah-01-small.jpg" alt="pelajar" width="200px" height="150px" /></a>
                            </asp:TableCell>                          
                        </asp:TableRow>
                    </asp:Table>
<%--                    <td style="text-align: left; vertical-align: top; font-weight: bold;">
                            <a href="pic/maklumat-sekolah-01.png" target="_blank">
                                <img src="pic/maklumat-sekolah-01-small.jpg" alt="pelajar" width="200px" height="150px" /></a>
                        </td>--%>
                </p>
            </td>
        </tr>
    </table>
</div>