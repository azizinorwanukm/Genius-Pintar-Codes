<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/jpn.Master"
    CodeBehind="jpn.ukm2.ukm2.list.aspx.vb" Inherits="permatapintar.jpn_ukm2_ukm2_list" %>

<%@ Register Src="commoncontrol/ukm2_select.ascx" TagName="ukm2_select" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblBreadcrum" runat="server" Text="Ujian UKM2>Senarai Pelajar" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm2_select ID="ukm2_select1" runat="server" />
</asp:Content>
