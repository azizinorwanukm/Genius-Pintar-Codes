<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="schoolprofile.confirm.change.aspx.vb" Inherits="permatapintar.schoolprofile_confirm_change1" %>

<%@ Register Src="../commoncontrol/schoolprofile_view.ascx" TagName="schoolprofile_view_change"
    TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/studentprofile_header_ppcs.ascx" TagName="studentprofile_header"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc2:studentprofile_header ID="studentprofile_header1" runat="server" />
    &nbsp;
    <uc1:schoolprofile_view_change ID="schoolprofile_view_change1" runat="server" />
    &nbsp;
    <table class="fbform">
        <tr class="fbform_header">
            <td colspan="2">Mengesahkan Maklumat Sekolah Baru
            </td>
        </tr>
        <tr>
            <td>Tarikh Mula:
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
                <asp:DropDownList ID="ddlStartDate_year" runat="server" AutoPostBack="false" Width="100px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Tarikh Akhir:
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
                <asp:DropDownList ID="ddlEndDate_year" runat="server" AutoPostBack="false" Width="100px">
                </asp:DropDownList>

                &nbsp;[Jika sekolah terkini, tidak perlu masukkan Tarikh Akhir]
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td class="fbform_sap">&nbsp;
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td style="text-align: left;">
                <asp:Button ID="btnConfirm" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;&nbsp;
                <asp:LinkButton ID="lnkStudentProfileView" runat="server">View Student Profile</asp:LinkButton>
            </td>
        </tr>
    </table>
    <div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label>
    </div>
</asp:Content>
