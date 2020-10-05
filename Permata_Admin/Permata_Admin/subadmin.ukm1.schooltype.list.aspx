<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/subadmin.Master" CodeBehind="subadmin.ukm1.schooltype.list.aspx.vb" Inherits="permatapintar.subadmin_ukm1_schooltype_list" %>

<%@ Register src="commoncontrol/ukm1_schooltype_list.ascx" tagname="ukm1_schooltype_list" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ukm1_schooltype_list ID="ukm1_schooltype_list1" runat="server" />
</asp:Content>
