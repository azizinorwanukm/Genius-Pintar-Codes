<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_pelajar_kepastian_agama.aspx.vb" Inherits="KPP_MS.admin_pelajar_kepastian_agama" %>

<%@ Register Src="~/commoncontrol/student_view_religion.ascx" TagPrefix="uc1" TagName="student_view_religion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:student_view_religion runat="server" id="student_view_religion" />
</asp:Content>
