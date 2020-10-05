<%@ Page MaintainScrollPositionOnPostBack="true" Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ppcs.masukmarkah.aspx.vb" Inherits="UKM3.ppcs_masukmarkah" %>

<%@ Register Src="~/Control/PPCS/ppcs_studentlist_checklist.ascx" TagPrefix="uc1" TagName="ppcs_studentlist_checklist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ppcs_studentlist_checklist runat="server" ID="ppcs_studentlist_checklist" />
</asp:Content>
