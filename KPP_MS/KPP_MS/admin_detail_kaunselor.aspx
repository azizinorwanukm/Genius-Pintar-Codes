<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_detail_kaunselor.aspx.vb" Inherits="KPP_MS.admin_detail_kaunselor" %>

<%@ Register Src="~/commoncontrol/counselor_Detal_Case.ascx" TagPrefix="uc1" TagName="counselor_Detal_Case" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:counselor_Detal_Case runat="server" id="counselor_Detal_Case" />
</asp:Content>
