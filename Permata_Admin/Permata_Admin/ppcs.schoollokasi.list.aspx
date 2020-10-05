<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ppcs.schoollokasi.list.aspx.vb" Inherits="permatapintar.ppcs_schoollokasi_list" %>

<%@ Register Src="commoncontrol/ppcs_schoollokasi_list.ascx" TagName="ppcs_schoollokasi_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ppcs_schoollokasi_list ID="ppcs_schoollokasi_list1" runat="server" />
</asp:Content>
