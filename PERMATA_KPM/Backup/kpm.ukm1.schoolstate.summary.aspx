<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/kpm.Master" CodeBehind="kpm.ukm1.schoolstate.summary.aspx.vb" Inherits="permatapintar.kpm_ukm1_schoolstate_summary" %>

<%@ Register src="commoncontrol/ukm1_state_summary.ascx" tagname="ukm1_state_summary" tagprefix="uc1" %>
<%@ Register src="commoncontrol/ukm1_state_sort.ascx" tagname="ukm1_state_sort" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ukm1_state_summary ID="ukm1_state_summary1" runat="server" />
    &nbsp;
    <uc2:ukm1_state_sort ID="ukm1_state_sort1" runat="server" />
</asp:Content>
