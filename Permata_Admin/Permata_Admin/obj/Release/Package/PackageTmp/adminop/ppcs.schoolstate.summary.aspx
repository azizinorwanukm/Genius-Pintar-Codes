<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/adminop/adminoperator.Master" CodeBehind="ppcs.schoolstate.summary.aspx.vb" Inherits="permatapintar.ppcs_schoolstate_summary1" %>

<%@ Register Src="../commoncontrol/ppcs_state_sort.ascx" TagName="ppcs_state_sort" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td> 
                <asp:Label ID="Label1" runat="server" Text="Ringkasan PPCS>Ringkasan Negeri" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ppcs_state_sort ID="ppcs_state_sort1" runat="server" />
    &nbsp;
</asp:Content>