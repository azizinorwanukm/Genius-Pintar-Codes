<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="settings.aspx.vb" Inherits="permatapintar.settings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Lain-lain>Sistem Konfigurasi V2"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <table class="fbform">
        <tr>
            <td><a href="ukm1_toplist.aspx" rel="nofollow" target="_self">Senarai Penyertaan Tertinggi UKM1 </a></td>
        </tr>
        <tr>
            <td><a href="admin.resetpartb.aspx" rel="nofollow" target="_self">UKM1: Reset Pelajar ke Soalan B</a></td>
        </tr>
        <tr>
            <td><a href="admin_display_examresults.aspx" rel="nofollow" target="_self">Paparan Keputusan UKM1 & UKM2</a></td>
        </tr>
        <tr>
            <td><a href="admin_displayppcs.aspx" rel="nofollow" target="_self">Paparan Keputusan PPCS</a></td>
        </tr>
        <tr>
            <td><a href="admin_survey_online.aspx" rel="nofollow" target="_self">Senarai Survey ID</a></td>
        </tr>
        <tr>
            <td><a href="admin_dob.aspx" rel="nofollow" target="_self">Senarai Tahun Lahir</a></td>
        </tr>
    </table>


</asp:Content>
