<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.pelajar.status.update.aspx.vb" Inherits="permatapintar.admin_pelajar_status_update" %>

<%@ Register Src="../commoncontrol/pelajar_status_update.ascx" TagName="pelajar_status_update" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Selenggara>Status Pelajar
            </td>
        </tr>
    </table>
    <uc1:pelajar_status_update ID="pelajar_status_update1" runat="server" />
</asp:Content>
