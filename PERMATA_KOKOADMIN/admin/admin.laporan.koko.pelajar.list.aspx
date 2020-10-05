<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.laporan.koko.pelajar.list.aspx.vb" Inherits="permatapintar.admin_laporan_koko_pelajar_list" %>

<%@ Register src="../commoncontrol/koko_pelajar_list_mark.ascx" tagname="koko_pelajar_list_mark" tagprefix="uc1" %>
<%@ Register src="../commoncontrol/gred_mod_view.ascx" tagname="gred_mod_view" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Laporan>Markah Kokurikulum>Senarai Pelajar
            </td>
        </tr>
    </table>
    <uc1:koko_pelajar_list_mark ID="koko_pelajar_list_mark1" runat="server" />
    &nbsp;<uc3:gred_mod_view ID="gred_mod_view1" runat="server" />
</asp:Content>