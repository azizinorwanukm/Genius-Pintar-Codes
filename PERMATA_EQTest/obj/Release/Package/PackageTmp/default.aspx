<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master"
    CodeBehind="default.aspx.vb" Inherits="PERMATA_EQTest._default1" %>

<%@ Register Src="commoncontrol/eqtest_create.ascx" TagName="eqtest_create" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:eqtest_create ID="eqtest_create1" runat="server" />
    &nbsp;
</asp:Content>
