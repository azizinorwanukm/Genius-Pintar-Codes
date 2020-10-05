<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm1.default.Master" CodeBehind="default.simple.aspx.vb" Inherits="permatapintar.default_simple" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table class="fbsection">
        <tr class="fbsection_header">
            <td colspan="2">UJIAN UKM1. KEMPEN 1Hari 1Sekolah 1Pelajar. Sila beritahu kawan disebelah anda mengenai
                    UJIAN UKM1!
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="border: 1px solid #dd3c10; width: 100%;">
                    <tr>
                        <td colspan="3" style="text-align: left; font-weight: bold;">MAKLUMAT INI DIPERLUKAN. SILA SEDIAKANNYA SEBELUM KE BILIK KOMPUTER.
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; font-weight: bold;">1. Maklumat Pelajar
                        </td>
                        <td style="text-align: left; font-weight: bold;">2. Carian Maklumat Sekolah
                        </td>
                        <td style="text-align: left; font-weight: bold;">3. Kemaskini Maklumat Sekolah
                        </td>
                        <td style="text-align: left; font-weight: bold;">4. Maklumat Pelajar dan Maklumat Sekolah
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            <a href="pic/maklumat-pelajar.png" target="_blank">
                                <img src="pic/maklumat-pelajar-small.jpg" alt="pelajar" width="200px" height="150px" /></a>
                        </td>
                        <td style="text-align: left; vertical-align: top; font-weight: bold;">
                            <a href="pic/maklumat-sekolah-01.png" target="_blank">
                                <img src="pic/maklumat-sekolah-01-small.jpg" alt="pelajar" width="200px" height="150px" /></a>
                        </td>
                        <td style="text-align: left; vertical-align: top; font-weight: bold;">
                            <a href="pic/maklumat-sekolah-02.png" target="_blank">
                                <img src="pic/maklumat-sekolah-02-small.jpg" alt="pelajar" width="200px" height="150px" /></a>
                        </td>
                        <td style="text-align: left; vertical-align: top; font-weight: bold;">
                            <a href="pic/maklumat-pelajar-all.png" target="_blank">
                                <img src="pic/maklumat-pelajar-all-small.jpg" alt="pelajar" width="200px" height="150px" /></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">* <b>Ini adalah contoh maklumat keseluruhan pelajar yang dikehendaki. <a href="pic/maklumat-pelajar-all.png"
                target="_blank">Klik di sini.</a></b>
            </td>
        </tr>
        <%--<tr><td colspan="2">
            *Jika Pelajar / Guru / Ibu bapa  tidak memilih sekolah sedia ada dalam senarai database KPM dan MARA, maka pelajar tidak akan dipertimbangkan untuk ke ujian UKM2. Kod XXX hanya untuk <b>sekolah antarabangsa, dan persendirian sahaja.</b>
            </td></tr>--%>
        <tr>
            <td colspan="2">&nbsp;
            </td>
        </tr>
    </table>

    <table class="fbform">
        <tr class="fbform_header">
            <td>Warganegara Malaysia
            </td>
        </tr>
        <tr>
            <td>*MYKAD/MYKID#:
                    <asp:TextBox ID="txtMYKAD" runat="server" Width="250px" MaxLength="25"></asp:TextBox>&nbsp;
                    <asp:Label ID="lblMsg" runat="server" CssClass="labelMsgRed" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Pilihan Bahasa Ujian: &nbsp;<asp:RadioButton ID="BM" runat="server" Text="Bahasa Malaysia"
                GroupName="myLanguage" Checked="true" />
                <asp:RadioButton ID="BI" runat="server" Text="English" GroupName="myLanguage" /><br />
                <asp:Label ID="Label1" runat="server" Text="[Ujian akan berdasarkan bahasa pilihan. Tidak boleh tukar bahasa selepas pilihan dibuat.]"
                    CssClass="labelMsg"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="fbform_sap">&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnNext" runat="server" Text="Seterusnya >>" CssClass="fbbutton"
                    Visible="true" />
                <asp:LinkButton ID="lnkNext" runat="server" Font-Bold="true" Visible="false">Seterusnya >></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td class="fbform_sap">&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <b>ARAHAN:</b>
                <ul>
                    <li><b><a href="pic/sijil_kelahiran02v1.jpg" target="_blank">Contoh Sijil Kelahiran.
                            Klik di sini</a></b></li>
                    <%--<li><b><a href="ukm1.requestid.aspx" target="_self">Tidak mempunyai MYKID. Klik di sini</a></b></li>--%>
                    <li>
                        <asp:Label ID="Label2" runat="server" CssClass="labelMsg" Text="Contoh: 123456121234 untuk mengelakkan kekeliruan. Tanpa tanda '-' atau ruang kosong."></asp:Label></li>
                    <li>
                        <asp:Label ID="Label3" runat="server" Text="Masukkan MYKAD/MYKID# dan tekan Seterusnya >>"
                            CssClass="labelMsg"></asp:Label></li>
                    <li>
                        <asp:Label ID="Label4" runat="server" Text="Jika anda pernah menduduki ujian ini dan logout/talian terputus, anda akan dibawa ke soalan terakhir anda."
                            CssClass="labelMsg"></asp:Label></li>
                    <li>
                        <asp:Label ID="Label6" runat="server" Text="Pelajar baru akan dibawa ke laman maklumat peribadi anda."
                            CssClass="labelMsg"></asp:Label></li>
                </ul>
            </td>
        </tr>
        <tr>
            <td>
                <div class="warning" id="div1" runat="server">
                    • Semasa mengisi maklumat pelajar, sekolah dan ibu bapa / penjaga, jika didapati
                        maklumat palsu, walaupun pelajar melepasi skor kelayakan, maka pelajar tidak layak
                        menduduki ujian UKM2 atau USIM-1.<br />
                    • Sekiranya orang lain yang menjawab soalan bagi pihak pelajar atau pelajar meniru,
                        maka pelajar akan terperangkap semasa ujian saringan kedua UKM2 yang dilakukan dengan
                        pengawasan.<br />
                    • Sila <b>JUJUR dan BERTANGGUNGJAWAB</b> pada diri sendiri semasa mengisi maklumat
                        dan menjawab soalan.<br />
                    •
                    <asp:Label ID="lblPCInfo" runat="server" Text="" Font-Bold="true"></asp:Label><br />
                    Maklumat ini akan disimpan bagi siasatan lanjut jika terdapat aduan penyalahgunaan MYKAD. 
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
