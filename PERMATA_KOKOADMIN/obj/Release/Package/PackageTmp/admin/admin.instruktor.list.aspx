<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.instruktor.list.aspx.vb" Inherits="permatapintar.admin_instruktor_list" %>

<%@ Register Src="../commoncontrol/instruktor_list.ascx" TagName="instruktor_list" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Kokurikulum>Pilih Instruktor
            </td>
        </tr>
    </table>
    <uc1:instruktor_list ID="instruktor_list1" runat="server" />
</asp:Content>
