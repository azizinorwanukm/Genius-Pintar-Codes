<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_kaunselor_portfolio.aspx.vb" Inherits="KPP_MS.admin_kaunselor_portfolio" %>

<%@ Register Src="~/commoncontrol/counselor_Portfolio.ascx" TagPrefix="uc1" TagName="counselor_Portfolio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:counselor_Portfolio runat="server" id="counselor_Portfolio" />
</asp:Content>
