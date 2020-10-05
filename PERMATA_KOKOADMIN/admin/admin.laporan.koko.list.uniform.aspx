<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.laporan.koko.list.uniform.aspx.vb" Inherits="permatapintar.admin_laporan_koko_list_uniform" %>

<%@ Register Src="../commoncontrol/koko_list_uniform.ascx" TagName="koko_list_uniform" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Laporan>Markah Kokurikulum>Badan Beruniform
            </td>
        </tr>
    </table>
    <uc1:koko_list_uniform ID="koko_list_uniform1" runat="server" />
</asp:Content>
