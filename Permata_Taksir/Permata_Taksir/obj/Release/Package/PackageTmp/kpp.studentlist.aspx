<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="kpp.studentlist.aspx.vb" Inherits="UKM3.kpp_default" %>

<%@ Register Src="~/Control/KPP/kpp_studentlist.ascx" TagPrefix="uc1" TagName="kpp_studentlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:kpp_studentlist runat="server" ID="kpp_studentlist" />
</asp:Content>
