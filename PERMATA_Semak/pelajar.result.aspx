<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master" CodeBehind="pelajar.result.aspx.vb" Inherits="UKM_SEMAKAN.pelajar_result" %>

<%@ Register Src="mycontrol/kolej_list.ascx" TagName="kolej_list" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="default.aspx">
        <img src="images/pp-logo.png" alt="logo" /></a>
    <h2>Selamat Datang ke Laman Semakan PERMATApintar</h2>
    &nbsp;
    <uc1:kolej_list ID="kolej_list1" runat="server" />
</asp:Content>
