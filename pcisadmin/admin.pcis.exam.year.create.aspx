<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.pcis.exam.year.create.aspx.vb" Inherits="araken.pcisadmin.admin_pcis_exam_year_create" %>

<%@ Register Src="commoncontrol/pcis_exam_year_create.ascx" TagName="pcis_exam_year_create" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pcis_exam_year_create ID="pcis_exam_year_create1" runat="server" />
    &nbsp;
</asp:Content>
