<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.koko.pelajar.list.aspx.vb" Inherits="permatapintar.admin_koko_pelajar_list" %>

<%@ Register Src="../commoncontrol/koko_pelajar_list.ascx" TagName="koko_pelajar_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Kokurikulum>Senarai Pelajar
            </td>
        </tr>
    </table>
    <uc1:koko_pelajar_list ID="koko_pelajar_list1" runat="server" />
</asp:Content>
