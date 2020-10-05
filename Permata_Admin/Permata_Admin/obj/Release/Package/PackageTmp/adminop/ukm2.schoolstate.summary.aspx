<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="ukm2.schoolstate.summary.aspx.vb" Inherits="permatapintar.ukm2_schoolstate_summary1" %>

<%@ Register Src="../commoncontrol/ukm2_state_sort.ascx" TagName="ukm2_state_sort" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ringkasan Ujian UKM2>Ringkasan Ujian Negeri"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    &nbsp;<uc2:ukm2_state_sort ID="ukm2_state_sort1" runat="server" />
</asp:Content>
