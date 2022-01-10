<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_kaunselor_pengurusanKaunselor.aspx.vb" Inherits="KPP_MS.admin_kaunselor_pengurusanKaunselor" %>

<%@ Register Src="~/commoncontrol/counselor_Management.ascx" TagPrefix="uc1" TagName="counselor_Management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:counselor_Management runat="server" id="counselor_Management" />
</asp:Content>
