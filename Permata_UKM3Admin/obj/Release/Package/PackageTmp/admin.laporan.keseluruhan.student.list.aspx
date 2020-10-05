<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.laporan.keseluruhan.student.list.aspx.vb" Inherits="permatapintar.admin_laporan_keseluruhan_student_list" %>

<%@ Register Src="~/commoncontrol/ppcs_class_view.ascx" TagPrefix="uc1" TagName="ppcs_class_view" %>
<%@ Register Src="~/commoncontrol/ppcs_student_list.ascx" TagPrefix="uc1" TagName="ppcs_student_list" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ppcs_class_view runat="server" ID="ppcs_class_view" />
    <uc1:ppcs_student_list runat="server" ID="ppcs_student_list" />
</asp:Content>
