<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm.Master" CodeBehind="ukm.pusatujian.petugas.update.aspx.vb" Inherits="permatapintar.ukm_pusatujian_petugas_update" %>

<%@ Register Src="commoncontrol/pusatujian_petugas_update.ascx" TagName="pusatujian_petugas_update"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Pusat Ujian UKM2>Kemaskini Petugas" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:pusatujian_petugas_update ID="pusatujian_petugas_update1" runat="server" />
</asp:Content>
