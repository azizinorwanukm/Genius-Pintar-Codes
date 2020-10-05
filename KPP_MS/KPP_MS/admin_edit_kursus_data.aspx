<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_edit_kursus_data.aspx.vb" Inherits="KPP_MS.admin_edit_kursus_data" %>

<%@ Register src="commoncontrol/course_Update.ascx" tagname="course_Update" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:course_Update ID="course_Update" runat="server" />

</asp:Content>
