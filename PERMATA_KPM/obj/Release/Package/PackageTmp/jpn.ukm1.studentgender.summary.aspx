﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/jpn.Master" CodeBehind="jpn.ukm1.studentgender.summary.aspx.vb" Inherits="permatapintar.jpn_ukm1_studentgender_summary" %>

<%@ Register Src="commoncontrol/ukm1_studentgender_summary.ascx" TagName="ukm1_studentgender_summary" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblBreadcrum" runat="server" Text="Ujian UKM1>Ringkasan Ujian UKM1>Ringkasan Ujian Jantina" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:ukm1_studentgender_summary ID="ukm1_studentgender_summary1"
        runat="server" />
</asp:Content>
