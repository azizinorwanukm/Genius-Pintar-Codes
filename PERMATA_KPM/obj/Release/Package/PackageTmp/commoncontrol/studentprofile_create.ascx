<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="studentprofile_create.ascx.vb"
    Inherits="permatapintar.studentprofile_create" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Pendaftaran Pelajar Baru
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            MYKAD/MYKID#:
        </td>
        <td>
            <asp:TextBox ID="txtMYKAD" runat="server" Width="250px" MaxLength="20" ReadOnly="true"
                BackColor="#CCCCCC"></asp:TextBox>&nbsp;*&nbsp;[Contoh:020820086011. Tanpa "-" atau ruang kosong]<br />
            <asp:Label ID="Label2" runat="server" CssClass="fblabel_msg" Text="[Pendaftaran hanya untuk warganegara Malaysia berumur 8-15 tahun sahaja]"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            Kata Laluan:
        </td>
        <td>
            <asp:TextBox ID="txtPwd" runat="server" Width="250px" MaxLength="250" TextMode="Password"></asp:TextBox>&nbsp;*&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            Ulang Kata Laluan:
        </td>
        <td>
            <asp:TextBox ID="txtPwdVerify" runat="server" Width="250px" MaxLength="250" TextMode="Password"></asp:TextBox>&nbsp;*&nbsp;
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td class="fbform_sap">
        </td>
    </tr>
    <tr>
        <td>
            Nama Pelajar:
        </td>
        <td>
            <asp:TextBox ID="txtStudentFullname" runat="server" Width="350px" MaxLength="250"></asp:TextBox>&nbsp;*&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            Jantina:
        </td>
        <td>
            <select name="selStudentGender" id="selStudentGender" style="width: 200px;" runat="server">
                <option value="" selected="selected"></option>
                <option value="LELAKI">LELAKI</option>
                <option value="PEREMPUAN">PEREMPUAN</option>
            </select>
        </td>
    </tr>
    <tr>
        <td>
            Tarikh Lahir:
        </td>
        <td>
            <select name="selStudentDOB_day" id="selStudentDOB_day" style="width: 50px;" runat="server">
                <option value="" selected="selected">Hari</option>
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
            </select>&nbsp;*&nbsp;
            <select name="selStudentDOB_month" id="selStudentDOB_month" style="width: 100px;"
                runat="server">
                <option value="" selected="selected">Bulan</option>
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
            </select>&nbsp;*&nbsp;
            <select name="selStudentDOB_year" id="selStudentDOB_year" style="width: 95px;" runat="server">
                <option value="" selected="selected">Tahun</option>
                <option value="2009">2009</option>
                <option value="2008">2008</option>
                <option value="2007">2007</option>
                <option value="2006">2006</option>
                <option value="2005">2005</option>
                <option value="2004">2004</option>
                <option value="2003">2003</option>
                <option value="2002">2002</option>
                <option value="2001">2001</option>
                <option value="2000">2000</option>
                <option value="1999">1999</option>
                <option value="1998">1998</option>
                <option value="1997">1997</option>
                <option value="1996">1996</option>
                <option value="1995">1995</option>
                <option value="1994">1994</option>
                <option value="1993">1993</option>
                <option value="1992">1992</option>
                <option value="1991">1991</option>
            </select>&nbsp;*
        </td>
    </tr>
    <tr>
        <td>
            Darjah/Tingkatan:
        </td>
        <td>
            <select name="selStudentForm" id="selStudentForm" style="width: 200px;" runat="server">
                <option value="" selected="selected"></option>
                <option value="DARJAH 1">DARJAH 1</option>
                <option value="DARJAH 2">DARJAH 2</option>
                <option value="DARJAH 3">DARJAH 3</option>
                <option value="DARJAH 4">DARJAH 4</option>
                <option value="DARJAH 5">DARJAH 5</option>
                <option value="DARJAH 6">DARJAH 6</option>
                <option value="TINGKATAN 1">TINGKATAN 1</option>
                <option value="TINGKATAN 2">TINGKATAN 2</option>
                <option value="TINGKATAN 3">TINGKATAN 3</option>
                <option value="TINGKATAN 4">TINGKATAN 4</option>
                <option value="TINGKATAN 5">TINGKATAN 5</option>
                <option value="LAIN-LAIN">LAIN-LAIN</option>
            </select>&nbsp;*&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            Bangsa:
        </td>
        <td>
            <select name="selStudentRace" id="selStudentRace" style="width: 200px;" runat="server">
                <option value="" selected="selected"></option>
                <option value="MELAYU">MELAYU</option>
                <option value="CINA">CINA</option>
                <option value="INDIA">INDIA</option>
                <option value="BUMIPUTERA (SABAH)">BUMIPUTERA (SABAH)</option>
                <option value="BUMIPUTERA (SARAWAK)">BUMIPUTERA (SARAWAK)</option>
                <option value="LAIN-LAIN">LAIN-LAIN</option>
            </select>&nbsp;*&nbsp;Agama:&nbsp;
            <select name="selStudentReligion" id="selStudentReligion" style="width: 200px;" runat="server">
                <option value="" selected="selected"></option>
                <option value="ISLAM">ISLAM</option>
                <option value="BUDDHA">BUDDHA</option>
                <option value="HINDU">HINDU</option>
                <option value="KRISTIAN">KRISTIAN</option>
                <option value="LAIN-LAIN">LAIN-LAIN</option>
            </select>&nbsp;*&nbsp;
        </td>
    </tr>
    <tr>
        <td style="text-align: left; vertical-align: top;">
            Kebolehan Berbahasa:
        </td>
        <td>
            <table class="fbform">
                <tr>
                    <td style="text-align: left; vertical-align: top;">
                        Pertuturan
                    </td>
                    <td>
                        <asp:CheckBox ID="chkTalkBM" runat="server" Text="B. Malalaysia" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkTalkBI" runat="server" Text="B. English" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkTalkMan" runat="server" Text="B. Mandarin" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkTalkTamil" runat="server" Text="B. Tamil" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkTalkArab" runat="server" Text="B. Arab" />&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; vertical-align: top;">
                        Penulisan
                    </td>
                    <td>
                        <asp:CheckBox ID="chkWriteBM" runat="server" Text="B. Malalaysia" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkWriteBI" runat="server" Text="B. English" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkWriteMan" runat="server" Text="B. Mandarin" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkWriteTamil" runat="server" Text="B. Tamil" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkWriteArab" runat="server" Text="B. Arab" />&nbsp;
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
            Alamat Rumah:
        </td>
        <td>
            <asp:TextBox ID="txtStudentAddress1" runat="server" Width="500px" MaxLength="250"
                TextMode="SingleLine"></asp:TextBox>&nbsp;*&nbsp;
        </td>
    </tr>
    <tr>
        <td valign="top">
        </td>
        <td>
            <asp:TextBox ID="txtStudentAddress2" runat="server" Width="500px" MaxLength="250"
                TextMode="SingleLine"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            Poskod:
        </td>
        <td>
            <asp:TextBox ID="txtStudentPostcode" runat="server" Width="50px" MaxLength="5"></asp:TextBox>&nbsp;*&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            Bandar:
        </td>
        <td>
            <asp:TextBox ID="txtStudentCity" runat="server" Width="250px" MaxLength="150"></asp:TextBox>&nbsp;*
            &nbsp; Negeri:&nbsp;
            <select name="selStudentState" id="selStudentState" style="width: 200px;" runat="server">
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
            </select>&nbsp;*&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            Email:
        </td>
        <td>
            <asp:TextBox ID="txtStudentEmail" runat="server" Width="250px" MaxLength="250"></asp:TextBox>&nbsp;*&nbsp;[Keputusan kelayakan
            ke Ujian UKM2 dan PPCS akan di hantar ke email ini.]
        </td>
    </tr>
    
    <tr>
        <td>
            Student ID:
        </td>
        <td>
            <asp:Label ID="lblStudentID" runat="server" Text="" CssClass="fblabel_msg"></asp:Label>
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
        <td>
            &nbsp;
        </td>
        <td style="text-align: right;">
            <asp:Button ID="btnCreate" runat="server" Text="Daftar Pelajar Baru" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Medan bertanda * adalah wajib diisi."></asp:Label></div>
