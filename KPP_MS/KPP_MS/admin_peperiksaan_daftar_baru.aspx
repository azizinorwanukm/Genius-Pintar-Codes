<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_peperiksaan_daftar_baru.aspx.vb" Inherits="KPP_MS.admin_peperiksaan_daftar_baru" %>

<%@ Register src="commoncontrol/exam_Create.ascx" tagname="exam_Create" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <uc1:exam_Create ID="exam_Create" runat="server" />
    
</asp:Content>
