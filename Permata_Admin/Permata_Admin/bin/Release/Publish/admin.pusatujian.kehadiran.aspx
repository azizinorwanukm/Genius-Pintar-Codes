<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="admin.pusatujian.kehadiran.aspx.vb" Inherits="permatapintar.admin_pusatujian_kehadiran" %>

<%@ Register Src="commoncontrol/pusatujian_list_kehadiran.ascx" TagName="pusatujian_list_kehadiran"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ujian UKM2>Kehadiran Pelajar" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:pusatujian_list_kehadiran ID="pusatujian_list_kehadiran1" runat="server" />
</asp:Content>
