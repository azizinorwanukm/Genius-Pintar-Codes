<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master" CodeBehind="kolej_tahap1.aspx.vb" Inherits="UKM_SEMAKAN.kolej_tahap1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="default.aspx">
        <img src="images/pp-logo.png" alt="logo" /></a>
    <h2>Selamat Datang ke Laman Semakan PERMATApintar</h2>
    <table id="mycustomtable">
        <tr>
            <th colspan="2">SEMAKAN KELAYAKAN KE Kolej PERMATApintar TAHAP 1
            </th>
        </tr>
        <tr>
            <td style="width: 20%;">
                <asp:Label ID="Label5" runat="server" Text="Tahun Kemasukkan:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblPPYear" runat="server" Text="2015" Font-Bold="true"></asp:Label>
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
                        <asp:Label ID="lblTarikhTutup" runat="server" Text="31 OKTOBER 2014" ForeColor="Red" Font-Bold="true"></asp:Label>.
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
