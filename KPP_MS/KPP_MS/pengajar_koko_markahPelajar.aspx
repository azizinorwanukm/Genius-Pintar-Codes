<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengajar.Master" CodeBehind="pengajar_koko_markahPelajar.aspx.vb" Inherits="KPP_MS.pengajar_koko_markahPelajar" %>

<%@ Register Src="~/commoncontrol/lecturer_koko_markStudent.ascx" TagPrefix="uc1" TagName="lecturer_koko_markStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:lecturer_koko_markStudent runat="server" id="lecturer_koko_markStudent" />
</asp:Content>
