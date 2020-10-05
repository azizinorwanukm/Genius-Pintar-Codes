<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/instruktor/instruktor.Master" CodeBehind="instruktor.event.create.aspx.vb" Inherits="permatapintar.instruktor_event_create" %>

<%@ Register Src="../commoncontrol/event_create.ascx" TagName="event_create" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Menu Utama>Kehadiran Pelajar>Tambah Acara
            </td>
        </tr>
    </table>
    <uc1:event_create ID="event_create1" runat="server" />
</asp:Content>
