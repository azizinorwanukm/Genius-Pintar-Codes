<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ukm3_ppcs.kelayakan.aspx.vb" Inherits="permatapintar.ukm3_ppcs_kelayakan" %>

<%@ Register Src="~/commoncontrol/ukm3_adminPPCS_studentlist_mark.ascx" TagPrefix="uc1" TagName="ukm3_adminPPCS_studentlist_mark" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ukm3_adminPPCS_studentlist_mark runat="server" ID="ukm3_adminPPCS_studentlist_mark" />
</asp:Content>
