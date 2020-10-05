<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="ppcs.eval.weekly.view.aspx.vb" Inherits="permatapintar.ppcs_eval_weekly_view" %>

<%@ Register Src="../commoncontrol/ppcs_eval_weekly_view.ascx" TagName="ppcs_eval_weekly_view" TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/studentprofile_header.ascx" TagName="studentprofile_header" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label2" runat="server" Text="PPCS > Laporan Mingguan" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc2:studentprofile_header ID="studentprofile_header1" runat="server" />&nbsp;
    <uc1:ppcs_eval_weekly_view ID="ppcs_eval_weekly_view1" runat="server" />
</asp:Content>
