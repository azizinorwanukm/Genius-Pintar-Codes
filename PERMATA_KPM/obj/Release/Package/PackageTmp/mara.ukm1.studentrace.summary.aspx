<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/mara.Master" CodeBehind="mara.ukm1.studentrace.summary.aspx.vb" Inherits="permatapintar.mara_ukm1_studentrace_summary" %>

<%@ Register Src="commoncontrol/ukm1_studentrace_summary.ascx" TagName="ukm1_studentrace_summary" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblBreadcrum" runat="server" Text="Ujian UKM1>Status Ujian Bangsa." CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm1_studentrace_summary ID="ukm1_studentrace_summary1" runat="server" />
</asp:Content>
