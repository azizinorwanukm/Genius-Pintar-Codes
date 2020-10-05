<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm.Master"
    CodeBehind="ukm.pusatujian.laporan.aspx.vb" Inherits="permatapintar.ukm_pusatujian_laporan" %>

<%@ Register Src="commoncontrol/pusatujian_view.ascx" TagName="pusatujian_view" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pusatujian_view ID="pusatujian_view1" runat="server" />
    &nbsp;
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Laporan Pusat Ujian
            </td>
        </tr>
        <tr><td>Komen Anda:</td></tr>
        <tr>
            <td>
                <asp:TextBox ID="txtKomen" runat="server" Width="450px" Rows="5" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="fbform_sap">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                1) Kekuatan<br />
                2) Kelemahan<br />
                3) Penambahbaikan<br />
                4) Lain-lain (penyelewangan, pelajar bertukar komputer, kegagalan elektrik, pengesahan
                maklumat juruteknik & pengawas)
            </td>
        </tr>
    </table>
    <div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="system message..."></asp:Label></div>

</asp:Content>
