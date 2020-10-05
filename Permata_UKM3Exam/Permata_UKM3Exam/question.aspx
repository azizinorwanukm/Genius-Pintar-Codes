<%@ Page Title="" Language="vb" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" MasterPageFile="~/question.Master" CodeBehind="question.aspx.vb" Inherits="UKM3.question1" %>

<%@ Register Src="Control/question.ascx" TagName="question" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:question ID="question1" runat="server" />
    &nbsp;
</asp:Content>
