<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm.Master" CodeBehind="ukm.pusatujian.list.all.aspx.vb" Inherits="permatapintar.ukm_pusatujian_list_all" %>

<%@ Register Src="commoncontrol/pusatujian_petugas_list_all.ascx" TagName="pusatujian_petugas_list_all"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Pusat Ujian UKM2>Senarai Petugas" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:pusatujian_petugas_list_all ID="pusatujian_petugas_list_all1" runat="server" />
</asp:Content>
