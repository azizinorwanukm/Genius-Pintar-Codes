<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/mara.Master" CodeBehind="mara.ukm1.schoolstate.summary.all.aspx.vb" Inherits="permatapintar.mara_ukm1_schoolstate_summary_all" %>

<%@ Register Src="commoncontrol/ukm1_state_summary.ascx" TagName="ukm1_state_summary" TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/ukm1_state_sort.ascx" TagName="ukm1_state_sort" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblBreadcrum" runat="server" Text="Ujian UKM1>Ringkasan Ujian Negeri-SEMUA" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm1_state_summary ID="ukm1_state_summary1" runat="server" />
    &nbsp;
    <uc2:ukm1_state_sort ID="ukm1_state_sort1" runat="server" />
    <asp:LinkButton ID="lnkAllState" runat="server">Semua Jenis Sekolah</asp:LinkButton>
</asp:Content>
