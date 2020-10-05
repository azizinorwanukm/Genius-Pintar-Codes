<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.koko.instruktor.assign.aspx.vb" Inherits="permatapintar.admin_koko_instruktor_assign" %>

<%@ Register Src="../commoncontrol/instruktor_update_koko.ascx" TagName="instruktor_update_koko" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Kokurikulum>Penetapan INSTRUKTOR
            </td>
        </tr>
    </table>
    <uc1:instruktor_update_koko ID="instruktor_update_koko1" runat="server" />
</asp:Content>
