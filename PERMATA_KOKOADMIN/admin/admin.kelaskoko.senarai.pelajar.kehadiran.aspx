<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="admin.kelaskoko.senarai.pelajar.kehadiran.aspx.vb" Inherits="permatapintar.admin_kelaskoko_senarai_pelajar_kehadiran" %>

<%@ Register Src="~/commoncontrol/kelaskoko_list_pelajar.ascx" TagPrefix="uc1" TagName="kelaskoko_list_pelajar" %>
<%@ Register Src="~/commoncontrol/event_view.ascx" TagPrefix="uc2" TagName="event_view" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Kumpulan Sukan & Permainan>Kehadiran Pelajar>Senarai Pelajar
            </td>
        </tr>
    </table>
    <uc2:event_view runat="server" ID="event_view" />
    <uc1:kelaskoko_list_pelajar runat="server" id="kelaskoko_list_pelajar" />
</asp:Content>