<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm.Master" CodeBehind="ukm.pusatujian.student.select.aspx.vb" Inherits="permatapintar.ukm_pusatujian_student_select" %>

<%@ Register src="commoncontrol/pusatujian_view.ascx" tagname="pusatujian_view" tagprefix="uc1" %>
<%@ Register src="commoncontrol/pusatujian_student_select.ascx" tagname="pusatujian_student_select" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pusatujian_view ID="pusatujian_view1" runat="server" />
    &nbsp;
    <uc2:pusatujian_student_select ID="pusatujian_student_select1" runat="server" />
</asp:Content>
