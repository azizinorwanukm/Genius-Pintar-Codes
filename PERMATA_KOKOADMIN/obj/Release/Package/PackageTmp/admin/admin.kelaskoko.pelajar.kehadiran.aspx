<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.kelaskoko.pelajar.kehadiran.aspx.vb" Inherits="permatapintar.admin_kelaskoko_pelajar_kehadiran" %>

<%@ Register Src="~/commoncontrol/event_view.ascx" TagPrefix="uc1" TagName="event_view" %>
<%@ Register Src="~/commoncontrol/kelaskoko_list_pelajar.ascx" TagPrefix="uc2" TagName="kelaskoko_list_pelajar" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Kumpulan Sukan & Permainan>Kehadiran Pelajar
            </td>
        </tr>
    </table>
    <uc1:event_view runat="server" ID="event_view" />
    <uc2:kelaskoko_list_pelajar runat="server" ID="kelaskoko_list_pelajar" />
</asp:Content>
