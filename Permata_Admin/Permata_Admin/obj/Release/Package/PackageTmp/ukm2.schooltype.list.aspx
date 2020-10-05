<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ukm2.schooltype.list.aspx.vb" Inherits="permatapintar.ukm2_schooltype_list" %>

<%@ Register Src="commoncontrol/ukm2_schooltype_list.ascx" TagName="ukm2_schooltype_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ukm2_schooltype_list ID="ukm2_schooltype_list1" runat="server" />
</asp:Content>
