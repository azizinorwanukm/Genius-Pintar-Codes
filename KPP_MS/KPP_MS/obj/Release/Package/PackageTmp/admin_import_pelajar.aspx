<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_import_pelajar.aspx.vb" Inherits="KPP_MS.admin_import_pelajar" %>

<%@ Register Src="~/commoncontrol/import_student.ascx" TagPrefix="uc1" TagName="import_student" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:import_student runat="server" id="import_student" />
</asp:Content>
