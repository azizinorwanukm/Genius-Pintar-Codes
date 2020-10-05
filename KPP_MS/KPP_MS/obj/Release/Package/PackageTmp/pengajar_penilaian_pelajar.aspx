<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengajar.Master" CodeBehind="pengajar_penilaian_pelajar.aspx.vb" Inherits="KPP_MS.pengajar_penilaian_pelajar" %>

<%@ Register Src="~/commoncontrol/homeroom_assessment_remark.ascx" TagPrefix="uc1" TagName="homeroom_assessment_remark" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:homeroom_assessment_remark runat="server" id="homeroom_assessment_remark" />
</asp:Content>
