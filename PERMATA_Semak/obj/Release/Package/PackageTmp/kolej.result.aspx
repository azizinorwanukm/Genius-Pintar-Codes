<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master" CodeBehind="kolej.result.aspx.vb" Inherits="UKM_SEMAKAN.kolej_result" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="default.aspx">
        <img src="images/pp-logo.png" alt="logo" /></a>
    <h2>Selamat Datang ke Laman Semakan PERMATApintar</h2>
    <table id="mycustomtable">
        <tr>
            <th colspan="2">SEMAKAN KELAYAKAN KE KOLEJ PERMATApintar® UKM
            </th>
        </tr>
        <tr>
            <td style="width: 20%;">
                <asp:Label ID="Label5" runat="server" Text="Tahun Kemasukkan:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblPPYear" runat="server" Text="" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 20%;">
                <asp:Label ID="Label1" runat="server" Text="MYKAD\MYKID#:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblMYKAD" runat="server" Text="" Font-Bold="true"></asp:Label>
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
            <td style="vertical-align: top;">
                <asp:Label ID="Label6" runat="server" Text="Kelayakan:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblPPMT" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <asp:Label ID="Label2" runat="server" Text="Program:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblProgram" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <asp:Label ID="Label4" runat="server" Text="Status Tawaran:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblStatusTawaran" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <asp:Panel ID="pnlreason" runat="server">
            <tr>
                <td style="vertical-align: top;">
                    <asp:Label ID="Label7" runat="server" Text="Sebab Penolakan:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblStatusReason" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </asp:Panel>
        <tr>
            <td style="vertical-align: top;">&nbsp;
            </td>
            <td>
                <asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red" Visible="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>

                <asp:Button ID="btnPrint" runat="server" Text="Cetak PDF" CssClass="fbbutton" Enabled="true" />
                <br />
                <asp:HyperLink ID="hyPDF" runat="server" Target="_blank" Visible="false">Klik disini.</asp:HyperLink>
            </td>

        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>
                <asp:Panel ID="pnlDisplay" runat="server">

                    <p> <b>Sila muat turun SURAT JAWAPAN PENERIMAAN dan Polisi Kolej PERMATApintar® UKM jika anda menerima tawaran
                        ini.</b></p>
                    <p><a href="download/kolej2018/DOKUMEN PENDAFTARAN KOLEJ PERMATAPINTAR UKM.zip" target="_blank">Klik disini untuk memuat turun file secara pukal .zip file
                            (3.4MB).</a></p>

                    <p> <b>ATAU sila memuat turun semua fail dibawah</b></p>

                    <p>
                        <a href="download/kolej2018/LAMPIRAN  A JAWAPAN PENERIMAAN @ PENOLAKAN.pdf" target="_blank">LAMPIRAN  A JAWAPAN PENERIMAAN @ PENOLAKAN.pdf</a>
                    </p>
                     <p>
                         <a href="download/kolej2018/LAMPIRAN B BORANG KEBENARAN MENGIKUTI PROGRAM.pdf" target="_blank">LAMPIRAN B BORANG KEBENARAN MENGIKUTI PROGRAM.pdf</a>
                     </p>
                     <p>
                         <a href="download/kolej2018/LAMPIRAN C BORANG MAKLUMAT PELAJAR 2018.pdf" target="_blank">LAMPIRAN C BORANG MAKLUMAT PELAJAR 2018.pdf</a>
                     </p>
                     <p>
                         <a href="download/kolej2018/LAMPIRAN D LAPORAN PEMERIKSAAN KESIHATAN.pdf" target="_blank">LAMPIRAN D LAPORAN PEMERIKSAAN KESIHATAN.pdf</a>
                     </p>
                     <p>
                         <a href="download/kolej2018/LAMPIRAN E BORANG PERAKUAN KEBENARAN MENDAPATKAN RAWATAN.pdf" target="_blank">LAMPIRAN E BORANG PERAKUAN KEBENARAN MENDAPATKAN RAWATAN.pdf</a>
                     </p>
                     <p>
                         <a href="download/kolej2018/LAMPIRAN F AKUAN PEMBAYARAN YURAN PENGAJIAN PELAJAR BAHARU.pdf" target="_blank">LAMPIRAN F AKUAN PEMBAYARAN YURAN PENGAJIAN PELAJAR BAHARU.pdf</a>
                     </p>
                     <p>
                         <a href="download/kolej2018/LAMPIRAN G TAWARAN KOLEJ KEDIAMAN.pdf" target="_blank">LAMPIRAN G TAWARAN KOLEJ KEDIAMAN.pdf</a>
                     </p>
                     <p>
                         <a href="download/kolej2018/LAMPIRAN F1 TATACARA PEMBAYARAN YURAN PELAJAR BAHARU.pdf" target="_blank">LAMPIRAN F1 TATACARA PEMBAYARAN YURAN PELAJAR BAHARU.pdf</a>
                     </p>
                    <p>
                        <a href="download/kolej2018/LAMPIRAN F2 SENARAI YURAN PENGAJIAN PELAJAR BAHARU.pdf" target="_blank">LAMPIRAN F2 SENARAI YURAN PENGAJIAN PELAJAR BAHARU.pdf</a>
                    </p> 
                     <p>
                         <a href="download/kolej2018/SENARAI SEMAK BORANG_SERAH SEMASA PENDAFTARAN.pdf" target="_blank">SENARAI SEMAK BORANG_SERAH SEMASA PENDAFTARAN.pdf</a>
                     </p>
                     <p>
                         <a href="download/kolej2018/PAKAIAN DAN PERALATAN YANG PERLU DIBAWA.pdf" target="_blank">PAKAIAN DAN PERALATAN YANG PERLU DIBAWA.pdf</a>
                     </p>
                     <p>
                         <a href="download/kolej2018/ATURCARA PENDAFTARAN PELAJAR BARU 2018.pdf" target="_blank">ATURCARA PENDAFTARAN PELAJAR BARU 2018.pdf
                            (3.4MB).</a>
                     </p>

                     <p>
                        Tarikh penerimaan dan penolakan tawaran ke Kolej PERMATApintar® UKM ialah
                        <asp:Label ID="lblTarikhTutup" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>.
                        Bagi pelajar yang tidak menjawab tawaran ke Kolej PERMATApintar® UKM sama ada melalui sistem atau tidak mengembalikan borang jawapan penawaran maka tawaran anda telah terbatal secara automatik.
                    </p>
                    <p>
                        Saya mengaku telah membaca, memahami dan mempersetujui polisi dan syarat-syarat yang telah dimeterai.
                    </p>

                </asp:Panel>
                <asp:Panel ID="PnlStatus" runat="server">
                    <p>
                        Sila klik <b>[Terima]</b> jika anda menerima tawaran ke Kolej PERMATApintar® UKM ini dan klik <b>[Tolak]</b> serta isikan kenapa jika anda menolak tawaran ke Kolej PERMATApintar® UKM ini.
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
                <asp:Panel ID="pnltolak" runat="server">
                    <p>
                        <b>Sila hubungi Kolej PERMATApintar® UKM jika ingin MENERIMA semula tawaran.</b> 
                    </p>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text="System Message..."></asp:Label>
    </div>
    <asp:Label ID="lblUKM3ID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblStudentID" runat="server" Text="1" Visible="false"></asp:Label>

    <asp:Label ID="lblSchoolName" runat="server" Text="2" Visible="false"></asp:Label>
    <asp:Label ID="lblSchoolAddress" runat="server" Text="3" Visible="false"></asp:Label>
    <asp:Label ID="lblSchoolPostcode" runat="server" Text="4" Visible="false"></asp:Label>
    <asp:Label ID="lblSchoolCity" runat="server" Text="5" Visible="false"></asp:Label>
    <asp:Label ID="lblSchoolState" runat="server" Text="6" Visible="false"></asp:Label>

    <asp:Label ID="lblStudentAddress1" runat="server" Text="2" Visible="false"></asp:Label>
    <asp:Label ID="lblStudentAddress2" runat="server" Text="2" Visible="false"></asp:Label>
    <asp:Label ID="lblStudentPostcode" runat="server" Text="2" Visible="false"></asp:Label>
    <asp:Label ID="lblStudentCity" runat="server" Text="2" Visible="false"></asp:Label>
    <asp:Label ID="lblStudentState" runat="server" Text="2" Visible="false"></asp:Label>

    <asp:Label ID="lblPPCSTarikhProgram" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblStatusDate" runat="server" Text="7" Visible="false"></asp:Label>
    <asp:Label ID="lblSQLDebug" runat="server" Text="lblSQLDebug" Visible="false"></asp:Label>
</asp:Content>
