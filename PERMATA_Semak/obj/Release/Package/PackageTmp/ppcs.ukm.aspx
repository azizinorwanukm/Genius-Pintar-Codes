<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master" CodeBehind="ppcs.ukm.aspx.vb" Inherits="UKM_SEMAKAN.ppcs_ukm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="default.aspx">
        <img src="images/pp-logo.png" alt="logo" /></a>
    <h2>Selamat Datang ke Laman Semakan PERMATApintar</h2>
    <table id="mycustomtable">
        <tr>
            <th colspan="2">
                <asp:Label ID="lblPPCSTitle" runat="server" Text="" Font-Bold="true"></asp:Label>
            </th>
        </tr>
        <tr>
            <td style="width: 20%;">
                <asp:Label ID="Label5" runat="server" Text="SESI PPCS:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblPPCSSessi" runat="server" Text="" Font-Bold="true"></asp:Label>
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
                <asp:Label ID="Label2" runat="server" Text="Kod Kursus:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCourseCode" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <asp:Label ID="Label4" runat="server" Text="Nama Kursus:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCourseNameBM" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <asp:Label ID="Label7" runat="server" Text="Status PPCS:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblPPCSStatus" runat="server" Text="" Visible="true" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <asp:Label ID="Label6" runat="server" Text="Status Tawaran:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblStatusTawaran" runat="server" Text="" Font-Bold="true"></asp:Label>&nbsp;&nbsp;<asp:Label ID="lblStatusDate" runat="server" Text=""></asp:Label><br />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <asp:Label ID="Label8" runat="server" Text="Status Penerimaan:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblIsPosMsg" runat="server" Text=""></asp:Label>
            </td>
        </tr>

        <tr>
            <td style="vertical-align: top;">&nbsp;
            </td>
            <td>
                <asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label><br />

            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>
                <asp:Button ID="btnPrint" runat="server" Text="Cetak PDF" CssClass="fbbutton" Enabled="true" Visible="false" />
            </td>
        </tr>
        <tr>
            <td><a href="https://get.adobe.com/reader/" target="_blank">
                <img id="imgPDF" src="images/get-adobe-reader.gif" alt="get-adobe" style="height: 25px; width: 150px; vertical-align: bottom;" runat="server" /></a></td>
            <td>
                <asp:HyperLink ID="hyPDF" runat="server" Target="_blank" Visible="false">Klik disini.</asp:HyperLink>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>
                <asp:Panel ID="pnlDisplay" runat="server" Visible="false">
                    <p>
                        <b>TAWARAN MENGIKUTI PROGRAM PERKHEMAHAN CUTI SEKOLAH</b><br />
                        Tarikh Program :
                        <asp:Label ID="lblPPCSTarikhProgram" runat="server" Text=""></asp:Label><br />
                        Lokasi Program :
                        <asp:Label ID="lblPPCSLokasiProgram" runat="server" Text=""></asp:Label><br />
                    </p>
                    <p>
                        <b>MAKLUMAT PENDAFTARAN</b><br />
                    </p>
                    <table style="border: none 0px; width: 100%;">
                        <tr>
                            <td>Tarikh:</td>
                            <td>
                                <asp:Label ID="lblPPCSTarikhDaftar" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Masa:</td>
                            <td>
                                <asp:Label ID="lblPPCSMasa" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Tempat:</td>
                            <td>
                                <asp:Label ID="lblPPCSTempatDaftar" runat="server" Text=""></asp:Label></td>
                        </tr>
                    </table>
                    <p>
                        Saya mengaku telah membaca, memahami dan mempersetujui polisi dan syarat-syarat
                        yang telah dimeterai. Saya juga bersetuju untuk tidak membuat sebarang tuntutan
                        atau saman kepada pihak Pusat sepanjang tempoh program perkhemahan dijalankan.
                    </p>
                    <p>
                        Sila klik <b>[Terima]</b> jika anda menerima tawaran ke PPCS ini dan klik <b>[Tolak]</b>
                        serta isikan kenapa jika anda menolak tawaran ke PPCS ini.
                    </p>
                    <p>
                        Sebab tawaran ditolak:<asp:TextBox ID="txtStatusReason" runat="server" Width="350px"
                            MaxLength="250"></asp:TextBox>
                    </p>
                    <p>
                        <b>Status Tawaran:</b><br />
                        <asp:Button ID="btnTerima" runat="server" Text="Terima " CssClass="fbbutton" />&nbsp;
                        <asp:Button ID="btnTolak" runat="server" Text="Tolak " CssClass="fbbutton" />&nbsp;
                    </p>
                    <p>
                        Sila muat turun dokumen yang diperlukan jika anda menerima tawaran
                        ini.&nbsp;<a href="download\ppcs201712\PPCS_201712_UKM.zip" target="_blank">Klik disini
                            (2.5MB).</a>
                    </p>
                    <p>
                        a) Polisi dan Borang perlu dihantar melalui cara emel:
 Mengimbas/ mengambil gambar borang dan polisi yang telah dilengkapkan terlebih dahulu sebelum dihantar melalui emel <b>pcspermata@gmail.com</b>. Urusetia akan membuat semakan sama ada borang lengkap atau tidak dan akan dihubungi sekiranya tidak lengkap.
                    </p>
                    <p>
                        b) Dokumen original  boleh dihantar sendiri ke pejabat PPCS  atau dipos kepada:<br />
                        Ketua Unit Program Perkhemahan Cuti Sekolah<br />
                        Pusat PERMATApintar Negara<br />
                        Universiti Kebangsaan Malaysia<br />
                        43600 UKM Bangi<br />
                        Selangor Darul Ehsan<br />
                    </p>
                    <p>
                        Sebarang maklumat atau pertanyaan lanjut boleh menghubungi urusetia PPCS di talian <b>0389217521/ 7524/ 7532/ 7533</b>.
                    </p>
                </asp:Panel>
                <asp:Panel ID="pnlExpired" runat="server">
                    <p>
                        Tarikh penerimaan dan penolakan tawaran ke PPCS ialah
                        <asp:Label ID="lblTarikhTutup" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>.
                        Bagi pelajar yang tidak menjawab tawaran ke PPCS sama ada melalui sistem atau tidak mengembalikan borang jawapan penawaran maka tawaran anda telah terbatal secara automatik. Sebarang surat-menyurat tidak akan dilayan. 
                    </p>
                    <p>
                        Bagi pelajar yang telah berbuat demikian, sekiranya tidak hadir tanpa alasan yang kukuh ke Program Perkhemahan Cuti Sekolah setelah menyatakan persetujuan, maka pelajar akan <b>disenaraihitamkan</b> daripada mengambil ujian UKM2 dan Program Perkhemahan Cuti Sekolah pada tahun berikutnya.
                    </p>

                </asp:Panel>
            </td>
        </tr>
    </table>
    <div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text="Mesej sistem..."></asp:Label>
    </div>
    <asp:Label ID="lblStudentID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblSchoolName" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblSchoolAddress" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblSchoolPostcode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblSchoolCity" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblSchoolState" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblIsPos" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblIsScan" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblPPCSPPNTradeMark" runat="server" Text="" Visible="false"></asp:Label>

</asp:Content>
