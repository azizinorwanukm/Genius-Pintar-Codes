<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="kpm.summary.school.aspx.vb" Inherits="permatapintar.kpm_summary_school" %>
<%@ Register src="commoncontrol/ukm1_school_summary.ascx" tagname="ukm1_school_summary" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ukm1_school_summary ID="ukm1_school_summary1" runat="server" />
</asp:Content>
