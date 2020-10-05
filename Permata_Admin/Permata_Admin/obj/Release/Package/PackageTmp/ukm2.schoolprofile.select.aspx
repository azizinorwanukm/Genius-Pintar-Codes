<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="ukm2.schoolprofile.select.aspx.vb" Inherits="permatapintar.ukm2_schoolprofile_select" %>

<%@ Register Src="commoncontrol/schoolprofile_select.ascx" TagName="schoolprofile_select"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:schoolprofile_select ID="schoolprofile_select1" runat="server" />
    &nbsp;
</asp:Content>
