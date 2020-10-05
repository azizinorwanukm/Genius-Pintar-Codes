<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="ppcs.studentgender.summary.aspx.vb" Inherits="permatapintar.ppcs_studentgender_summary" %>

<%@ Register Src="commoncontrol/ppcs_studentgender_summary.ascx" TagName="ppcs_studentgender_summary"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ringkasan PPCS>Ringkasan Jantina" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ppcs_studentgender_summary ID="ppcs_studentgender_summary1" runat="server" /> 
</asp:Content>
