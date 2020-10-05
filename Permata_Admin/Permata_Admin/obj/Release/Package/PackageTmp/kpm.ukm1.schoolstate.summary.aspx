<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="kpm.ukm1.schoolstate.summary.aspx.vb" Inherits="permatapintar.kpm_ukm1_schoolstate_summary" %>

<%@ Register Src="commoncontrol/ukm1_state_sort.ascx" TagName="ukm1_state_sort" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ringkasan Ujian UKM1>Ringkasan Ujian Negeri"
                    CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm1_state_sort ID="ukm1_state_sort1" runat="server" />
</asp:Content>
