<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/penguruspelajar/popup.Master" CodeBehind="laporan.harian.note.update.aspx.vb" Inherits="permatapintar.laporan_harian_note_update9" %>

<%@ Register Src="../commoncontrol/laporan.harian.note.update.ascx" TagName="laporan"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Catatan Laporan Harian
            </td>
        </tr>
    </table>
    <uc1:laporan ID="laporan1" runat="server" />
    &nbsp;
</asp:Content>
