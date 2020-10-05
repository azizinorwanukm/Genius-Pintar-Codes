<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master" CodeBehind="ukm2.asasi.semak.aspx.vb" Inherits="UKM_SEMAKAN.ukm2_asasi_semak" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="default.aspx">
        <img src="images/pp-logo.png" alt="logo" /></a>
    <h2>Selamat Datang ke Laman Semakan PERMATApintar</h2>
    <table id="mycustomtable">
        <tr>
            <th colspan="2">
                <asp:Label ID="lblUKM2Title" runat="server" Text="" ForeColor="Red"></asp:Label>
            </th>
        </tr>
        <tr>
            <td style="width: 20%;">
                <asp:Label ID="Label5" runat="server" Text="TAHUN UJIAN:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblUKM2ExamYear" runat="server" Text="" Font-Bold="true"></asp:Label>
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
                <asp:Label ID="Label3" runat="server" Text="Nama Pelajar:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblStudentFullname" runat="server" Text=""></asp:Label>
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
    </table>
    &nbsp;
    <asp:Panel ID="pnlDisplay" runat="server" Visible="false">
        <table id="mycustomtable">
            <tr>
                <td style="width: 20%;">
                    <asp:Label ID="Label9" runat="server" Text="Tarikh Ujian:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblTarikhUjian" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label12" runat="server" Text="Sessi:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblSessiUKM2" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top;">
                    <asp:Label ID="Label11" runat="server" Text="Nota:"></asp:Label>
                </td>
                <td>
                    <b>Sila semak dari masa ke semasa kerana mungkin akan ada perubahan Tarikh, Sesi atau Pusat Ujian.</b>
                </td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Nama Pusat Ujian:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblPusatName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Alamat:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblPusatAddress" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Poskod:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblPusatPostcode" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Bandar:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblPusatCity" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="Negeri:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblPusatState" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Tel#:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblPusatNoTel" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <b>Sila pastikan anda membawa bersama MYKAD/MYKID/Surat Beranak sebagai bukti pengesahan.</b>
                </td>
            </tr>
        </table>
        <br />
    </asp:Panel>
    <div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text="System Message..."></asp:Label>
    </div>
    <asp:Label ID="lblStudentID" runat="server" Text="1" Visible="false"></asp:Label>
    <asp:Label ID="lblPusatCode" runat="server" Text="2" Visible="false"></asp:Label>
    <asp:Label ID="lblUKM2End" runat="server" Text="2" Visible="false"></asp:Label>

    <asp:Label ID="lblStudentAddress1" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblStudentAddress2" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblStudentPostcode" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblStudentCity" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblStudentState" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>
