<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/subadmin.Master" CodeBehind="subadmin.ppcs.update.aspx.vb" Inherits="permatapintar.subadmin_ppcs_update" %>

<%@ Register Src="commoncontrol/ppcs_update.ascx" TagName="ppcs_update" TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/studentprofile_view.ascx" TagName="studentprofile_view"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc2:studentprofile_view ID="studentprofile_view1" runat="server" />
    &nbsp;<uc1:ppcs_update ID="ppcs_update1" runat="server" />
    &nbsp;
</asp:Content>