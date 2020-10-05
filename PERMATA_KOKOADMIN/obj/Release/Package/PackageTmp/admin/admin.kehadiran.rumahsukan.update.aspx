<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.kehadiran.rumahsukan.update.aspx.vb" Inherits="permatapintar.admin_kehadiran_rumahsukan_update" %>

<%@ Register Src="../commoncontrol/event_view.ascx" TagName="event_view" TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/event_pelajar_select.ascx" TagName="event_pelajar_select" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Kokurikulum>Kehadiran Pelajar
            </td>
        </tr>
    </table>
    <uc1:event_view ID="event_view1" runat="server" />
    <uc2:event_pelajar_select ID="event_pelajar_select1" runat="server" />
</asp:Content>
