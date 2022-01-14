<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pelajar.Master" CodeBehind="pelajar_pilih_kursus.aspx.vb" Inherits="KPP_SYS.pelajar_pilih_kursus" %>

<%@ Register Src="~/commoncontrol/student_update_coursePlacement.ascx" TagPrefix="uc1" TagName="student_update_coursePlacement" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:student_update_coursePlacement runat="server" ID="student_update_coursePlacement" />

</asp:Content>
