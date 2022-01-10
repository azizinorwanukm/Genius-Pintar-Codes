<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_edit_yuran.aspx.vb" Inherits="KPP_MS.admin_edit_yuran" %>

<%@ Register Src="~/commoncontrol/payment_edit.ascx" TagPrefix="uc1" TagName="payment_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:payment_edit runat="server" id="payment_edit" />
</asp:Content>
