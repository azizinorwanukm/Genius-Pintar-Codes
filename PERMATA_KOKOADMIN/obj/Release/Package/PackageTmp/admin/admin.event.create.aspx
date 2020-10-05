<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.event.create.aspx.vb" Inherits="permatapintar.admin_event_create" %>

<%@ Register Src="../commoncontrol/event_create.ascx" TagName="event_create" TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/instruktor_view_header_simple.ascx" TagName="instruktor_view_header_simple" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Kokurikulum>Kehadiran Pelajar>Tambah Acara
            </td>
        </tr>
    </table>
    <uc2:instruktor_view_header_simple ID="instruktor_view_header_simple1" runat="server" />
    <uc1:event_create ID="event_create1" runat="server" />
</asp:Content>
