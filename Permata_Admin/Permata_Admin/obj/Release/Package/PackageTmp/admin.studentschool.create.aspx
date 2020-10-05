<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.studentschool.create.aspx.vb" Inherits="permatapintar.admin_studentschool_create" %>

<%@ Register Src="commoncontrol/studentschool_create.ascx" TagName="studentschool_create" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentschool_create ID="studentschool_create1" runat="server" />
    &nbsp;
</asp:Content>
