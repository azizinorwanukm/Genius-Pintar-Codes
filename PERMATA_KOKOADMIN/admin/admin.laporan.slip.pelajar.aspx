<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.laporan.slip.pelajar.aspx.vb" Inherits="permatapintar.admin_laporan_slip_pelajar" %>
<%@ Register src="../commoncontrol/sijil_pelajar.ascx" tagname="sijil_pelajar" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Laporan>Slip Peperiksaan
            </td>
        </tr>
    </table>
    <uc1:sijil_pelajar ID="sijil_pelajar1" runat="server" />
</asp:Content>
