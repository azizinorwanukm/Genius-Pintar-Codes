<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master" CodeBehind="kolej.result.aspx.vb" Inherits="UKM_SEMAKAN.kolej_result" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="default.aspx">
        <img src="images/pp-logo.png" alt="logo" /></a>
    <h2>Selamat Datang ke Laman Semakan GENIUS@Pintar UKM</h2>
    <table id="mycustomtable">
        <tr>
            <th colspan="2">SEMAKAN KELAYAKAN KE KOLEJ GENIUS@Pintar UKM
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

                    <p> <b>Sila muat turun borang yang di sediakan di bawah jika anda menerima tawaran ke Kolej GENIUS@Pintar UKM
                        ini.</b></p>
                    <p><a href="download/kolej2021/DOKUMEN PENDAFTARAN KOLEJ GENIUS@Pintar UKM 2021.zip" target="_blank">Klik disini untuk memuat turun file secara pukal .zip file</a></p>
                    <p><a href="download/kolej2021/DOKUMEN PENDAFTARAN KOLEJ GENIUS@Pintar UKM 2021.rar" target="_blank">Klik disini untuk memuat turun file secara pukal .rar file</a></p>

                    <p> <b>ATAU sila memuat turun semua fail dibawah</b></p>

                    <p>
                        <a href="download/kolej2021/LAMPIRAN A POLISI & PERATURAN KGPN.pdf" target="_blank">LAMPIRAN A POLISI & PERATURAN KGPN</a>
                    </p>
                     <p>
                         <a href="download/kolej2021/LAMPIRAN B BORANG KEBENARAN MENGIKUTI PENGAJIAN.pdf" target="_blank">LAMPIRAN B BORANG KEBENARAN MENGIKUTI PENGAJIAN</a>
                     </p>
                     <p>
                         <a href="download/kolej2021/LAMPIRAN C BORANG MAKLUMAT MURID 2021.pdf" target="_blank">LAMPIRAN C BORANG MAKLUMAT MURID 2021</a>
                     </p>
                     <p>
                         <a href="download/kolej2021/LAMPIRAN D LAPORAN PEMERIKSAAN KESIHATAN.pdf" target="_blank">LAMPIRAN D LAPORAN PEMERIKSAAN KESIHATAN</a>
                     </p>
                     <p>
                         <a href="download/kolej2021/LAMPIRAN D1 BORANG PERAKUAN KEBENARAN MENDAPATKAN RAWATAN.pdf" target="_blank">LAMPIRAN D1 BORANG PERAKUAN KEBENARAN MENDAPATKAN RAWATAN</a>
                     </p>
                     <p>
                         <a href="download/kolej2021/LAMPIRAN E AKUAN PEMBAYARAN YURAN PENGAJIAN.pdf" target="_blank">LAMPIRAN E AKUAN PEMBAYARAN YURAN PENGAJIAN</a>
                     </p>
                     <p>
                         <a href="download/kolej2021/LAMPIRAN E1 TATACARA PEMBAYARAN YURAN PELAJAR BAHARU.pdf" target="_blank">LAMPIRAN E1 TATACARA PEMBAYARAN YURAN PELAJAR BAHARU</a>
                     </p>
                     <p>
                         <a href="download/kolej2021/LAMPIRAN E2 SENARAI YURAN PENGAJIAN MURID BAHARU.pdf" target="_blank">LAMPIRAN E2 SENARAI YURAN PENGAJIAN MURID BAHARU</a>
                     </p>
                    <p>
                        <a href="download/kolej2021/LAMPIRAN F BORANG AKUJANJI KOLEJ KEDIAMAN.pdf" target="_blank">LAMPIRAN F BORANG AKUJANJI KOLEJ KEDIAMAN</a>
                    </p> 
                     <p>
                         <a href="download/kolej2021/LAMPIRAN F1 PAKAIAN DAN PERALATAN YANG PERLU DIBAWA OLEH MURID.pdf" target="_blank">LAMPIRAN F1 PAKAIAN DAN PERALATAN YANG PERLU DIBAWA OLEH MURID</a>
                     </p>
                     <p>
                         <a href="download/kolej2021/SENARAI SEMAK BORANG_SERAH SEMASA PENDAFTARAN.pdf" target="_blank">SENARAI SEMAK BORANG SERAH SEMASA PENDAFTARAN</a>
                     </p>
                    <%-- <p>
                         <a href="download/kolej2021/ATURCARA PENDAFTARAN PELAJAR BARU 2018.pdf" target="_blank">ATURCARA PENDAFTARAN PELAJAR BARU 2018</a>
                     </p>--%>

                     <p>
                        Tarikh penerimaan dan penolakan tawaran ke Kolej GENIUS@Pintar UKM ialah
                        <asp:Label ID="lblTarikhTutup" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>.
                        Bagi pelajar yang tidak menjawab tawaran ke Kolej GENIUS@Pintar UKM sama ada melalui sistem atau tidak mengembalikan borang jawapan penawaran maka tawaran anda telah terbatal secara automatik.
                    </p>
                    <p>
                        Saya mengaku telah membaca, memahami dan mempersetujui polisi dan syarat-syarat yang telah dimeterai.
                    </p>

                </asp:Panel>
                <asp:Panel ID="PnlStatus" runat="server">
                    <p>
                        Sila klik <b>[Terima]</b> jika anda menerima tawaran ke Kolej GENIUS@Pintar UKM ini dan klik <b>[Tolak]</b> serta isikan kenapa jika anda menolak tawaran ke Kolej GENIUS@Pintar UKM ini.
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
                        <b>Sila hubungi Kolej GENIUS@Pintar UKM jika ingin MENERIMA semula tawaran.</b> 
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
