<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/penguruspejabat/main.Master"
    CodeBehind="pelajar.status.aspx.vb" Inherits="permatapintar.pelajar_status1" %>

<%@ Register Src="../commoncontrol/ppcs_student_status.ascx" TagName="ppcs_student_status"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ppcs_student_status ID="ppcs_student_status1" runat="server" />
    &nbsp;
</asp:Content>
