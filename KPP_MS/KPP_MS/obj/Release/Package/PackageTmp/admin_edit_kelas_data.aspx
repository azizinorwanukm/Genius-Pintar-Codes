<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_edit_kelas_data.aspx.vb" Inherits="KPP_MS.admin_edit_kelas_data" %>

<%@ Register src="commoncontrol/class_Update.ascx" tagname="class_Update" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <uc1:class_Update ID="class_Update" runat="server" />
    
</asp:Content>
