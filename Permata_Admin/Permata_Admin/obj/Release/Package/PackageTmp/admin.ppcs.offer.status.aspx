<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.ppcs.offer.status.aspx.vb" Inherits="permatapintar.admin_ppcs_offer_status" %>

<%@ Register Src="commoncontrol/ppcs_offer_status.ascx" TagName="ppcs_offer_status" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblBreadcrum" runat="server" Text="PPCS>Status Tawaran" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ppcs_offer_status ID="ppcs_offer_status1" runat="server" />
    &nbsp;
</asp:Content>
