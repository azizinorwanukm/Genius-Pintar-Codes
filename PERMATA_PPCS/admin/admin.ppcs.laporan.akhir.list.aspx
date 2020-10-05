<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.ppcs.laporan.akhir.list.aspx.vb" Inherits="permatapintar.admin_ppcs_laporan_akhir_list" %>
<%@ Register src="../commoncontrol/PPCS_Eval_End_list.ascx" tagname="PPCS_Eval_End_list" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Laporan PPCS>Laporan Akhir Terkini
            </td>
        </tr>
    </table>
    <uc1:PPCS_Eval_End_list ID="PPCS_Eval_End_list1" runat="server" />
</asp:Content>
