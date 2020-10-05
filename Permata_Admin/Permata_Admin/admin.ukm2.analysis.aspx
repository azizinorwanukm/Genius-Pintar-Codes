<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.ukm2.analysis.aspx.vb" Inherits="permatapintar.admin_ukm2_analysis" %>
<%@ Register src="commoncontrol/ukm2_analisa.ascx" tagname="ukm2_analisa" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ujian UKM2>Analisa UKM2" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm2_analisa ID="ukm2_analisa1" runat="server" />
</asp:Content>
