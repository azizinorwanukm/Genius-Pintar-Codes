<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master" CodeBehind="kolej.aspx.vb" Inherits="UKM_SEMAKAN.kolej" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="default.aspx">
        <img src="images/pp-logo.png" alt="logo" /></a>
    <h2>Selamat Datang ke Laman Semakan PERMATApintar</h2>
    <table id="mycustomtable">
        <tr>
            <th colspan="2">SEMAKAN KELAYAKAN KE Kolej PERMATApintar
            </th>
        </tr>
        <tr>
            <td style="width: 25%;">
                <asp:Label ID="Label5" runat="server" Text="Tahun Kemasukkan:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblPPYear" runat="server" Text="2015" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="MYKAD\MYKID#:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMYKAD" runat="server" Text="" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 20%;">
                <asp:Label ID="Label4" runat="server" Text="No Pendaftaran Pelajar:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblNoPelajar" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 20%;">
                <asp:Label ID="Label3" runat="server" Text="Nama:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblStudentFullname" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 20%;">
                <asp:Label ID="Label2" runat="server" Text="Program:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblProgram" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">&nbsp;
            </td>
            <td>
                <asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>
                <asp:Button ID="btnSemak" runat="server" Text=" Semak " CssClass="fbbutton" />&nbsp;<asp:Button
                    ID="btnPrint" runat="server" Text="Cetak PDF" CssClass="fbbutton" Enabled="true"
                    Visible="false" />
                &nbsp;
                <asp:HyperLink ID="hyPDF" runat="server" Target="_blank" Visible="false">Klik disini.</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>
                <asp:Panel ID="pnlDownload" runat="server" Visible="false">
                    <p>
                        Sila muat turun dokumen berkaitan KOLEJ PERMATApintar dan baca isi kandungannya.<br />
                        <a href="download\kolej2015\kolej_permatapintar2015.zip" target="_blank">Semua PDF dalam format ZIP. Klik disini
                            (3MB).</a>
                    </p>

                    <table style="width: 100%;">
                        <tr style="font-weight:bold;">
                            <td>PDF Format</td>
                            <td>MS Word Format</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HyperLink ID="lnkTawaran" runat="server" Target="_blank">Surat Tawaran Mengikuti Program Pengajian di Kolej PERMATApintar Negara</asp:HyperLink>
                            </td>
                            <td><a href="" target="_blank">NA</a></td>

                            
                        </tr>
                        <tr>
                            <td><a href="download\kolej2015\2015 1_Senarai Semak, Surat Tawaran dan Borang-Borang.pdf" target="_blank">2015 1_Senarai Semak, Surat Tawaran dan Borang-Borang.pdf</a></td>
                            <td><a href="" target="_blank">NA</a></td>
                        </tr>
                        <tr>
                            <td><a href="download\kolej2015\2015 2_Senarai Semak Dokumen.pdf" target="_blank">2015 2_Senarai Semak Dokumen.pdf</a></td>
                            <td><a href="" target="_blank">NA</a></td>
                        </tr>
                        <tr>
                            <td><a href="download\kolej2015\2015 Lampiran A - Surat Jawapan Penerimaan.pdf" target="_blank">2015 Lampiran A - Surat Jawapan Penerimaan.pdf</a></td>
                            <td><a href="" target="_blank">NA</a></td>
                        </tr>
                        <tr>
                            <td><a href="download\kolej2015\2015 Lampiran B - Borang Kebenaran Mengikuti Program.pdf" target="_blank">2015 Lampiran B - Borang Kebenaran Mengikuti Program.pdf</a></td>
                            <td><a href="" target="_blank">NA</a></td>
                        </tr>
                        <tr>
                            <td><a href="download\kolej2015\2015 Lampiran C - Maklumat Pelajar.pdf" target="_blank">2015 Lampiran C - Maklumat Pelajar.pdf</a></td>
                            <td><a href="" target="_blank">NA</a></td>
                        </tr>
                        <tr>
                            <td><a href="download\kolej2015\2015 Lampiran D - Borang Pemeriksaan Kesihatan.pdf" target="_blank">2015 Lampiran D - Borang Pemeriksaan Kesihatan.pdf</a></td>
                            <td><a href="" target="_blank">NA</a></td>
                        </tr>
                        <tr>
                            <td><a href="download\kolej2015\2015 Lampiran E - Akuan Pembayaran Yuran.pdf" target="_blank">2015 Lampiran E - Akuan Pembayaran Yuran.pdf</a></td>
                            <td><a href="" target="_blank">NA</a></td>
                        </tr>
                        <tr>
                            <td><a href="download\kolej2015\2015 Lampiran E1 - Tatacara Pembayaran Yuran.pdf" target="_blank">2015 Lampiran E1 - Tatacara Pembayaran Yuran.pdf</a></td>
                            <td><a href="" target="_blank">NA</a></td>
                        </tr>
                        <tr>
                            <td><a href="download\kolej2015\2015 Lampiran E2 - Senarai Yuran Bayaran.pdf" target="_blank">2015 Lampiran E2 - Senarai Yuran Bayaran.pdf</a></td>
                            <td><a href="" target="_blank">NA</a></td>
                        </tr>
                        <tr>
                            <td><a href="download\kolej2015\2015 Lampiran F - Tawaran Kolej Kediaman.pdf" target="_blank">2015 Lampiran F - Tawaran Kolej Kediaman.pdf</a></td>
                            <td><a href="" target="_blank">NA</a></td>
                        </tr>
                        <tr>
                            <td><a href="download\kolej2015\2015 Lampiran F1 - Pakaian dan Peralatan yang perlu di bawa oleh pelajar.pdf" target="_blank">2015 Lampiran F1 - Pakaian dan Peralatan yang perlu di bawa oleh pelajar.pdf</a></td>
                            <td><a href="" target="_blank">NA</a></td>
                        </tr>
                        <tr>
                            <td><a href="download\kolej2015\ORIENTATION TENTATIVE PROGRAMME 2015.pdf" target="_blank">ORIENTATION TENTATIVE PROGRAMME 2015.pdf</a></td>
                            <td><a href="" target="_blank">NA</a></td>
                        </tr>

                    </table>
                    <p>
                        Saya mengaku telah membaca, memahami dan mempersetujui polisi dan syarat-syarat
                        yang telah dimeterai.
                    </p>
                </asp:Panel>

                <asp:Panel ID="pnlDisplay" runat="server" Visible="false">
                    <p>
                        Sila klik <b>[Terima]</b> jika anda menerima tawaran ke Kolej PERMATApintar ini dan klik <b>[Tolak]</b>
                        serta isikan kenapa jika anda menolak tawaran ke Kolej PERMATApintar ini.
                    </p>
                    <p>
                        Sebab tawaran ditolak:<asp:TextBox ID="txtStatusReason" runat="server" Width="350px"
                            MaxLength="250"></asp:TextBox>
                    </p>
                    <p>
                        <asp:Button ID="btnTerima" runat="server" Text="Terima " CssClass="fbbutton" />&nbsp;
                        <asp:Button ID="btnTolak" runat="server" Text="Tolak " CssClass="fbbutton" />&nbsp;
                    </p>

                </asp:Panel>
                <asp:Panel ID="pnlExpired" runat="server" Visible="false">
                    <p>
                        Tarikh akhir penerimaan dan penolakan tawaran ke Kolej PERMATApintar ialah
                        <asp:Label ID="lblTarikhTutup" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>.
                        Bagi pelajar yang tidak menjawab tawaran ke Kolej PERMATApintar sama ada melalui sistem atau tidak
                        mengembalikan borang jawapan penawaran maka tawaran anda telah terbatal secara automatik.
                        Sebarang surat-menyurat tidak akan dilayan.
                    </p>
                    <p>
                        Bagi pelajar yang telah berbuat demikian, sekiranya tidak hadir tanpa alasan yang
                        kukuh ke Program Perkhemahan Cuti Sekolah setelah menyatakan persetujuan, maka pelajar
                        akan <b>disenaraihitamkan</b> daripada mengambil ujian UKM2 dan Program Perkhemahan
                        Cuti Sekolah pada tahun berikutnya.
                    </p>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
    </div>
    <asp:Label ID="lblStudentID" runat="server" Text="1" Visible="false"></asp:Label>

    <asp:Label ID="lblSchoolName" runat="server" Text="2" Visible="false"></asp:Label>
    <asp:Label ID="lblSchoolAddress" runat="server" Text="3" Visible="false"></asp:Label>
    <asp:Label ID="lblSchoolPostcode" runat="server" Text="4" Visible="false"></asp:Label>
    <asp:Label ID="lblSchoolCity" runat="server" Text="5" Visible="false"></asp:Label>
    <asp:Label ID="lblSchoolState" runat="server" Text="6" Visible="false"></asp:Label>


    <asp:Label ID="lblFatherFullname" runat="server" Text="6" Visible="false"></asp:Label>
    <asp:Label ID="lblStudentAddress1" runat="server" Text="6" Visible="false"></asp:Label>
    <asp:Label ID="lblStudentAddress2" runat="server" Text="6" Visible="false"></asp:Label>
    <asp:Label ID="lblStudentPostcode" runat="server" Text="6" Visible="false"></asp:Label>
    <asp:Label ID="lblStudentCity" runat="server" Text="6" Visible="false"></asp:Label>
    <asp:Label ID="lblStudentState" runat="server" Text="6" Visible="false"></asp:Label>

    <asp:Label ID="lblStatusDate" runat="server" Text="7" Visible="false"></asp:Label>
</asp:Content>
