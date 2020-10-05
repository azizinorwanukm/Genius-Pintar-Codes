<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="tappcs.studentlist.aspx.vb" Inherits="UKM3.tappcs_studentlist" %>

<%@ Register Src="~/Control/TAPPCS/tappcs_prepostMark.ascx" TagPrefix="uc1" TagName="tappcs_prepostMark" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:tappcs_prepostMark runat="server" ID="tappcs_prepostMark" />
</asp:Content>
