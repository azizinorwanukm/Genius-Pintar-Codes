<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="ukm2.schoolppd.summary.aspx.vb" Inherits="permatapintar.ukm2_schoolppd_summary" %>

<%@ Register Src="../commoncontrol/ukm2_ppd_summary.ascx" TagName="ukm2_ppd_summary" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ringkasan Ujian UKM2>Ringkasan Ujian PPD"
                    CssClass="lblBreadcrum"></asp:Label>

            </td>
        </tr>
    </table>
    <uc1:ukm2_ppd_summary ID="ukm2_ppd_summary1" runat="server" />
</asp:Content>
