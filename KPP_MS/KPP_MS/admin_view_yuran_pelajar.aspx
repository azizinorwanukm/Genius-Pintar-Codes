<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_view_yuran_pelajar.aspx.vb" Inherits="KPP_MS.admin_view_yuran_pelajar" %>

<%@ Register Src="~/commoncontrol/payment_view_invoice_student.ascx" TagPrefix="uc1" TagName="payment_view_invoice_student" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:payment_view_invoice_student runat="server" id="payment_view_invoice_student" />
</asp:Content>
