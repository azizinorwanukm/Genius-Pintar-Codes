<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="schoolprofile_create.ascx.vb"
    Inherits="permatapintar.schoolprofile_create" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Maklumat Sekolah
        </td>
    </tr>
    <tr>
        <td>
            Kod Sekolah
        </td>
        <td>
            :<asp:TextBox ID="txtSchoolCode" runat="server" Width="150px" MaxLength="50" Text="XXX"
                CssClass="fbreadonly" ReadOnly="true"></asp:TextBox>&nbsp;[Sekolah yang didaftarkan
            oleh pelajar sendiri]
        </td>
    </tr>
    <tr>
        <td style="width: 20%;">
            Nama Sekolah
        </td>
        <td>
            :<asp:TextBox ID="txtSchoolName" runat="server" Width="350px" MaxLength="250"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            Alamat Sekolah
        </td>
        <td>
            :<asp:TextBox ID="txtSchoolAddress" runat="server" Width="350px" MaxLength="500"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            Poskod
        </td>
        <td>
            :<asp:TextBox ID="txtSchoolPostcode" runat="server" Width="80px" MaxLength="5"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            Bandar
        </td>
        <td>
            :<asp:TextBox ID="txtSchoolCity" runat="server" Width="250px" MaxLength="100"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>
            Negeri
        </td>
        <td>
            :<select name="selSchoolState" id="selSchoolState" style="width: 250px;" runat="server">
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
                <option value="WP KUALA LUMPUR">WP KUALA LUMPUR</option>
                <option value="WP LABUAN">WP LABUAN</option>
                <option value="WP PUTRAJAYA">WP PUTRAJAYA</option>
            </select>*
        </td>
    </tr>
    <tr>
        <td>
            Telefon
        </td>
        <td>
            :<asp:TextBox ID="txtSchoolNoTel" runat="server" Width="250px" MaxLength="100"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Faks
        </td>
        <td>
            :<asp:TextBox ID="txtSchoolNoFax" runat="server" Width="250px" MaxLength="100"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Email
        </td>
        <td>
            :<asp:TextBox ID="txtSchoolEmail" runat="server" Width="250px" MaxLength="100"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Jenis Sekolah
        </td>
        <td>
            :<asp:DropDownList ID="ddlSchoolType" runat="server" Width="250px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            SchoolID
        </td>
        <td>
            :<asp:Label ID="lblSchoolID" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td class="fbform_sap">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:Button ID="btnConfirm" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;|&nbsp;<asp:LinkButton ID="lnkBack" runat="server">Kembali</asp:LinkButton>
        </td>
    </tr>
</table>
**Nota:<br />Jika Pelajar/Guru/Ibubapa tidak memilih sekolah sedia ada dalam senarai database KPM dan MARA, maka pelajar tidak akan dipertimbangkan untuk ke ujian UKM2. Kod <b>XXX</b> hanya untuk <b>sekolah antarabangsa dan persendirian sahaja.</b>
<%--
&nbsp;
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Mengesahkan Maklumat Sekolah Baru
        </td>
    </tr>
    <tr>
        <td>
            Tarikh Mula:
        </td>
        <td>
            <select name="selStartDate_day" id="selStartDate_day" style="width: 50px;" runat="server">
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
            </select>&nbsp;
            <select name="selStartDate_month" id="selStartDate_month" style="width: 100px;" runat="server">
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
            </select>&nbsp;
            <select name="selStartDate_year" id="selStartDate_year" style="width: 95px;" runat="server">
                <option value="" selected="selected">Tahun</option>
                <option value="2012">2012</option>
                <option value="2011">2011</option>
                <option value="2010">2010</option>
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
                <option value="1990">1990</option>
                <option value="1989">1989</option>
                <option value="1988">1988</option>
                <option value="1987">1987</option>
                <option value="1986">1986</option>
                <option value="1985">1985</option>
            </select>&nbsp;*
        </td>
    </tr>
    <tr>
        <td>
            Tarikh Akhir:
        </td>
        <td>
            <select name="selEndDate_day" id="selEndDate_day" style="width: 50px;" runat="server">
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
            </select>&nbsp;
            <select name="selEndDate_month" id="selEndDate_month" style="width: 100px;" runat="server">
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
            </select>&nbsp;
            <select name="selEndDate_year" id="selEndDate_year" style="width: 95px;" runat="server">
                <option value="" selected="selected">Tahun</option>
                <option value="2012">2012</option>
                <option value="2011">2011</option>
                <option value="2010">2010</option>
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
                <option value="1990">1990</option>
                <option value="1989">1989</option>
                <option value="1988">1988</option>
                <option value="1987">1987</option>
                <option value="1986">1986</option>
                <option value="1985">1985</option>
            </select>&nbsp;[Jika sekolah terkini, tidak perlu masukkan Tarikh Akhir]
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
        </td>
    </tr>
</table>--%>
