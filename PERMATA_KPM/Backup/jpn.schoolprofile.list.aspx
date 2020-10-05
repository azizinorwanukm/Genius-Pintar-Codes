<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/jpn.Master" CodeBehind="jpn.schoolprofile.list.aspx.vb" Inherits="permatapintar.jpn_schoolprofile_list" %>
<%@ Register src="commoncontrol/schoolprofile_list.ascx" tagname="schoolprofile_list" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:schoolprofile_list ID="schoolprofile_list1" runat="server" />
</asp:Content>
