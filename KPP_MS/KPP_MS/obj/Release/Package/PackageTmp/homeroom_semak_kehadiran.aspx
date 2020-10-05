<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengajar.Master" CodeBehind="homeroom_semak_kehadiran.aspx.vb" Inherits="KPP_MS.homeroom_semak_kehadiran" %>

<%@ Register Src="~/commoncontrol/homeroom_ViewAttendance.ascx" TagPrefix="uc1" TagName="homeroom_ViewAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:homeroom_ViewAttendance runat="server" ID="homeroom_ViewAttendance" />
</asp:Content>
