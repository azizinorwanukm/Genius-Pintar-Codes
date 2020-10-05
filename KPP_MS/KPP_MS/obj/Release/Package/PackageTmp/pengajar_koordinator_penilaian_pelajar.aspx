<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengajar.Master" CodeBehind="pengajar_koordinator_penilaian_pelajar.aspx.vb" Inherits="KPP_MS.pengajar_koordinator_penilaian_pelajar" %>

<%@ Register Src="~/commoncontrol/coordinator_assessment_remark.ascx" TagPrefix="uc1" TagName="coordinator_assessment_remark" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:coordinator_assessment_remark runat="server" id="coordinator_assessment_remark" />
</asp:Content>
