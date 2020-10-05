<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.event.update.aspx.vb" Inherits="permatapintar.admin_event_update" %>

<%@ Register Src="../commoncontrol/event_update.ascx" TagName="event_update" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Menu Utama>Kehadiran Pelajar>Kemaskini Acara
            </td>
        </tr>
    </table>
    <uc1:event_update ID="event_update1" runat="server" />
</asp:Content>
