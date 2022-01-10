<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_transaksi_yuran_view.aspx.vb" Inherits="KPP_MS.admin_transaksi_yuran_view" %>

<%@ Register Src="~/commoncontrol/payment_Transaction_list.ascx" TagPrefix="uc1" TagName="payment_Transaction_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:payment_Transaction_list runat="server" id="payment_Transaction_list" />
</asp:Content>
