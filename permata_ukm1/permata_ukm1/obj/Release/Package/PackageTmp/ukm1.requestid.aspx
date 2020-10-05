<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm1.default.Master"
    CodeBehind="ukm1.requestid.aspx.vb" Inherits="permatapintar.ukm1_requestid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform" border="0px">
        <tr class="fbsection_header">
            <td colspan="3">
                Maklumat Pelajar [Tidak mempunyai MYKID/MYKAD]
            </td>
        </tr>
        <tr>
            <td style="width: 15%;">
                *No Sijil Kelahiran:
            </td>
            <td>
                <asp:TextBox ID="nosuratberanak" runat="server" Width="250px" MaxLength="20"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="nosuratberanak"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" CssClass="labelMsg" Text="[Untuk pelajar yang tiada nombor KPT/MYKAD/MYKID sahaja. Contoh: K123456]"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                *Nama Penuh:
            </td>
            <td>
                <asp:TextBox ID="namapenuh" runat="server" Width="250px" MaxLength="200"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="namapenuh"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="Label5" runat="server" CssClass="labelMsg" Text="[Nama seperti di dalam Surat Beranak.]"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                *Tarikh Lahir:
            </td>
            <td>
                <select name="RespDOBday" id="RespDOBday" style="width: 80px;" runat="server">
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
                </select>
                <select name="RespDOBmonth" id="RespDOBmonth" style="width: 80px;" runat="server">
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
                </select>
                <select name="RespDOBYear" id="RespDOBYear" style="width: 80px;" runat="server">
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
                </select>
                <asp:Label ID="lblDOBMsg" runat="server" CssClass="labelMsgRed" Text=""></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label1" runat="server" CssClass="labelMsg" Text="[Ujian ini hanya untuk pelajar berumur 8 hingga 15 tahun sahaja]"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                *Email:
            </td>
            <td>
                <asp:TextBox ID="email" runat="server" Width="250px" MaxLength="250"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="email"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="Label3" runat="server" CssClass="labelMsg" Text="[Nombor sementara mengantikan MYKAD/MYKID# akan dihantar ke email ANDA. Sila pastikan email yang digunakan berfungsi.]"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                *Kepastian Email:
            </td>
            <td>
                <asp:TextBox ID="emailverify" runat="server" Width="250px" MaxLength="250"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="emailverify"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" CssClass="labelMsg" Text="[Mesti sama dengan email di atas untuk kepastian email]"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td colspan="2" class="fbform_sap">
                <asp:Label ID="lblMsg" runat="server" Text="" CssClass="labelMsgRed"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                <asp:Button ID="btnSubmit" runat="server" Text="Hantar" CssClass="fbbutton" />&nbsp;
                <asp:Button ID="btnNext" runat="server" Text="Seterusnya >>" CssClass="fbbutton"
                    Visible="false" />&nbsp;
            </td>
        </tr>
    </table>
    <div class="warning" id="div2" runat="server">
        Tekan Hantar untuk mendapat nombor sementara mengantikan MYKAD/MYKID#. Untuk pengesahan,
        sila lihat email ANDA samada di dalam Inbox/Spam untuk nombor sementara mengantikan
        MYKAD/MYKID# selepas 5 hingga 10 minit. Pelajar yang lahir tahun 2001 dan ke atas
        diandaikan mempunyai MYKID#.<br />
        <br />
        JANGAN masukkan email permatapintar@ukm.my</div>
</asp:Content>
