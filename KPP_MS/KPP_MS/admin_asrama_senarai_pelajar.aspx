<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_asrama_senarai_pelajar.aspx.vb" Inherits="KPP_MS.admin_asrama_senarai_pelajar" %>

<%@ Register Src="~/commoncontrol/hostel_List_Student.ascx" TagPrefix="uc1" TagName="hostel_List_Student" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:hostel_List_Student runat="server" id="hostel_List_Student" />
</asp:Content>
