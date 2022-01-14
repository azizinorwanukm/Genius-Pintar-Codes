<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pelajar.Master" CodeBehind="pelajar_laporan_peperiksaan.aspx.vb" Inherits="KPP_SYS.pelajar_laporan_peperiksaan" %>

<%@ Register Src="~/commoncontrol/student_exam_information.ascx" TagPrefix="uc1" TagName="student_exam_information" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <script src="http://code.jquery.com/jquery-1.10.2.min.js" type="text/javascript"></script>

    <uc1:student_exam_information runat="server" ID="student_exam_information" />
</asp:Content>
