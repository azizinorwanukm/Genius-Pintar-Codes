<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="admin.ppcs.laporan.mingguan.list.aspx.vb" Inherits="permatapintar.admin_ppcs_laporan_mingguan_list" %>

<%@ Register Src="../commoncontrol/PPCS_Eval_Weekly_list.ascx" TagName="PPCS_Eval_Weekly_list"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Laporan PPCS>Laporan Mingguan Terkini
            </td>
        </tr>
    </table>
    <uc1:PPCS_Eval_Weekly_list ID="PPCS_Eval_Weekly_list1" runat="server" />
</asp:Content>
