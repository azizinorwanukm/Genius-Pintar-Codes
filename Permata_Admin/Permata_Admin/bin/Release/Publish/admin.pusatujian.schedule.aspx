<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="admin.pusatujian.schedule.aspx.vb" Inherits="permatapintar.admin_pusatujian_schedule" %>

<%@ Register Src="commoncontrol/pusatujian_schedule.ascx" TagName="pusatujian_schedule" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Pusat Ujian UKM2>Jadual Pusat Ujian"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:pusatujian_schedule ID="pusatujian_schedule1" runat="server" />
</asp:Content>
