<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.ppcs.report.aspx.vb" Inherits="permatapintar.admin_ppcs_report" %>
<%@ Register src="commoncontrol/ppcs_report_01.ascx" tagname="ppcs_report_01" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="PPCS>Laporan PPCS" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
     <uc1:ppcs_report_01 ID="ppcs_report_011" runat="server" />
</asp:Content>
