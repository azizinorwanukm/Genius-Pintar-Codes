<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_carian_pengajar.aspx.vb" Inherits="KPP_MS.admin_carian_pengajar" %>
<%@ Register src="commoncontrol/lecturer_List_Table.ascx" tagname="lecturer_List_Table" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <uc1:lecturer_List_Table ID="lecturer_List_Table" runat="server" />
    
</asp:Content>
