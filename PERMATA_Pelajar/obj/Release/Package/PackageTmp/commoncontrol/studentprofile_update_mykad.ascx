<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="studentprofile_update_mykad.ascx.vb"
    Inherits="permatapintar.studentprofile_update_mykad" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Kemaskini Maklumat Pelajar
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top;">Gambar
        </td>
        <td>
            <table>
                <tr>
                    <td>
                        <asp:Image ID="imgStudent" Style="width: 120px; height: 150px;" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:FileUpload ID="imgUpload" runat="server" />
                        <asp:Button ID="btnUpload" runat="server" Text="Muatnaik Gambar" CssClass="fbbutton" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">MYKAD/MYKID#:
        </td>
        <td>
            <asp:TextBox ID="txtMYKAD" runat="server" Width="250px" MaxLength="250"></asp:TextBox>&nbsp;*&nbsp;[Contoh:020820086011.
            Tanpa "-" atau ruang kosong]<br />
            <asp:Label ID="Label2" runat="server" CssClass="fblabel_msg" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Nama Penuh:
        </td>
        <td>
            <asp:TextBox ID="txtStudentFullname" runat="server" Width="250px" MaxLength="250"></asp:TextBox>&nbsp;*&nbsp;
        </td>
    </tr>
    <tr>
        <td>Jantina:
        </td>
        <td>
            <select name="selStudentGender" id="selStudentGender" style="width: 250px;" runat="server">
                <option value="" selected="selected"></option>
                <option value="LELAKI">LELAKI</option>
                <option value="PEREMPUAN">PEREMPUAN</option>
            </select>
        </td>
    </tr>
    <tr>
        <td>Tarikh Lahir:
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
            </select>&nbsp;&nbsp;
            <select name="selStudentDOB_month" id="selStudentDOB_month" style="width: 80px;"
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
            </select>&nbsp;&nbsp;
            <asp:DropDownList ID="ddlDOB_Year" runat="server" Width="95px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Darjah/Tingkatan:
        </td>
        <td>
            <asp:DropDownList ID="ddlStudentForm" runat="server" Width="250px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Bangsa:
        </td>
        <td>
            <asp:DropDownList ID="ddlStudentRace" runat="server" Width="250px">
            </asp:DropDownList>

            &nbsp;&nbsp;Agama:&nbsp;
            <asp:DropDownList ID="ddlStudentReligion" runat="server" Width="250px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Kebolehan Berbahasa:
        </td>
        <td>
            <table class="fbform">
                <tr>
                    <td style="text-align: left; vertical-align: top;">Pertuturan
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
                    <td style="text-align: left; vertical-align: top;">Penulisan
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
        <td>&nbsp;
        </td>
        <td class="fbform_sap">&nbsp;
        </td>
    </tr>
    <tr>
        <td>Alamat Rumah:
        </td>
        <td>
            <asp:TextBox ID="txtStudentAddress1" runat="server" Width="350px" MaxLength="250"
                TextMode="SingleLine"></asp:TextBox>&nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td>
            <asp:TextBox ID="txtStudentAddress2" runat="server" Width="350px" MaxLength="250"
                TextMode="SingleLine"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td>Poskod:
        </td>
        <td>
            <asp:TextBox ID="txtStudentPostcode" runat="server" Width="50px" MaxLength="5"></asp:TextBox>&nbsp;
            Bandar:<asp:TextBox ID="txtStudentCity" runat="server" Width="250px" MaxLength="150"></asp:TextBox>
        </td>

    </tr>
    <tr>
        <td>Negeri:
            
        </td>
        <td>
            <asp:DropDownList ID="ddlStudentState" runat="server" Width="250px">
            </asp:DropDownList>&nbsp;&nbsp;[LAIN-LAIN bagi Negara selain MALAYSIA]
        </td>
    </tr>
    <tr>
        <td>Negara:
        </td>
        <td>
            <asp:DropDownList ID="ddlStudentCountry" runat="server" Width="250px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Email:
        </td>
        <td>
            <asp:TextBox ID="txtStudentEmail" runat="server" Width="250px" MaxLength="250"></asp:TextBox>&nbsp;&nbsp;[Kelayakan
            anda ke Ujian UKM2 dan PPCS akan di hantar ke email ini.]
        </td>
    </tr>
    <tr>
        <td>Kata Laluan:
        </td>
        <td>
            <asp:TextBox ID="txtPwd" runat="server" Width="250px" MaxLength="250"></asp:TextBox>&nbsp;&nbsp;[Kata
            laluan akan digunakan pada masa akan datang.]
        </td>
    </tr>
    <tr>
        <td>Tel#:
        </td>
        <td>
            <asp:TextBox ID="txtStudentContactNo" runat="server" Width="250px" MaxLength="150"></asp:TextBox>&nbsp;&nbsp;[Telefon untuk dihubungi mengenai status PERMATApintar.]
        </td>
    </tr>
    <tr>
        <td></td>
        <td>Bagi pelajar berumur 8-12 tahun, nyatakan kecenderungan anda:
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:DropDownList ID="ddlCenderung" runat="server" Width="200px"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td class="fbform_sap">&nbsp;
        </td>
    </tr>


    <tr>
        <td>Alumni ID:
        </td>
        <td>
            <asp:Label ID="lblAlumniID" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>No Pelajar:
        </td>
        <td>
            <asp:Label ID="lblNoPelajar" runat="server" Text="" ForeColor="Red"></asp:Label>
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
        <td>&nbsp;
        </td>
        <td style="text-align: left;">
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;|&nbsp;
            <asp:LinkButton ID="lnkStudentProfileView" runat="server">Maklumat Pelajar</asp:LinkButton>
        </td>
    </tr>
</table>
<asp:Label ID="lblLog" runat="server" Text=""></asp:Label>
<asp:Label ID="lblMYKAD_ori" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lblAlumniID_ori" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lblNoPelajar_ori" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lblStudentType" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lblDebug" runat="server" Text=""></asp:Label>

