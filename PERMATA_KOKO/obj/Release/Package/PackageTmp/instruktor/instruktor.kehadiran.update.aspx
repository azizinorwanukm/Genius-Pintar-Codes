<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/instruktor/instruktor.Master" CodeBehind="instruktor.kehadiran.update.aspx.vb" Inherits="permatapintar.instruktor_kehadiran_update" %>

<%@ Register Src="../commoncontrol/instruktor_view_header.ascx" TagName="instruktor_view_header" TagPrefix="uc2" %>
<%@ Register Src="../commoncontrol/event_view.ascx" TagName="event_view" TagPrefix="uc3" %>
<%@ Register Src="../commoncontrol/event_pelajar_select.ascx" TagName="event_pelajar_select" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Kokurikulum>Kehadiran Pelajar
            </td>
        </tr>
    </table>
    <uc3:event_view ID="event_view1" runat="server" />

    <uc1:event_pelajar_select ID="event_pelajar_select1" runat="server" />
</asp:Content>
