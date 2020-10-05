<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/penguruspejabat/main.Master"
    CodeBehind="ppcs.update.aspx.vb" Inherits="permatapintar.ppcs_update1" %>

<%@ Register Src="../commoncontrol/ppcs_update.ascx" TagName="ppcs_update" TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/studentprofile_header_ppcs.ascx" TagName="studentprofile_header_ppcs"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc2:studentprofile_header_ppcs ID="studentprofile_header_ppcs1" runat="server" />
    &nbsp;<uc1:ppcs_update ID="ppcs_update1" runat="server" />
</asp:Content>
