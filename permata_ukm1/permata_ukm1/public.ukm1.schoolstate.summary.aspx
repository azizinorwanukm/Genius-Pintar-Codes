<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/popup.master" CodeBehind="public.ukm1.schoolstate.summary.aspx.vb" Inherits="permatapintar.public_ukm1_schoolstate_summary" %>

<%@ Register Src="commoncontrol/ukm1_state_summary.ascx" TagName="ukm1_state_summary" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ukm1_state_summary ID="ukm1_state_summary1" runat="server" />
    &nbsp;
</asp:Content>
