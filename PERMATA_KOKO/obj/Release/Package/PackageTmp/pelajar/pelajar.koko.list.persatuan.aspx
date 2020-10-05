<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pelajar/pelajar.master" CodeBehind="pelajar.koko.list.persatuan.aspx.vb" Inherits="permatapintar.pelajar_koko_list_persatuan" %>

<%@ Register Src="../commoncontrol/koko_list_persatuan.ascx" TagName="koko_list_persatuan" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Laporan>Senarai Kokurikulum>Kelab & Persatuan
            </td>
        </tr>
    </table>
    <uc1:koko_list_persatuan ID="koko_list_persatuan1" runat="server" />
</asp:Content>
