<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ppcs.schooltype.list.aspx.vb" Inherits="permatapintar.ppcs_schooltype_list" %>

<%@ Register Src="commoncontrol/ppcs_schooltype_list.ascx" TagName="ppcs_schooltype_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ppcs_schooltype_list ID="ppcs_schooltype_list1" runat="server" />
</asp:Content>
