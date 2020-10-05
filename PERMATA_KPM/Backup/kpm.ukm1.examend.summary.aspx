<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/kpm.Master"
    CodeBehind="kpm.ukm1.examend.summary.aspx.vb" Inherits="permatapintar.kpm_ukm1_examend_summary" %>

<%@ Register Src="commoncontrol/ukm1_examstart_datelist.ascx" TagName="ukm1_examstart_datelist"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ukm1_examstart_datelist ID="ukm1_examstart_datelist1" runat="server" />
</asp:Content>
