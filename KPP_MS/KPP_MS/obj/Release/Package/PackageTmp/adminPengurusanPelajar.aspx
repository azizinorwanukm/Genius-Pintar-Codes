<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="adminPengurusanPelajar.aspx.vb" Inherits="KPP_MS.adminPengurusanPelajar" %>

<%@ Register Src="~/commoncontrol/student_Update.ascx" TagPrefix="uc1" TagName="student_Update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <uc1:student_Update runat="server" ID="student_Update" />


</asp:Content>
