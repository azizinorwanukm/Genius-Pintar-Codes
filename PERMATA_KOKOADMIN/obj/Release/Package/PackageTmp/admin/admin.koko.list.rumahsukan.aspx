<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.koko.list.rumahsukan.aspx.vb" Inherits="permatapintar.admin_koko_list_rumahsukan" %>

<%@ Register Src="../commoncontrol/koko_list_rumahsukan.ascx" TagName="koko_list_rumahsukan" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Kokurikulum>Senarai Kokurikulum>Rumah Sukan
            </td>
        </tr>
    </table>
    <uc1:koko_list_rumahsukan ID="koko_list_rumahsukan1" runat="server" />
</asp:Content>