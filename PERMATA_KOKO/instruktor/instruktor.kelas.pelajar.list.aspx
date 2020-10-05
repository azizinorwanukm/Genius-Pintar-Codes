<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/instruktor/instruktor.Master" CodeBehind="instruktor.kelas.pelajar.list.aspx.vb" Inherits="permatapintar.instruktor_kelas_pelajar_list" %>

<%@ Register Src="../commoncontrol/kelas_pelajar_list.ascx" TagName="kelas_pelajar_list" TagPrefix="uc2" %>
<%@ Register Src="../commoncontrol/kelas_view_header.ascx" TagName="kelas_view_header" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Senarai Pelajar>Kelas>Senarai Pelajar
            </td>
        </tr>
    </table>
    <uc1:kelas_view_header ID="kelas_view_header1" runat="server" />
    &nbsp;
    <uc2:kelas_pelajar_list ID="kelas_pelajar_list1" runat="server" />
</asp:Content>
