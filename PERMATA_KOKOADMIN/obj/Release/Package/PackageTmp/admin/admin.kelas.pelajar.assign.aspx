<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.kelas.pelajar.assign.aspx.vb" Inherits="permatapintar.admin_kelas_pelajar_assign" %>

<%@ Register Src="../commoncontrol/kelas_view_header.ascx" TagName="kelas_view_header" TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/pelajar_select.ascx" TagName="pelajar_select" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Kelas>Penetapan PELAJAR>Pilih PELAJAR
            </td>
        </tr>
    </table>
    <uc1:kelas_view_header ID="kelas_view_header1" runat="server" />
    &nbsp;
    <uc2:pelajar_select ID="pelajar_select1" runat="server" />
</asp:Content>
