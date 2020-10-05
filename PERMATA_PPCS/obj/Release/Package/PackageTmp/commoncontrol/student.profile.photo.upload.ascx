<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student.profile.photo.upload.ascx.vb"
    Inherits="permatapintar.student_profile_photo_upload" %>
<table width="100%" border="0px" style="background-color: #eceff6;">
    <tr>
        <td colspan="2">
            VIEW PROFIL PELAJAR &nbsp;
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">
            *MYKAD/MYKID/Surat Beranak#:
        </td>
        <td>
            <asp:Label ID="tokenid" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
            <table>
                <tr>
                    <td>
                        <asp:FileUpload ID="imgUpload" runat="server" />
                        <asp:Button ID="btnSubmit" runat="server" Text="Upload Photo" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Image ID="imgStudent" Style="width: 100px; height: 150px;" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            *Nama Penuh:
        </td>
        <td>
            <asp:Label ID="RespFullname" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            *Jantina:
        </td>
        <td>
            <asp:RadioButton ID="RespGender1" runat="server" Text="Lelaki" GroupName="sex" />
            &nbsp;
            <asp:RadioButton ID="RespGender2" runat="server" Text="Perempuan" GroupName="sex" />
        </td>
    </tr>
    <tr>
        <td>
            *Tarikh Lahir:
        </td>
        <td>
            <select name="RespDOBday" id="RespDOBday" style="width: 50px;" runat="server">
                <option value="00" selected="selected">Hari</option>
                <option value="01">01</option>
                <option value="02">02</option>
                <option value="03">03</option>
                <option value="04">04</option>
                <option value="05">05</option>
                <option value="06">06</option>
                <option value="07">07</option>
                <option value="08">08</option>
                <option value="09">09</option>
                <option value="10">10</option>
                <option value="11">11</option>
                <option value="12">12</option>
                <option value="13">13</option>
                <option value="14">14</option>
                <option value="15">15</option>
                <option value="16">16</option>
                <option value="17">17</option>
                <option value="18">18</option>
                <option value="19">19</option>
                <option value="20">20</option>
                <option value="21">21</option>
                <option value="22">22</option>
                <option value="23">23</option>
                <option value="24">24</option>
                <option value="25">25</option>
                <option value="26">26</option>
                <option value="27">27</option>
                <option value="28">28</option>
                <option value="29">29</option>
                <option value="30">30</option>
                <option value="31">31</option>
            </select>
            <select name="RespDOBmonth" id="RespDOBmonth" style="width: 100px;" runat="server">
                <option value="00" selected="selected">Bulan</option>
                <option value="January">January</option>
                <option value="February">February</option>
                <option value="March">March</option>
                <option value="April">April</option>
                <option value="May">May</option>
                <option value="June">June</option>
                <option value="July">July</option>
                <option value="August">August</option>
                <option value="September">September</option>
                <option value="October">October</option>
                <option value="November">November</option>
                <option value="December">December</option>
            </select>
            <select name="RespDOBYear" id="RespDOBYear" style="width: 95px;" runat="server">
                <option value="0000" selected="selected">Tahun</option>
                <option value="2002">2002</option>
                <option value="2001">2001</option>
                <option value="2000">2000</option>
                <option value="1999">1999</option>
                <option value="1998">1998</option>
                <option value="1997">1997</option>
                <option value="1996">1996</option>
                <option value="1995">1995</option>
            </select>
            &nbsp;<asp:Label ID="lblDOBMsg" runat="server" CssClass="labelMsgRed" Text=""></asp:Label>&nbsp;
            <asp:Label ID="Label1" runat="server" CssClass="labelMsg" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            *Darjah/Tingkatan:
        </td>
        <td>
            <select name="RespForm" id="RespForm" style="width: 200px;" runat="server">
                <option value="" selected="selected"></option>
                <option value="DARJAH 2">DARJAH 2</option>
                <option value="DARJAH 3">DARJAH 3</option>
                <option value="DARJAH 4">DARJAH 4</option>
                <option value="DARJAH 5">DARJAH 5</option>
                <option value="DARJAH 6">DARJAH 6</option>
                <option value="TINGKATAN 1">TINGKATAN 1</option>
                <option value="TINGKATAN 2">TINGKATAN 2</option>
                <option value="TINGKATAN 3">TINGKATAN 3</option>
            </select>
        </td>
    </tr>
    <tr>
        <td>
            *Bangsa:
        </td>
        <td>
            <select name="RespRace" id="RespRace" style="width: 200px;" runat="server">
                <option value="" selected="selected"></option>
                <option value="MELAYU">MELAYU</option>
                <option value="CINA">CINA</option>
                <option value="INDIA">INDIA</option>
                <option value="BUMIPUTERA (SABAH)">BUMIPUTERA (SABAH)</option>
                <option value="BUMIPUTERA (SARAWAK)">BUMIPUTERA (SARAWAK)</option>
                <option value="LAIN-LAIN">LAIN-LAIN</option>
            </select>&nbsp; *Agama:&nbsp;
            <select name="RespReligion" id="RespReligion" style="width: 200px;" runat="server">
                <option value="" selected="selected"></option>
                <option value="ISLAM">ISLAM</option>
                <option value="BUDDHA">BUDDHA</option>
                <option value="HINDU">HINDU</option>
                <option value="KRISTIAN">KRISTIAN</option>
                <option value="LAIN-LAIN">LAIN-LAIN</option>
            </select>
        </td>
    </tr>
    <tr>
        <td style="text-align: left; vertical-align: top;">
            Kebolehan Berbahasa:
        </td>
        <td>
            <table border="0px" style="background-color: #eceff6;">
                <tr>
                    <td style="text-align: left; vertical-align: top;">
                        Pertuturan
                    </td>
                    <td>
                        <asp:CheckBox ID="chkLang1" runat="server" Text="B. Malalaysia" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkLang2" runat="server" Text="B. English" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkLang3" runat="server" Text="B. Mandarin" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkLang4" runat="server" Text="B. Tamil" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkLang5" runat="server" Text="B. Arab" />
                    </td>
                    <td>
                        &nbsp;<asp:Label ID="lblTutur" runat="server" CssClass="labelMsgRed" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; vertical-align: top;">
                        Penulisan
                    </td>
                    <td>
                        <asp:CheckBox ID="chkLangWrite1" runat="server" Text="B. Malalaysia" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkLangWrite2" runat="server" Text="B. English" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkLangWrite3" runat="server" Text="B. Mandarin" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkLangWrite4" runat="server" Text="B. Tamil" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkLangWrite5" runat="server" Text="B. Arab" />
                    </td>
                    <td>
                        &nbsp;<asp:Label ID="lblTulis" runat="server" CssClass="labelMsgRed" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td class="fbform_sap">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td valign="top">
            *Alamat Rumah:
        </td>
        <td>
            <asp:TextBox ID="RespAddress" runat="server" Width="250px" MaxLength="250" TextMode="MultiLine"
                Rows="3"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            *Poskod:
        </td>
        <td>
            <asp:TextBox ID="RespPostcode" runat="server" Width="50px" MaxLength="5"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            *Bandar:
        </td>
        <td>
            <asp:TextBox ID="RespCity" runat="server" Width="250px" MaxLength="100"></asp:TextBox>
            &nbsp; *Negeri:&nbsp;
            <select name="RespState" id="RespState" style="width: 200px;" runat="server">
                <option value=""></option>
                <option value="JOHOR">JOHOR</option>
                <option value="KEDAH">KEDAH</option>
                <option value="KELANTAN">KELANTAN</option>
                <option value="MELAKA">MELAKA</option>
                <option value="NEGERI SEMBILAN">NEGERI SEMBILAN</option>
                <option value="PAHANG">PAHANG</option>
                <option value="PULAU PINANG">PULAU PINANG</option>
                <option value="PERAK">PERAK</option>
                <option value="PERLIS">PERLIS</option>
                <option value="SABAH">SABAH</option>
                <option value="SARAWAK">SARAWAK</option>
                <option value="SELANGOR">SELANGOR</option>
                <option value="TERENGGANU">TERENGGANU</option>
                <option value="WP.KUALA LUMPUR">WP.KUALA LUMPUR</option>
                <option value="WP.LABUAN">WP.LABUAN</option>
                <option value="WP.PUTRAJAYA">WP.PUTRAJAYA</option>
            </select>
        </td>
    </tr>
    <tr>
        <td>
            Email:
        </td>
        <td>
            <asp:TextBox ID="RespEmail" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
        </td>
    </tr>
</table>
<table width="100%" border="0px" style="background-color: #eceff6;">
    <tr>
        <td colspan="2">
            Maklumat Sekolah
        </td>
    </tr>
    <tr>
        <td>
            *Kod Sekolah:
        </td>
        <td>
            <asp:TextBox ID="SchoolCode" runat="server" Width="250px" MaxLength="250"></asp:TextBox>&nbsp;
            &nbsp;
            <%--<asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="fbbutton" />&nbsp;--%>
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            *Nama Sekolah:
        </td>
        <td>
            <asp:TextBox ID="SchoolName" runat="server" Width="250px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            *Alamat Sekolah:
        </td>
        <td>
            <asp:TextBox ID="SchoolAddress" runat="server" Width="250px" MaxLength="250" TextMode="MultiLine"
                Rows="3"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            *Poskod:
        </td>
        <td>
            <asp:TextBox ID="SchoolPostcode" runat="server" Width="50px" MaxLength="5"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            *Bandar:
        </td>
        <td>
            <asp:TextBox ID="SchoolCity" runat="server" Width="250px" MaxLength="100"></asp:TextBox>&nbsp;
            *Negeri:&nbsp;
            <select name="SchoolState" id="SchoolState" style="width: 200px;" runat="server">
                <option value="" selected="selected"></option>
                <option value="JOHOR">JOHOR</option>
                <option value="KEDAH">KEDAH</option>
                <option value="KELANTAN">KELANTAN</option>
                <option value="MELAKA">MELAKA</option>
                <option value="NEGERI SEMBILAN">NEGERI SEMBILAN</option>
                <option value="PAHANG">PAHANG</option>
                <option value="PULAU PINANG">PULAU PINANG</option>
                <option value="PERAK">PERAK</option>
                <option value="PERLIS">PERLIS</option>
                <option value="SABAH">SABAH</option>
                <option value="SARAWAK">SARAWAK</option>
                <option value="SELANGOR">SELANGOR</option>
                <option value="TERENGGANU">TERENGGANU</option>
                <option value="WP KUALA LUMPUR">WP KUALA LUMPUR</option>
                <option value="WP LABUAN">WP LABUAN</option>
                <option value="WP PUTRAJAYA">WP PUTRAJAYA</option>
            </select>
        </td>
    </tr>
    <tr>
        <td>
            *Jenis Sekolah:
        </td>
        <td>
            <select name="SchoolType" id="SchoolType" style="width: 255px;" runat="server">
                <option value="" selected="selected"></option>
                <option value="Kebangsaan">Kebangsaan</option>
                <option value="Jenis Kebangsaan (C)">Jenis Kebangsaan (C)</option>
                <option value="Jenis Kebangsaan (T)">Jenis Kebangsaan (T)</option>
                <option value="SM Kebangsaan">SM Kebangsaan</option>
                <option value="SM Berasrama Penuh">SM Berasrama Penuh</option>
                <option value="SMK Agama">SMK Agama</option>
                <option value="SM agama">SM agama</option>
                <option value="Sekolah Seni">Sekolah Seni</option>
                <option value="Sekolah Sukan">Sekolah Sukan</option>
                <option value="SM Khas">SM Khas</option>
                <option value="SM Teknik">SM Teknik</option>
                <option value="SM+ SR (Model Khas)">SM+ SR (Model Khas)</option>
                <option value="MRSM">MRSM</option>
                <option value="SEK. RENDAH SWASTA">SEK. RENDAH SWASTA</option>
                <option value="SEK. RENDAH NEGERI">SEK. RENDAH NEGERI</option>
                <option value="SEK. MENENGAH NEGERI">SEK. MENENGAH NEGERI</option>
                <option value="SEK. ANTARABANGSA">SEK. ANTARABANGSA</option>
                <option value="SEK. RENDAH AGAMA INTEGRASI">SEK. RENDAH AGAMA INTEGRASI</option>
                <option value="LAIN-LAIN">LAIN-LAIN</option>
            </select>
        </td>
    </tr>
</table>
<table width="100%" border="0px" style="background-color: #eceff6;">
    <tr>
        <td colspan="2">
            Maklumat Ibubapa/Penjaga
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            *Nama Penuh Ibubapa/Penjaga:
        </td>
        <td>
            <asp:TextBox ID="ParentFullname" runat="server" Width="250px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            *Nombor Talipon:
        </td>
        <td>
            <asp:TextBox ID="ParentContactNo" runat="server" Width="250px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 15%;">
            *Pekerjaan Ibubapa/Penjaga:
        </td>
        <td>
            <asp:TextBox ID="ParentJob" runat="server" Width="250px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Tahap Pendidikan:
        </td>
        <td>
            <select name="ParentEdu" id="ParentEdu" style="width: 255px;" runat="server">
                <option value="" selected="selected"></option>
                <option value="Master dan ke-atas">Master dan ke-atas</option>
                <option value="Ijazah">Ijazah</option>
                <option value="Diploma">Diploma</option>
                <option value="Sijil">Sijil</option>
                <option value="STPM">STPM</option>
                <option value="SPM dan ke-bawah">SPM dan ke-bawah</option>
            </select>
        </td>
    </tr>
    <tr>
        <td>
            *Pendapatan Sekeluarga:
        </td>
        <td>
            <select name="ParentSalary" id="ParentSalary" style="width: 255px;" runat="server">
                <option value="" selected="selected"></option>
                <option value="< RM1500">< RM1501</option>
                <option value="> RM1500 < RM3501">> RM1500 < RM3501</option>
                <option value="> RM3500 < RM5500">> RM3500 < RM5501</option>
                <option value="> RM5500">> RM5500</option>
            </select>
        </td>
    </tr>
    <tr>
        <td>
            System ID:
        </td>
        <td>
            <asp:Label ID="txtRespondentid" runat="server" Text="" CssClass="fblabel_view"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td class="fbform_sap">
            <asp:Label ID="lblMsg" runat="server" Text="" CssClass="labelMsg"></asp:Label>
            ID:<asp:Label ID="lblResult" runat="server" Text="" CssClass="labelMsg"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
</table>
