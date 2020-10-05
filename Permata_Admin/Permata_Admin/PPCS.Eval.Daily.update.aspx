<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="PPCS.Eval.Daily.update.aspx.vb" Inherits="permatapintar.PPCS_Eval_Daily_update" %>

<%@ Register Src="commoncontrol/studentprofile_header.ascx" TagName="studentprofile_header"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/PPCS_Eval_Daily_update.ascx" TagName="PPCS_Eval_Daily_update"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_header ID="studentprofile_header1" runat="server" />
    &nbsp;
    <uc2:PPCS_Eval_Daily_update ID="PPCS_Eval_Daily_update1" runat="server" />
    &nbsp;
</asp:Content>
