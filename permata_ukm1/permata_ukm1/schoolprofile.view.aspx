<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm1.default.Master" CodeBehind="schoolprofile.view.aspx.vb" Inherits="permatapintar.schoolprofile_view1" %>
<%@ Register src="commoncontrol/studentschool_view.ascx" tagname="studentschool_view" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentschool_view ID="studentschool_view1" runat="server" />
    
</asp:Content>
