<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="laporan.pelajar.class.select.aspx.vb" Inherits="permatapintar.laporan_pelajar_class_select" %>

<%@ Register Src="../commoncontrol/ppcs_course_view.ascx" TagName="ppcs_course_view"
    TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/ppcs_class_select.ascx" TagName="ppcs_class_select"
    TagPrefix="uc2" %>
<%@ Register Src="../commoncontrol/ppcs_list_courseid.ascx" TagName="ppcs_list_courseid"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ppcs_course_view ID="ppcs_course_view1" runat="server" />
    &nbsp;
    <uc2:ppcs_class_select ID="ppcs_class_select1" runat="server" />
    &nbsp;
    <uc3:ppcs_list_courseid ID="ppcs_list_courseid1" runat="server" />
</asp:Content>
