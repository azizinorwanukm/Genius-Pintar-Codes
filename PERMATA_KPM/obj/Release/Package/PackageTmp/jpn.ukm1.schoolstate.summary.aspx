﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/jpn.Master"
    CodeBehind="jpn.ukm1.schoolstate.summary.aspx.vb" Inherits="permatapintar.jpn_ukm1_schoolstate_summary" %>

<%@ Register Src="commoncontrol/ukm1_state_sort.ascx" TagName="ukm1_state_sort" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblBreadcrum" runat="server" Text="Ujian UKM1>Ringkasan Ujian UKM1>Ringkasan Ujian Negeri" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc2:ukm1_state_sort ID="ukm1_state_sort1" runat="server" />
</asp:Content>
