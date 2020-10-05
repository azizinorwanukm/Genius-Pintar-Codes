<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="config_examStudentQuestionList.aspx.vb" Inherits="permatapintar.WebForm2" %>

<%@ Register Src="~/commoncontrol/config_examStudentQuestionList.ascx" TagPrefix="uc1" TagName="config_examStudentQuestionList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:config_examStudentQuestionList runat="server" ID="config_examStudentQuestionList" />
</asp:Content>
