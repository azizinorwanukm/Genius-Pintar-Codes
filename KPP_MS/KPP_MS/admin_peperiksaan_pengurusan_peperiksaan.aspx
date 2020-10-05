<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_peperiksaan_pengurusan_peperiksaan.aspx.vb" Inherits="KPP_MS.admin_peperiksaan_pengurusan_peperiksaan" %>
<%@ Register src="commoncontrol/exam_List_Table.ascx" tagname="exam_List_Table" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <uc1:exam_List_Table ID="exam_List_Table" runat="server" />
    
</asp:Content>
