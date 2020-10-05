<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="ukm1.schoolppd.summary.aspx.vb" Inherits="permatapintar.ukm1_schoolppd_summary1" %>

<%@ Register src="../commoncontrol/ukm1_schoolppd_summary.ascx" tagname="ukm1_schoolppd_summary" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ringkasan Ujian UKM1>Ringkasan Ujian PPD"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm1_schoolppd_summary ID="ukm1_schoolppd_summary1" runat="server" />
</asp:Content>
