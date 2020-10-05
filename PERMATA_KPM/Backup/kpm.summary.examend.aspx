<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/kpm.Master" CodeBehind="kpm.summary.examend.aspx.vb" Inherits="permatapintar.kpm_summary_Examend" %>
<%@ Register src="commoncontrol/ukm1_examend_summary.ascx" tagname="ukm1_examend_summary" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ukm1_examend_summary ID="ukm1_examend_summary1" runat="server" />
</asp:Content>
