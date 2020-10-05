<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master"
    CodeBehind="ukm2.aspx.vb" Inherits="UKM_SEMAKAN.ukm2" %>

<%@ Register Src="mycontrol/state_pusatujianstatus.ascx" TagName="state_pusatujianstatus" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="default.aspx">
        <img src="images/pp-logo.png" alt="logo" /></a>
    <h3>Selamat Datang ke Laman Semakan PERMATApintar</h3>

    <asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red" ></asp:Label>
    <table id="mycustomtable">
        <tr>
            <th colspan="2">SEMAKAN KELAYAKAN KE UJIAN UKM2 TAHUN <asp:Label ID="lblExamYear" runat="server" Text=""></asp:Label>
            </th>
        </tr>
        <tr>
            <td style="width: 20%;">
                <asp:Label ID="Label1" runat="server" Text="MYKAD\MYKID#:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMYKAD" runat="server" Text="" Width="150px"></asp:TextBox>&nbsp;<asp:Button
                    ID="btnSemak" runat="server" Text="Semak" CssClass="fbbutton" />&nbsp;<asp:Button
                        ID="btnPrint" runat="server" Text="Cetak PDF" CssClass="fbbutton" Enabled="false" />
                &nbsp;
                <br />
                <asp:HyperLink ID="hyPDF" runat="server" Target="_blank" Visible="false">Klik disini.</asp:HyperLink>
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
                <asp:Label ID="Label2" runat="server" Text="Pusat Ujian:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblPusatUjian" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Tarikh Ujian<br/>[DD-MM-YYYY]:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTarikhUjian" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="Waktu Ujian:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSessiUKM2" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>
                <asp:Panel ID="pnlDisplay" runat="server" Visible="false">
                    <div style="font-weight: bold; font-size: large; color: Red">
                        Bagi pelajar yang LAYAK sila semak dari masa ke semasa Pusat Ujian, Tarikh Ujian dan Sesi. Pertukaran mungkin akan dibuat pada saat akhir sebelum ujian dijalankan.
                    </div>
                    <hr />
                    <p>
                        Sila pastikan pengenalan diri yang sah seperti MYKAD/MYKID/Sijil Kelahiran perlu
                        dibawa dan dikemukakan kepada Pengawas di pusat ujian. Kegagalan anda untuk membawa
                        dokumen-dokumen tersebut mengakibatkan anda tidak dibenarkan menduduki ujian.
                    </p>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <uc1:state_pusatujianstatus ID="state_pusatujianstatus1" runat="server" />
            </td>
        </tr>
    </table>
    <div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text="Sila masukkan MYKAD/MYKID# sepertimana didaftarkan semasa mengambil Ujian UKM1."></asp:Label>
    </div>
    <asp:Label ID="lblStudentID" runat="server" Text="1" Visible="false"></asp:Label>
    <asp:Label ID="lblSchoolName" runat="server" Text="2" Visible="false"></asp:Label>
    <asp:Label ID="lblSchoolAddress" runat="server" Text="3" Visible="false"></asp:Label>
    <asp:Label ID="lblSchoolPostcode" runat="server" Text="4" Visible="false"></asp:Label>
    <asp:Label ID="lblSchoolCity" runat="server" Text="5" Visible="false"></asp:Label>
    <asp:Label ID="lblSchoolState" runat="server" Text="6" Visible="false"></asp:Label>
    <asp:Label ID="lblPusatState" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>
