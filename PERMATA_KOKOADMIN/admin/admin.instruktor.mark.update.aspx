<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.instruktor.mark.update.aspx.vb" Inherits="permatapintar.admin_instruktor_mark_update" %>

<%@ Register Src="../commoncontrol/koko_pelajar_mark.ascx" TagName="koko_pelajar_mark" TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/instruktor_view_header.ascx" TagName="instruktor_view_header" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Kokurikulum>Masukkan Markah>Kemaskini
            </td>
        </tr>
    </table>
    <uc2:instruktor_view_header ID="instruktor_view_header1" runat="server" />
    &nbsp;
    <uc1:koko_pelajar_mark ID="koko_pelajar_mark1" runat="server" />
</asp:Content>
