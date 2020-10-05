<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.event.list.all.aspx.vb" Inherits="permatapintar.admin_event_list_all" %>

<%@ Register Src="../commoncontrol/event_list.ascx" TagName="event_list" TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/event_list_all.ascx" TagName="event_list_all" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Laporan>Laporan Kehadiran
            </td>
        </tr>
    </table>
    <uc2:event_list_all ID="event_list_all1" runat="server" />

</asp:Content>
