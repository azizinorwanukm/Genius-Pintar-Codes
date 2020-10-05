<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="config_examStudentQuestionSetup.aspx.vb" Inherits="permatapintar.config_examStudentQuestionSetup" %>

<%@ Register Src="~/commoncontrol/config_examStudentQuestionSetup.ascx" TagPrefix="uc1" TagName="config_examStudentQuestionSetup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:config_examStudentQuestionSetup runat="server" ID="config_examStudentQuestionSetup" />
</asp:Content>
