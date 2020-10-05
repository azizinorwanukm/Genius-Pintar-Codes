<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/instruktor/instruktor.Master" CodeBehind="instruktor.koko.list.rumahsukan.aspx.vb" Inherits="permatapintar.instruktor_koko_list_rumahsukan" %>

<%@ Register Src="../commoncontrol/koko_list_rumahsukan.ascx" TagName="koko_list_rumahsukan" TagPrefix="uc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Senarai Pelajar>Kokurikulum>Rumah Sukan
            </td>
        </tr>
    </table>
    <uc1:koko_list_rumahsukan ID="koko_list_rumahsukan1" runat="server" />
</asp:Content>
