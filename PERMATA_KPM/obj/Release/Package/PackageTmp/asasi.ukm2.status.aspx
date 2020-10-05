<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/asasi.Master" CodeBehind="asasi.ukm2.status.aspx.vb" Inherits="permatapintar.asasi_ukm2_status" %>

<%@ Register Src="commoncontrol/ukm2_status_list.ascx" TagName="ukm2_status_list"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ukm2_status_list ID="ukm2_status_list1" runat="server" />
</asp:Content>
