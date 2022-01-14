<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/penjaga.Master" CodeBehind="penjaga_bayaran.aspx.vb" Inherits="KPP_SYS.penjaga_bayaran" %>

<%@ Register Src="~/commoncontrol/parent_imagePayment.ascx" TagPrefix="uc1" TagName="parent_imagePayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  
    <uc1:parent_imagePayment runat="server" id="parent_imagePayment" />
</asp:Content>
