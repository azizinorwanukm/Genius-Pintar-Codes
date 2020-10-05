<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/instruktor/instruktor.Master" CodeBehind="instruktor.koko.pelajar.list.aspx.vb" Inherits="permatapintar.instruktor_koko_pelajar_list1" %>

<%@ Register Src="../commoncontrol/koko_pelajar_list.ascx" TagName="koko_pelajar_list" TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/koko_header.ascx" TagName="koko_header" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Senarai Pelajar>Kokurikulum
            </td>
        </tr>
    </table>
    <uc1:koko_pelajar_list ID="koko_pelajar_list1" runat="server" />
</asp:Content>
