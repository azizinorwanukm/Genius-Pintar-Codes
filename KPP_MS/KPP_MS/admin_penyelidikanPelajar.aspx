<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_penyelidikanPelajar.aspx.vb" Inherits="KPP_MS.admin_penyelidikanPelajar" %>

<%@ Register Src="~/commoncontrol/student_Research_List.ascx" TagPrefix="uc1" TagName="student_Research_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:student_Research_List runat="server" id="student_Research_List" />

</asp:Content>
