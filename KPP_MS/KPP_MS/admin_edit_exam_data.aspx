<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_edit_exam_data.aspx.vb" Inherits="KPP_MS.admin_edit_exam_data" %>
<%@ Register src="commoncontrol/exam_Update.ascx" tagname="exam_Update" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <uc1:exam_Update ID="exam_Update" runat="server" />
    
</asp:Content>
