<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.ppmt.status.update.aspx.vb" Inherits="permatapintar.admin_ppmt_status_update" %>
<%@ Register src="../commoncontrol/ukm3_ppmt_update.ascx" tagname="ukm3_ppmt_update" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Program Pendidikan PERMATApintar>Kelayakan ke Kolej PERMATApintar
            </td>
        </tr>
    </table>
    <uc1:ukm3_ppmt_update ID="ukm3_ppmt_update1" runat="server" />
</asp:Content>
