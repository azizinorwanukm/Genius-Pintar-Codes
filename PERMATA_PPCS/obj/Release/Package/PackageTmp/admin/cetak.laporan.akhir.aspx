<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="cetak.laporan.akhir.aspx.vb" Inherits="permatapintar.cetak_laporan_akhir" %>

<%@ Register Src="../commoncontrol/ppcs_class_view.ascx" TagName="ppcs_class_view"
    TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/ppcs_student_list.ascx" TagName="ppcs_student_list"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ppcs_class_view ID="ppcs_class_view1" runat="server" />
    &nbsp;<uc2:ppcs_student_list ID="ppcs_student_list1" runat="server" />
    &nbsp;
</asp:Content>
