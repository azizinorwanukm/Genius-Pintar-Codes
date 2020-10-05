<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="Import_Exam_Data.aspx.vb" Inherits="KPP_MS.Import_Exam_Data" %>

<%@ Register Src="~/commoncontrol/test_import.ascx" TagPrefix="uc1" TagName="test_import" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:test_import runat="server" id="test_import" />
</asp:Content>
