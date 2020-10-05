<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_peperiksaan_pengurusan_gred.aspx.vb" Inherits="KPP_MS.admin_peperiksaan_pengurusan_gred" %>
<%@ Register src="commoncontrol/grade_List_Table.ascx" tagname="grade_List_Table" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:grade_List_Table ID="grade_List_Table" runat="server" />

</asp:Content>
