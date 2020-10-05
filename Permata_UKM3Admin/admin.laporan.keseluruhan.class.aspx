<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.laporan.keseluruhan.class.aspx.vb" Inherits="permatapintar.admin_laporan_keseluruhan_class" %>

<%@ Register Src="~/commoncontrol/ppcs_course_view.ascx" TagPrefix="uc1" TagName="ppcs_course_view" %>
<%@ Register Src="~/commoncontrol/ppcs_class_select.ascx" TagPrefix="uc2" TagName="ppcs_class_select" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <uc1:ppcs_course_view runat="server" id="ppcs_course_view" />
    <uc2:ppcs_class_select runat="server" id="ppcs_class_select" />
</asp:Content>
