<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_transaksi_yuran.aspx.vb" Inherits="KPP_MS.admin_transaksi_yuran" %>

<%@ Register Src="~/commoncontrol/payment_Transaction.ascx" TagPrefix="uc1" TagName="payment_Transaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:payment_Transaction runat="server" id="payment_Transaction" />

</asp:Content>
