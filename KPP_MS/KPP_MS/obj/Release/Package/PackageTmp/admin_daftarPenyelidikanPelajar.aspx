<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_daftarPenyelidikanPelajar.aspx.vb" Inherits="KPP_MS.admin_daftarPenyelidikanPelajar" %>

<%@ Register Src="~/commoncontrol/Student_RegisterResearch_List.ascx" TagPrefix="uc1" TagName="Student_RegisterResearch_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:Student_RegisterResearch_List runat="server" id="Student_RegisterResearch_List" />
</asp:Content>
