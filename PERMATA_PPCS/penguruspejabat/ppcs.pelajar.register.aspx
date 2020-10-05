<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/penguruspejabat/main.Master"
    CodeBehind="ppcs.pelajar.register.aspx.vb" Inherits="permatapintar.ppcs_pelajar_register1" %>

<%@ Register Src="../commoncontrol/studentprofile_ppcs_create.ascx" TagName="studentprofile_ppcs_create"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_ppcs_create ID="studentprofile_ppcs_create1" runat="server" />
</asp:Content>
