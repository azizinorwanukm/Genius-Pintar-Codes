<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengajar.Master" CodeBehind="pengajar_koko_pangkatPelajar.aspx.vb" Inherits="KPP_MS.pengajar_koko_pangkatPelajar" %>

<%@ Register Src="~/commoncontrol/lecturer_koko_positionStudent.ascx" TagPrefix="uc1" TagName="lecturer_koko_positionStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:lecturer_koko_positionStudent runat="server" id="lecturer_koko_positionStudent" />
</asp:Content>
