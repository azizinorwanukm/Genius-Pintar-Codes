<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="PPCS.Eval.End.update.aspx.vb" Inherits="permatapintar.PPCS_Eval_End_update" %>
<%@ Register src="commoncontrol/studentprofile_header.ascx" tagname="studentprofile_header" tagprefix="uc1" %>
<%@ Register src="commoncontrol/PPCS_Eval_End_update.ascx" tagname="PPCS_Eval_End_update" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_header ID="studentprofile_header1" runat="server" />
    &nbsp;
    <uc2:PPCS_Eval_End_update ID="PPCS_Eval_End_update1" runat="server" />
</asp:Content>
