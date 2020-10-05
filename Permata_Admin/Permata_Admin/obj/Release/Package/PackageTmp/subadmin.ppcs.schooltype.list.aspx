<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/subadmin.Master" CodeBehind="subadmin.ppcs.schooltype.list.aspx.vb" Inherits="permatapintar.subadmin_ppcs_schooltype_list" %>
<%@ Register src="commoncontrol/ppcs_schooltype_list.ascx" tagname="ppcs_schooltype_list" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ppcs_schooltype_list ID="ppcs_schooltype_list1" runat="server" />
</asp:Content>
