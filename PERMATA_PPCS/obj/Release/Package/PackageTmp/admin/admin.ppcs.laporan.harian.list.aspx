<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="admin.ppcs.laporan.harian.list.aspx.vb" Inherits="permatapintar.admin_ppcs_laporan_harian_list" %>

<%@ Register Src="../commoncontrol/PPCS_Eval_Daily_list.ascx" TagName="PPCS_Eval_Daily_list"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Laporan PPCS>Laporan Harian Terkini
            </td>
        </tr>
    </table>
    <uc1:PPCS_Eval_Daily_list ID="PPCS_Eval_Daily_list1" runat="server" />
</asp:Content>
