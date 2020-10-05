<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/jpn.Master"
    CodeBehind="jpn.ukm1.schoolstate.summary.aspx.vb" Inherits="permatapintar.jpn_ukm1_schoolstate_summary" %>

<%@ Register Src="commoncontrol/ukm1_state_summary.ascx" TagName="ukm1_state_summary"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/ukm1_state_sort.ascx" TagName="ukm1_state_sort" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ukm1_state_summary ID="ukm1_state_summary1" runat="server" />
    <uc2:ukm1_state_sort ID="ukm1_state_sort1" runat="server" />
</asp:Content>
