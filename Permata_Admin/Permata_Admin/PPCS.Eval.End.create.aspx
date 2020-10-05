<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="PPCS.Eval.End.create.aspx.vb" Inherits="permatapintar.PPCS_Eval_End_create" %>
<%@ Register src="commoncontrol/PPCS_Eval_End_create.ascx" tagname="PPCS_Eval_End_create" tagprefix="uc1" %>
<%@ Register src="commoncontrol/studentprofile_header.ascx" tagname="studentprofile_header" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc2:studentprofile_header ID="studentprofile_header1" runat="server" />
    &nbsp;
    <uc1:PPCS_Eval_End_create ID="PPCS_Eval_End_create1" runat="server" />
</asp:Content>
