<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_classList.aspx.vb" Inherits="permatapintar.admin_classList1" %>
<%@ Register Src="~/commoncontrol/admin.classList.ascx" TagPrefix="uc1" TagName="adminclassList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:adminclassList runat="server" id="adminclassList" />
</asp:Content>
