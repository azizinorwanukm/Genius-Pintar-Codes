<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.ukm3.mark.update.aspx.vb" Inherits="permatapintar.admin_ukm3_mark_update" %>
<%@ Register src="../commoncontrol/ukm3_mark_update.ascx" tagname="ukm3_mark_update" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Program Pendidikan PERMATApintar>Markah UKM3
            </td>
        </tr>
    </table>
    <uc1:ukm3_mark_update ID="ukm3_mark_update1" runat="server" />
</asp:Content>
