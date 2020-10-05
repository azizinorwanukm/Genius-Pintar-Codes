<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/instruktor/instruktor.Master" CodeBehind="instruktor.event.list.aspx.vb" Inherits="permatapintar.instruktor_event_list" %>

<%@ Register Src="../commoncontrol/event_list.ascx" TagName="event_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Menu Utama>Kehadiran Pelajar>Pilih Acara
            </td>
        </tr>
    </table>
    <uc1:event_list ID="event_list1" runat="server" />
</asp:Content>
