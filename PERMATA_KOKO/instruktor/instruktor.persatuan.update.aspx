<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/instruktor/instruktor.Master" CodeBehind="instruktor.persatuan.update.aspx.vb" Inherits="permatapintar.instruktor_persatuan_update" %>

<%@ Register Src="../commoncontrol/instruktor_view_header.ascx" TagName="instruktor_view_header" TagPrefix="uc2" %>
<%@ Register Src="../commoncontrol/koko_pelajar_mark_persatuan.ascx" TagName="koko_pelajar_mark_persatuan" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Kokurikulum>Masukkan Markah>Kemaskini
            </td>
        </tr>
    </table>
    <uc2:instruktor_view_header ID="instruktor_view_header1" runat="server" />
    &nbsp;
    <uc1:koko_pelajar_mark_persatuan ID="koko_pelajar_mark_persatuan1" runat="server" />
</asp:Content>