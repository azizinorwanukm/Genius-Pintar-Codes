<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_transaksi_email.aspx.vb" Inherits="KPP_MS.admin_transaksi_email" %>

<%@ Register Src="~/commoncontrol/payment_Email.ascx" TagPrefix="uc1" TagName="payment_Email" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:payment_Email runat="server" ID="payment_Email" />
</asp:Content>
