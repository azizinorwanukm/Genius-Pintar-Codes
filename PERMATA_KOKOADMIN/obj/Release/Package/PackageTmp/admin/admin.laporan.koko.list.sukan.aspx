<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.laporan.koko.list.sukan.aspx.vb" Inherits="permatapintar.admin_laporan_koko_list_sukan" %>

<%@ Register Src="../commoncontrol/koko_list_sukan.ascx" TagName="koko_list_sukan" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Laporan>Markah Kokurikulum>Sukan & Permainan
            </td>
        </tr>
    </table>
    <uc1:koko_list_sukan ID="koko_list_sukan1" runat="server" />
</asp:Content>

