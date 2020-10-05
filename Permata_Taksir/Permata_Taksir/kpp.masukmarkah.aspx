<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="kpp.masukmarkah.aspx.vb" Inherits="UKM3.kpp_masukmarkah" %>

<%@ Register Src="~/Control/KPP/kpp_studentlist_checklist.ascx" TagPrefix="uc1" TagName="kpp_studentlist_checklist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:kpp_studentlist_checklist runat="server" ID="kpp_studentlist_checklist" />
</asp:Content>
