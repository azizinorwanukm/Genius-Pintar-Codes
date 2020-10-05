<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/instruktor/instruktor.Master" CodeBehind="instruktor.event.list.all.aspx.vb" Inherits="permatapintar.instruktor_event_list_all" %>


<%@ Register Src="../commoncontrol/event_list_all.ascx" TagName="event_list_all" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Kokurikulum>Kehadiran Pelajar>Senarai Acara
                
            </td>
        </tr>
    </table>
    <uc1:event_list_all ID="event_list_all1" runat="server" />

</asp:Content>