<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_edit_asrama_data.aspx.vb" Inherits="KPP_MS.admin_edit_asrama_data" %>

<%@ Register Src="~/commoncontrol/hostel_Update.ascx" TagPrefix="uc1" TagName="hostel_Update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:hostel_Update runat="server" id="hostel_Update" />
</asp:Content>
