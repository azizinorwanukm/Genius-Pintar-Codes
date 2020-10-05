<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm.Master"
    CodeBehind="ukm.pusatujian.petugas.select.aspx.vb" Inherits="permatapintar.ukm_pusatujian_petugas_select" %>

<%@ Register Src="commoncontrol/pusatujian_petugas_select.ascx" TagName="pusatujian_petugas_select"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/pusatujian_view.ascx" TagName="pusatujian_view" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Pusat Ujian UKM2>Pusat Ujian>Assign Petugas" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc2:pusatujian_view ID="pusatujian_view1" runat="server" />
    &nbsp;<uc1:pusatujian_petugas_select ID="pusatujian_petugas_select1" runat="server" />
    &nbsp;
</asp:Content>
