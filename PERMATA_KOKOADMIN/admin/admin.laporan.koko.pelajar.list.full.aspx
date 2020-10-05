<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/print.Master" CodeBehind="admin.laporan.koko.pelajar.list.full.aspx.vb" Inherits="permatapintar.admin_laporan_koko_pelajar_list_full" %>
<%@ Register src="../commoncontrol/koko_pelajar_list_mark_full.ascx" tagname="koko_pelajar_list_mark_full" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Laporan>Markah Kokurikulum>Senarai Pelajar>Paparan Penuh
            </td>
        </tr>
    </table>
    
    <uc1:koko_pelajar_list_mark_full ID="koko_pelajar_list_mark_full1" runat="server" />
</asp:Content>
