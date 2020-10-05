<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="student.List.aspx.vb" Inherits="UKM3.student_List1" %>

<%@ Register Src="~/Control/student_List.ascx" TagPrefix="uc1" TagName="student_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:student_List runat="server" id="student_List" />
</asp:Content>
