<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/subadmin.Master" CodeBehind="subadmin.ukm1.schoolstate.summary.aspx.vb" Inherits="permatapintar.subadmin_ukm1_schoolstate_summary" %>

<%@ Register src="commoncontrol/ukm1_state_summary.ascx" tagname="ukm1_state_summary" tagprefix="uc1" %>
<%@ Register src="commoncontrol/ukm1_state_sort.ascx" tagname="ukm1_state_sort" tagprefix="uc2" %>
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
    <uc1:ukm1_state_summary ID="ukm1_state_summary1" runat="server" />
    &nbsp;
    <uc2:ukm1_state_sort ID="ukm1_state_sort1" runat="server" />
</asp:Content>
