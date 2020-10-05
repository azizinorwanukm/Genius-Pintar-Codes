<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/instruktor/instruktor.Master" CodeBehind="instruktor.uniform.update.aspx.vb" Inherits="permatapintar.instruktor_uniform_update" %>

<%@ Register Src="../commoncontrol/instruktor_view_header.ascx" TagName="instruktor_view_header" TagPrefix="uc2" %>
<%@ Register Src="../commoncontrol/koko_pelajar_mark_uniform.ascx" TagName="koko_pelajar_mark_uniform" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Menu Utama>Masukkan Markah>Kemaskini
            </td>
        </tr>
    </table>
    <uc2:instruktor_view_header ID="instruktor_view_header1" runat="server" />
    &nbsp;
    <uc1:koko_pelajar_mark_uniform ID="koko_pelajar_mark_uniform1" runat="server" />
    &nbsp;
</asp:Content>