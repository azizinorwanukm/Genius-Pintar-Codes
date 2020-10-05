<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/subadmin.Master" CodeBehind="subadmin.ukm2.studentprofile.register.aspx.vb" Inherits="permatapintar.subadmin_ukm2_studentprofile_register" %>

<%@ Register Src="commoncontrol/studentprofile_ukm2_create.ascx" TagName="studentprofile_ukm2_create"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/schoolprofile_view.ascx" TagName="schoolprofile_view"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc2:schoolprofile_view ID="schoolprofile_view1" runat="server" />
    &nbsp;<uc1:studentprofile_ukm2_create ID="studentprofile_ukm2_create1" runat="server" />
    &nbsp;
</asp:Content>
