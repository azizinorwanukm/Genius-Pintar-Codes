<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pelajar/pelajar.master" CodeBehind="pelajar.koko.list.all.aspx.vb" Inherits="permatapintar.pelajar_koko_list_all" %>

<%@ Register src="../commoncontrol/koko_list_all.ascx" tagname="koko_list_all" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Laporan>Senarai Kokurikulum
            </td>
        </tr>
    </table>
    &nbsp;
    <uc1:koko_list_all ID="koko_list_all1" runat="server" />
</asp:Content>
