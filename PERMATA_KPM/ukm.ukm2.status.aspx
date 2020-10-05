<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm.Master" CodeBehind="ukm.ukm2.status.aspx.vb" Inherits="permatapintar.ukm_ukm2_status" %>

<%@ Register Src="commoncontrol/ukm2_status_list.ascx" TagName="ukm2_status_list"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Ujian UKM2>Status Ujian UKM2" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm2_status_list ID="ukm2_status_list1" runat="server" />
</asp:Content>