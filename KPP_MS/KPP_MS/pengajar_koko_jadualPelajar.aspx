<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengajar.Master" CodeBehind="pengajar_koko_jadualPelajar.aspx.vb" Inherits="KPP_MS.pengajar_koko_jadualPelajar" %>

<%@ Register Src="~/commoncontrol/lecturer_koko_scheduleStudent.ascx" TagPrefix="uc1" TagName="lecturer_koko_scheduleStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:lecturer_koko_scheduleStudent runat="server" id="lecturer_koko_scheduleStudent" />
</asp:Content>
