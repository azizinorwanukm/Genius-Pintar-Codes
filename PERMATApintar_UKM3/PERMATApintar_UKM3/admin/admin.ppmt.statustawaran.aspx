<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.ppmt.statustawaran.aspx.vb" Inherits="permatapintar.admin_ppmt_statustawaran" %>

<%@ Register Src="../commoncontrol/ukm3_statustawaran_list.ascx" TagName="ukm3_statustawaran_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>Program Pendidikan PERMATApintar>Status Tawaran
            </td>
        </tr>
    </table>
    <uc1:ukm3_statustawaran_list ID="ukm3_statustawaran_list1" runat="server" />
</asp:Content>
