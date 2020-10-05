<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ketuapengurusakademik/main.Master"
    CodeBehind="laporan.keseluruhan.class.select.aspx.vb" Inherits="permatapintar.laporan_keseluruhan_class_select1" %>

<%@ Register Src="../commoncontrol/ppcs_class_select.ascx" TagName="ppcs_class_select"
    TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/ppcs_course_view.ascx" TagName="ppcs_course_view"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc2:ppcs_course_view ID="ppcs_course_view1" runat="server" />
    &nbsp;<uc1:ppcs_class_select ID="ppcs_class_select1" runat="server" />
    &nbsp;
</asp:Content>
