<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="admin.ppcs.kelayakan.aspx.vb" Inherits="permatapintar.admin_ppcs_kelayakan" %>

<%@ Register Src="commoncontrol/ppcs_kelayakan_select.ascx" TagName="ppcs_kelayakan_select"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ujian UKM2>Kelayakan ke PPCS" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ppcs_kelayakan_select ID="ppcs_kelayakan_select1" runat="server" />
</asp:Content>
