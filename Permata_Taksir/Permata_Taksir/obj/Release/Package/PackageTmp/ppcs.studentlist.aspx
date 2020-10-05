<%@ Page Title="" Language="vb" MaintainScrollPositionOnPostBack="true" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ppcs.studentlist.aspx.vb" Inherits="UKM3.ppcs_default" %>

<%@ Register Src="~/Control/PPCS/ppcs_studentlist.ascx" TagPrefix="uc1" TagName="ppcs_studentlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ppcs_studentlist runat="server" ID="ppcs_studentlist" />
</asp:Content>
