<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengajar.Master" CodeBehind="penagajr_kemasukan_pelajar.aspx.vb" Inherits="KPP_MS.penagajr_kemasukan_pelajar" %>

<%@ Register Src="~/commoncontrol/lecturer_student_registration.ascx" TagPrefix="uc1" TagName="lecturer_student_registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:lecturer_student_registration runat="server" id="lecturer_student_registration" />
</asp:Content>
