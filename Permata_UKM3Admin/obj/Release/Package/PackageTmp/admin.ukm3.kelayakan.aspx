<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.ukm3.kelayakan.aspx.vb" Inherits="permatapintar.admin_ukm3_kelayakan" %>

<%@ Register Src="commoncontrol/ukm3_kelayakan_select.ascx" TagName="ukm3_kelayakan_select"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="PPCS>Kelayakan ke UKM3" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm3_kelayakan_select ID="ukm3_kelayakan_select1" runat="server" />
</asp:Content>
