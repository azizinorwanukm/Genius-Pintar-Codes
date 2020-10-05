<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_transaksi_yuran_gambar.aspx.vb" Inherits="KPP_MS.admin_transaksi_yuran_gambar" %>

<%@ Register Src="~/commoncontrol/payment_admin_image.ascx" TagPrefix="uc1" TagName="payment_admin_image" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <uc1:payment_admin_image runat="server" id="payment_admin_image" />
    
</asp:Content>
