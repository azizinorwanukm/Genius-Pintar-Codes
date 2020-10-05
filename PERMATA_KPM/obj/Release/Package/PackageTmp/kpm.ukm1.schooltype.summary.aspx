<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/kpm.Master" CodeBehind="kpm.ukm1.schooltype.summary.aspx.vb" Inherits="permatapintar.kpm_ukm1_schooltype_summary" %>

<%@ Register Src="commoncontrol/ukm1_schooltype_summary.ascx" TagName="ukm1_schooltype_summary" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblBreadcrum" runat="server" Text="Ujian UKM1>Ringkasan Ujian UKM1>Ringkasan Ujian Jenis Sekolah" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm1_schooltype_summary ID="ukm1_schooltype_summary1" runat="server" />
</asp:Content>
