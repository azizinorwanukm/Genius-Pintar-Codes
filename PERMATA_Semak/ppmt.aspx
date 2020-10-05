<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master"
    CodeBehind="ppmt.aspx.vb" Inherits="UKM_SEMAKAN.ppmt" %>

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
                SEMAKAN KELAYAKAN KE PROGRAM PENDIDIKAN PERMATApintar™(PPPp Negara)
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
            <td style="width: 20%;">
                <asp:Label ID="Label3" runat="server" Text="Nama:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblStudentFullname" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <asp:Label ID="Label4" runat="server" Text="No Pelajar:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblNoPelajar" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <asp:Label ID="Label2" runat="server" Text="Program Pendidikan:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblProgram" runat="server" Text=""></asp:Label>
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
                        <b>MAKLUMAT PENDAFTARAN</b><br />
                        Saudara diminta hadir untuk mendaftar pada tarikh, tempat dan masa seperti berikut:<br />
                        Tarikh Pendaftaran : 20 JANUARI 2014 (ISNIN)<br />
                        Tempat : DEWAN SERBAGUNA<br />
                        Pusat PERMATApintar™ Negara, UKM<br />
                        Masa Pendaftaran : 8:30 PAGI - 11.00 PAGI<br />
                        Taklimat Ibu Bapa: 11:00 PAGI- 1:00 TENGAHARI (DEWAN MAKAN)<br />
                    </p>
                    <hr />
                    <p>
                        Sila klik <b>[Terima]</b> jika anda menerima tawaran ke PROGRAM PENDIDIKAN PERMATApintar™
                        ini dan klik <b>[Tolak]</b> serta isikan kenapa jika anda menolak tawaran ke ini.</p>
                    <p>
                        Sebab tawaran ditolak:<asp:TextBox ID="txtStatusReason" runat="server" Width="350px"
                            MaxLength="250"></asp:TextBox>
                    </p>
                    <p>
                        <asp:Button ID="btnTerima" runat="server" Text="Terima " CssClass="fbbutton" />&nbsp;
                        <asp:Button ID="btnTolak" runat="server" Text="Tolak " CssClass="fbbutton" />&nbsp;</p>
                    <p>
                        Sila muat turun SURAT JAWAPAN dan lain-lain dokumen jika anda menerima jawapan.
                        Maklumat lengkap tentang program akan diberi semasa hari pendaftaran.<a href="download/4p/4P-Negara.zip"
                            target="_blank"> Klik disini (3.8MB).</a></p>
                    <p>
                        Keputusan untuk menerima atau menolak tawaran dengan kadar segera pada atau sebelum
                        16 Januari 2014 (Khamis).</p>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text="System Message..."></asp:Label></div>
        
        PPCSDate: <asp:Label ID="lblPPCSDate" runat="server" Text=""></asp:Label>
        
    <asp:Label ID="lblStudentID" runat="server" Text="1" Visible="false"></asp:Label>
    <asp:Label ID="lblStudentAddress1" runat="server" Text="3" Visible="false"></asp:Label>
    <asp:Label ID="lblStudentAddress2" runat="server" Text="3" Visible="false"></asp:Label>
    <asp:Label ID="lblStudentPostcode" runat="server" Text="4" Visible="false"></asp:Label>
    <asp:Label ID="lblStudentCity" runat="server" Text="5" Visible="false"></asp:Label>
    <asp:Label ID="lblStudentState" runat="server" Text="6" Visible="false"></asp:Label>
    <asp:Label ID="lblStatusDate" runat="server" Text="7" Visible="false"></asp:Label>
</asp:Content>
