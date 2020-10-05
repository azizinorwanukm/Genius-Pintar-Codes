<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master"
    CodeBehind="default.aspx.vb" Inherits="permatapintar._default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="tablelogin" border="0px">
        <tr>
            <td style="width: 30px;">&nbsp;
            </td>
            <td valign="middle">
                <h1>Araken I-PROFILE</h1>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td valign="middle" style="text-align: right;">&nbsp;
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>
                <h2>
                    <asp:Label ID="default_01" runat="server" Text="LOGIN DI SINI" CssClass="lblHeader"></asp:Label></h2>
                <asp:Label ID="default_02" runat="server" Font-Italic="true" Font-Size="Medium" Text=""></asp:Label>
                &nbsp;<br />
                <asp:Button ID="btnenUS" runat="server" Text="English" />&nbsp;<asp:Button ID="btnmsMY"
                    runat="server" Text="B. Malaysia" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>
                <asp:Label ID="default_03" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td style="text-align: left">
                <asp:Label ID="lbllogin_id" runat="server" Text="TEL#"></asp:Label>:
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtMYKAD" Width="200px" MaxLength="20" runat="server" Height="25px"
                    Font-Bold="true" Font-Size="16px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>
                <asp:Button ID="btnLogin" runat="server" Text="Login >>" CssClass="mybutton" />
                &nbsp;<asp:Label ID="lblSelectedLang" runat="server" Text=""></asp:Label>&nbsp;
                <table width="100%" style="border:0px none;">
                    <tr>
                        <td></td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="2">PERHATIAN: Buat masa kini, ujian ini hanya boleh dijalankan menggunakan perisian pelayaran:<br />
                            <ul>
                                <li>
                                    <img src="images/IE.png" alt="IE" />Microsoft Internet Explorer</li>
                                <li>
                                    <img src="images/chrome.png" alt="IE" />Google Chrome</li>
                            </ul>
                            <br />
                            <p>
                                Pastikan yang anda menggunakan perisian yang terkini. Jika perisian lain digunakan, tarik dan lepas
                            pada modul pertama akan menghadapi masalah teknikal.

                            </p>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a href="ukm2.drag.drop.aspx?" target="_blank">Uji Drag & Drop</a>&nbsp;|&nbsp;
                            <a href="ukm2.click.image.aspx?" target="_blank">Uji Click Image</a>&nbsp;|&nbsp;
                            <a href="http://www.speedtest.net/" target="_blank">Menguji Kepantasan Talian.</a>&nbsp;|&nbsp;
                            <a href="ukm2.server.speed.aspx?" target="_blank">Uji Kelajuan Server</a>&nbsp;|&nbsp;
                            <a href="http://windows.microsoft.com/en-us/internet-explorer/download-ie" target="_blank">Muat Turun MS Internet Explorer</a>&nbsp;|&nbsp;
                            <asp:Label ID="lblLang" runat="server" Text=""></asp:Label>&nbsp;|&nbsp;
                            <asp:Label ID="lblSaringanExamYear" runat="server" Text=""></asp:Label>&nbsp;|&nbsp;
                            <asp:Label ID="lblSaringanEnd" runat="server" Text=""></asp:Label>&nbsp;|&nbsp;
                            <asp:Label ID="lblSaringanLocation" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblDebug" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblStudentID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblStudentFullname" runat="server" Text="" Visible="false"></asp:Label>
</asp:Content>
