<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/penjaga.Master" CodeBehind="penjaga_bayaran_detail.aspx.vb" Inherits="KPP_SYS.penjaga_bayaran_detail" %>

<%@ Register Src="~/commoncontrol/parent_imagePayment_details.ascx" TagPrefix="uc1" TagName="parent_imagePayment_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:parent_imagePayment_details runat="server" id="parent_imagePayment_details" />

</asp:Content>
