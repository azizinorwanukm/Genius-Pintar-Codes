<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master" CodeBehind="ppcs.semak.aspx.vb" Inherits="UKM_SEMAKAN.ppcs_semak" %>

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
                <asp:Label ID="Label1" runat="server" Text="MYKAD\MYKID#:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMYKAD" runat="server" Text="" Width="200px"></asp:TextBox>
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
                <asp:Button ID="btnSemak" runat="server" Text=" Semak " CssClass="fbbutton" />&nbsp;<asp:Button
                    ID="btnPrint" runat="server" Text="Cetak PDF" CssClass="fbbutton" Enabled="true" Visible="false" />
            </td>
        </tr>

        <tr>
            <td>&nbsp;
            </td>
            <td>
                <asp:Panel ID="pnlExpired" runat="server" Visible="false">
                    <p>
                        Tarikh penerimaan dan penolakan tawaran ke PPCS ialah
                        <asp:Label ID="lblTarikhTutup" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>.
                        Bagi pelajar yang tidak menjawab tawaran ke PPCS sama ada melalui sistem atau tidak
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
        <asp:Label ID="lblMsg" runat="server" Text="Mesej sistem..."></asp:Label>
    </div>
    <asp:Label ID="lblPPSCDateUKM" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblPPSCDateUSIM" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblPPCSStatus" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lblStudentID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblSchoolName" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblSchoolAddress" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblSchoolPostcode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblSchoolCity" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblSchoolState" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblIsPos" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblIsScan" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblDebug" runat="server" Text=""></asp:Label>

</asp:Content>
