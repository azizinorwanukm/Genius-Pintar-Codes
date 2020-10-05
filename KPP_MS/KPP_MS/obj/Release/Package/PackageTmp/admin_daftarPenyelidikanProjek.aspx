<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_daftarPenyelidikanProjek.aspx.vb" Inherits="KPP_MS.admin_daftarPenyelidikanProjek" %>

<%@ Register Src="~/commoncontrol/Student_RegisterProject_List.ascx" TagPrefix="uc1" TagName="Student_RegisterProject_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:Student_RegisterProject_List runat="server" id="Student_RegisterProject_List" />
</asp:Content>
