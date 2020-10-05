<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/kpm.Master" CodeBehind="kpm.ukm1.schoolprofile.list.aspx.vb" Inherits="permatapintar.kpm_ukm1_schoolprofile_list" %>
<%@ Register src="commoncontrol/ukm1_schoolprofile_list.ascx" tagname="ukm1_schoolprofile_list" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ukm1_schoolprofile_list ID="ukm1_schoolprofile_list1" runat="server" />
</asp:Content>
