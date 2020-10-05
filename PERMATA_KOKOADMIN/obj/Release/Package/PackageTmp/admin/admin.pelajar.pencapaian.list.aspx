<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.pelajar.pencapaian.list.aspx.vb" Inherits="permatapintar.admin_pelajar_pencapaian_list" %>

<%@ Register Src="../commoncontrol/pelajar_pencapaian_list.ascx" TagName="pelajar_pencapaian_list" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Kokurikulum>Sahkan Pencapaian>Senarai Pelajar
            </td>
        </tr>
    </table>
    <uc1:pelajar_pencapaian_list ID="pelajar_pencapaian_list1" runat="server" />
</asp:Content>
