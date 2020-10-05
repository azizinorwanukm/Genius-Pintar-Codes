<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/penguruspejabat/main.Master" CodeBehind="admin.ppcs.student.search.aspx.vb" Inherits="permatapintar.admin_ppcs_student_search1" %>
<%@ Register src="../commoncontrol/ppcs_student_search.ascx" tagname="ppcs_student_search" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ppcs_student_search ID="ppcs_student_search1" runat="server" />
</asp:Content>
