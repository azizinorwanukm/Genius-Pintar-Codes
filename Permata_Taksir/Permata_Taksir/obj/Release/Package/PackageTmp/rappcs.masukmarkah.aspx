<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="rappcs.masukmarkah.aspx.vb" Inherits="UKM3.rappcs_masukmarkah" %>

<%@ Register Src="~/Control/RAPPCS/rappcs_studentlist_checklist.ascx" TagPrefix="uc1" TagName="rappcs_studentlist_checklist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:rappcs_studentlist_checklist runat="server" ID="rappcs_studentlist_checklist" />
</asp:Content>
