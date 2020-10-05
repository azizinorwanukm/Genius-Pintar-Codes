<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="kpp.studentselection.aspx.vb" Inherits="UKM3.kpp_studentselection" %>

<%@ Register Src="~/Control/KPP/kpp_studentselection.ascx" TagPrefix="uc1" TagName="kpp_studentselection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:kpp_studentselection runat="server" ID="kpp_studentselection" />
</asp:Content>
