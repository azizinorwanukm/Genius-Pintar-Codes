<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_pelajar_kepastian_kelas.aspx.vb" Inherits="KPP_MS.admin_pelajar_kepastian_kelas" %>

<%@ Register Src="~/commoncontrol/student_view_class.ascx" TagPrefix="uc1" TagName="student_view_class" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:student_view_class runat="server" id="student_view_class" />
</asp:Content>
