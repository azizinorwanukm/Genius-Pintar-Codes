<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="ppcs.eval.daily.view.aspx.vb" Inherits="permatapintar.ppcs_eval_daily_view" %>

<%@ Register Src="../commoncontrol/ppcs_eval_daily_view.ascx" TagName="ppcs_eval_daily_view" TagPrefix="uc1" %>
<%@ Register src="../commoncontrol/studentprofile_header.ascx" tagname="studentprofile_header" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="PPCS > Laporan Harian"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc2:studentprofile_header ID="studentprofile_header1" runat="server" />
&nbsp;<uc1:ppcs_eval_daily_view ID="ppcs_eval_daily_view1" runat="server" />
    &nbsp;
</asp:Content>
