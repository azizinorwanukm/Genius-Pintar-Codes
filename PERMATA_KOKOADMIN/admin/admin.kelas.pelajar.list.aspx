<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.kelas.pelajar.list.aspx.vb" Inherits="permatapintar.admin_kelas_pelajar_list" %>

<%@ Register Src="../commoncontrol/kelas_view_header.ascx" TagName="kelas_view_header" TagPrefix="uc1" %>
<%@ Register src="../commoncontrol/kelas_pelajar_list.ascx" tagname="kelas_pelajar_list" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Kelas>Senarai Pelajar>Pilih Pelajar
            </td>
        </tr>
    </table>
    <uc1:kelas_view_header ID="kelas_view_header1" runat="server" />
    &nbsp;
    <uc2:kelas_pelajar_list ID="kelas_pelajar_list1" runat="server" />
</asp:Content>
