<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.ukm2.view.aspx.vb" Inherits="permatapintar.admin_ukm2_view" %>

<%@ Register Src="commoncontrol/studentprofile_view.ascx" TagName="studentprofile_view" TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/ukm2_view.ascx" TagName="ukm2_view" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_view ID="studentprofile_view1" runat="server" />
    &nbsp;
    
    <uc2:ukm2_view ID="ukm2_view1" runat="server" />
</asp:Content>
