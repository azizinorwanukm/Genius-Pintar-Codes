<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/jpn.Master" CodeBehind="jpn.ukm1.dobyear.summary.aspx.vb" Inherits="permatapintar.jpn_ukm1_dobyear_summary" %>

<%@ Register Src="commoncontrol/ukm1_dobyear_summary.ascx" TagName="ukm1_dobyear_summary" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblBreadcrum" runat="server" Text="Ujian UKM1>Ringkasan Ujian UKM1>Ringkasan Ujian Umur" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm1_dobyear_summary ID="ukm1_dobyear_summary1" runat="server" />
</asp:Content>
