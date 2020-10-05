<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="kelas.List.aspx.vb" Inherits="UKM3.kelas_List1" %>

<%@ Register Src="~/Control/kelas_List.ascx" TagPrefix="uc1" TagName="kelas_List" %>
<%@ Register Src="~/Control/kelas_courseView.ascx" TagPrefix="uc1" TagName="kelas_courseView" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:kelas_courseView runat="server" id="kelas_courseView" />
    <uc1:kelas_List runat="server" id="kelas_List" />
</asp:Content>
