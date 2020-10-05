<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master"
    CodeBehind="ppcs.aspx.vb" Inherits="UKM_SEMAKAN.ppcs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="default.aspx">
        <img src="images/pp-logo.png" alt="logo" /></a>
    <h2>
        Selamat Datang ke Laman Semakan PERMATApintar</h2>
    <table id="mycustomtable">
        <tr>
            <th colspan="2">
                SEMAKAN KELAYAKAN KE PROGRAM PERKHEMAHAN CUTI SEKOLAH (PPCS)
            </th>
        </tr>
        <tr>
            <td style="width: 20%;">
                <asp:Label ID="Label5" runat="server" Text="SESSI PPCS:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblPPCSDate" runat="server" Text="" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 20%;">
                <asp:Label ID="Label1" runat="server" Text="MYKAD\MYKID#:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMYKAD" runat="server" Text="" Width="200px"></asp:TextBox>
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
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
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
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Panel ID="pnlDisplay" runat="server" Visible="false">
                    <p>
                        <b>STATUS PELAJAR YANG MENERIMA DAN MENOLAK TAWARAN</b></p>
                    <p>
                        Tarikh Program : 30 November 2014 - 19 Disember 2014<br />
                        Lokasi Program : Pusat PERMATApintar™ Negara, Universiti Kebangsaan Malaysia.<br />
                    </p>
                    <p>
                        <b>MAKLUMAT PENDAFTARAN</b><br />
                        Tarikh : 30 November 2014 (Ahad)<br />
                        Masa : 9.00 pagi – 12.00 tengahari<br />
                        Tempat : Lobi Auditorium, Pusat PERMATApintar™ Negara, UKM<br />
                        Yuran : RM 135.00 (Baharu)/ RM 85.00 (Lama)<br />
                    </p>
                    <hr />
                    <p>
                        Saya mengaku telah membaca, memahami dan mempersetujui polisi dan syarat-syarat
                        yang telah dimeterai. Saya juga bersetuju untuk tidak membuat sebarang tuntutan
                        atau saman kepada pihak Pusat sepanjang tempoh program perkhemahan dijalankan.</p>
                    <p>
                        Sila klik <b>[Terima]</b> jika anda menerima tawaran ke PPCS ini dan klik <b>[Tolak]</b>
                        serta isikan kenapa jika anda menolak tawaran ke PPCS ini.</p>
                    <p>
                        Sebab tawaran ditolak:<asp:TextBox ID="txtStatusReason" runat="server" Width="350px"
                            MaxLength="250"></asp:TextBox>
                    </p>
                    <p>
                        <asp:Button ID="btnTerima" runat="server" Text="Terima " CssClass="fbbutton" />&nbsp;
                        <asp:Button ID="btnTolak" runat="server" Text="Tolak " CssClass="fbbutton" />&nbsp;</p>
                    <p>
                        Sila muat turun SURAT JAWAPAN PENERIMAAN dan Polisi PPCS jika anda menerima tawaran
                        ini.<a href="download\PPCS201406\BORANG-PPCS-201406.zip" target="_blank">Klik disini
                            (1.3MB).</a></p>
                </asp:Panel>
                <asp:Panel ID="pnlExpired" runat="server">
                    <p>
                        Tarikh penerimaan dan penolakan tawaran ke PPCS ialah
                        <asp:Label ID="lblTarikhTutup" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>.
                        Bagi pelajar yang tidak menjawab tawaran ke PPCS sama ada melalui sistem atau tidak
                        mengembalikan borang jawapan penawaran maka tawaran anda telah terbatal secara automatik.
                        Sebarang surat-menyurat tidak akan dilayan.</p>
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
        <asp:Label ID="lblMsg" runat="server" Text="System Message..."></asp:Label></div>
    <asp:Label ID="lblStudentID" runat="server" Text="1" Visible="false"></asp:Label>
    
    <asp:Label ID="lblSchoolName" runat="server" Text="2" Visible="false"></asp:Label>
    <asp:Label ID="lblSchoolAddress" runat="server" Text="3" Visible="false"></asp:Label>
    <asp:Label ID="lblSchoolPostcode" runat="server" Text="4" Visible="false"></asp:Label>
    <asp:Label ID="lblSchoolCity" runat="server" Text="5" Visible="false"></asp:Label>
    <asp:Label ID="lblSchoolState" runat="server" Text="6" Visible="false"></asp:Label>


    <asp:Label ID="lblStatusDate" runat="server" Text="7" Visible="false"></asp:Label>
</asp:Content>
