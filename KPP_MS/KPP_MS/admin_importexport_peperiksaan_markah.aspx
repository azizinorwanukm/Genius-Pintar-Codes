<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_importexport_peperiksaan_markah.aspx.vb" Inherits="KPP_MS.admin_importexport_peperiksaan_markah" %>

<%@ Register Src="~/commoncontrol/import_exam_marks.ascx" TagPrefix="uc1" TagName="import_exam_marks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:import_exam_marks runat="server" ID="import_exam_marks" />
</asp:Content>
